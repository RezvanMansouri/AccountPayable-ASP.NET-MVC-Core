using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment4.Models;
using Assignment4.Models.DbGenerated;
using Microsoft.AspNetCore.Http;

namespace Assignment4.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly apContext _apContext;

        public InvoicesController(apContext apcontext)
        {
            _apContext = apcontext;
        }
        

        /// <summary>
        /// this method is the action for loading the invoice page from vendor
        /// </summary>
        /// <param name="id"> vendorID</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(int id)
        {
            //list of invoices for the specefic vendor
            var list = _apContext.Invoices.Include(f => f.InvoiceLineItems).Where(g => g.VendorId == id).ToList();
            HttpContext.Session.SetInt32("Vendor", id);

            //make the invoice as  defult when we get to this page
            var activeInvoice = _apContext.Invoices.Include(f => f.InvoiceLineItems).Where(g => g.VendorId == id).FirstOrDefault();
            
            //if the list is empty so there is not invoice for that vendor , then show a temp message
            if (activeInvoice == null)
            {
                TempData["msg"] = "There is no invoice for this vendor";
                return RedirectToAction($"{HttpContext.Session.GetString("selectedTab")}", "Vendors");
            }
            //get the specefic lineItems for the defult vendor
            var selecteditem = _apContext.InvoiceLineItems.Where(i => i.InvoiceId == activeInvoice.InvoiceId);
            ViewBag.item = selecteditem;

            // get term due days for the invoices
            var selectedVendor = _apContext.Vendors.Find(id);
            int term = _apContext.Terms.Find(selectedVendor.DefaultTermsId).TermsDueDays;

            HttpContext.Session.SetString("selectedinvoice", activeInvoice.InvoiceNumber);

            return View(new InvoicesByView() { Invoices = list, invoiceLineItem = new InvoiceLineItem(), vendor = selectedVendor , term =term});
        }

        //this action calls when one of the invoices is clicked

        [HttpGet(("/Invoices/SelectInvoice/{id?}"))]
        public IActionResult SelectInvoice(int id)
        {
            //get the vedor id from session to load the related information again
            int ID = (int)HttpContext.Session.GetInt32("Vendor");

            // get the list of invoices again for the vendor
            var list = _apContext.Invoices.Include(f => f.InvoiceLineItems).Where(g => g.VendorId == ID).ToList();

            //find the selected invoice
            var activeInvoice = _apContext.Invoices.Where(g => g.InvoiceId == id).FirstOrDefault();

            //find the lineitems for the inovice and puy them in a viewbag
            var selecteditem = _apContext.InvoiceLineItems.Where(i => i.InvoiceId == id);
            ViewBag.item = selecteditem;

            //find the terms for the invoices
            var selectedVendor = _apContext.Vendors.Find(ID);
            int term = _apContext.Terms.Find(selectedVendor.DefaultTermsId).TermsDueDays;

            HttpContext.Session.SetString("selectedinvoice", activeInvoice.InvoiceNumber);
            HttpContext.Session.SetInt32("invoiceID", activeInvoice.InvoiceId);

            return View("Index", new InvoicesByView() { Invoices = list, invoiceLineItem = new InvoiceLineItem(), vendor = selectedVendor, term = term });
        }


        //post method for the form
        [HttpPost("/Invoices")]
        public IActionResult Add(InvoicesByView invoiceItem)
        {
            if (ModelState.IsValid)
            {
                //if inputs are valid add them in the database and save
                _apContext.InvoiceLineItems.Add(invoiceItem.invoiceLineItem);
                _apContext.SaveChanges();

                return RedirectToAction("SelectInvoice", "Invoices", new { id = invoiceItem.invoiceLineItem.InvoiceId });
            }
            else
            {
                //if not just return to same page with the same invoice id
                return SelectInvoice(invoiceItem.invoiceLineItem.InvoiceId);
            }
        }
    }
}


