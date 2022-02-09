using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment4.Models;
using Assignment4.Models.DbGenerated;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment4.Controllers
{
    public class VendorController : Controller
    {
        private apContext _apContext;

        public VendorController(apContext apx) => _apContext = apx;

        //GET
        //Add action for a vendor
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.terms = _apContext.Terms.OrderBy(a => a.TermsDueDays).ToList();
            ViewBag.glAccount = _apContext.GeneralLedgerAccounts.OrderBy(b => b.AccountNumber).ToList();

            ViewBag.Action = "Add";

            return View("Edit", new Vendor());
        }



        //POST
        //Add action for a vendor
        [HttpPost]

        public ActionResult Add(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _apContext.Vendors.Add(vendor);
                _apContext.SaveChanges();

                string selectedTab = HttpContext.Session.GetString("selectedTab");

                return RedirectToAction(selectedTab, "Vendors");
            }
            else
            {
                ModelState.AddModelError("", "There are errors in the form.");

                ViewBag.Action = "Add";

                ViewBag.terms = _apContext.Terms.OrderBy(a => a.TermsDueDays).ToList();
                ViewBag.glAccount = _apContext.GeneralLedgerAccounts.OrderBy(b => b.AccountNumber).ToList();

                return View("Edit", vendor);
            }
        }


        //GET
        //Edit action for a vendor
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";

            var vendor = _apContext.Vendors.Find(id);
            ViewBag.terms = _apContext.Terms.OrderBy(a => a.TermsDueDays).ToList();
            ViewBag.glAccount = _apContext.GeneralLedgerAccounts.OrderBy(b => b.AccountNumber).ToList();

            return View(vendor);
        }


        //POST
        //Add action for a vendor
        [HttpPost]
        public ActionResult Edit(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _apContext.Vendors.Update(vendor);
                _apContext.SaveChanges();

                string selectedTab = HttpContext.Session.GetString("selectedTab");

                return RedirectToAction(selectedTab, "Vendors");
            }
            else
            {
                ModelState.AddModelError("", "There are errors in the form.");

                ViewBag.Action = "Edit";

                ViewBag.terms = _apContext.Terms.OrderBy(a => a.TermsDueDays).ToList();
                ViewBag.glAccount = _apContext.GeneralLedgerAccounts.OrderBy(b => b.AccountNumber).ToList();

                return View(vendor);
            }
        }


        //Delete Action
        public ActionResult Delete(int id)
        {
            var vendor = _apContext.Vendors.Find(id);

            //set the deleted field to true
            vendor.IsDeleted = true;

            _apContext.Vendors.Update(vendor);
            _apContext.SaveChanges();

            HttpContext.Session.SetInt32("idForDelete", vendor.VendorId);
            TempData["alertMessage"] = $"The vendor {vendor.VendorName} was deleted. ";

            string selectedTab = HttpContext.Session.GetString("selectedTab");

            return RedirectToAction(selectedTab, "Vendors");
        }

        //Undo Action
        public ActionResult Undo()
        {
            int id = (int)HttpContext.Session.GetInt32("idForDelete");

            var vendor = _apContext.Vendors.Find(id);

            //set back the deleted field to false
            vendor.IsDeleted = false;

            _apContext.Vendors.Update(vendor);
            _apContext.SaveChanges();

            string selectedTab = HttpContext.Session.GetString("selectedTab");

            return RedirectToAction(selectedTab, "Vendors");
        }

    }
}
