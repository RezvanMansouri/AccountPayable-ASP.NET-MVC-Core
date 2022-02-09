using Assignment4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment4.Models.DbGenerated;
using Microsoft.AspNetCore.Http;


namespace Assignment4.Controllers
{
    public class VendorsController : Controller
    {
        apContext _apContext { get; set; }
        public VendorsController(apContext apContext)
        {
            _apContext = apContext;
        }

        //4 Action for different group of letter and by defaul AtoE is set

        [HttpGet]

        public IActionResult AtoE()
        {
            var vendorList = _apContext.Vendors.OrderBy(n => n.VendorName).Where(a => a.IsDeleted == false).ToList();

            List<Vendor> newList = new();

            foreach (var v in vendorList)
            {
                if (char.Parse(v.VendorName.Substring(0, 1).ToLower()) >= 'a')
                    if (char.Parse(v.VendorName.Substring(0, 1).ToLower()) <= 'e')
                        newList.Add(v);
            }
            HttpContext.Session.SetString("selectedTab", "AtoE");

            return View("AtoE", newList);
        }

        public IActionResult FtoK()
        {
            var vendorList = _apContext.Vendors.OrderBy(n => n.VendorName).Where(a => a.IsDeleted == false).ToList();

            List<Vendor> newList = new();

            foreach (var v in vendorList)
            {
                if (char.Parse(v.VendorName.Substring(0, 1).ToLower()) >= 'f')
                    if (char.Parse(v.VendorName.Substring(0, 1).ToLower()) <= 'k')
                        newList.Add(v);
            }
            HttpContext.Session.SetString("selectedTab", "FtoK");

            return View("AtoE", newList);
        }

        public IActionResult LtoR()
        {
            var vendorList = _apContext.Vendors.OrderBy(n => n.VendorName).Where(a => a.IsDeleted == false).ToList();

            List<Vendor> newList = new();

            foreach (var v in vendorList)
            {
                if (char.Parse(v.VendorName.Substring(0, 1).ToLower()) >= 'l')
                    if (char.Parse(v.VendorName.Substring(0, 1).ToLower()) <= 'r')
                        newList.Add(v);
            }
            HttpContext.Session.SetString("selectedTab", "LtoR");

            return View("AtoE", newList);
        }

        public IActionResult StoZ()
        {
            var vendorList = _apContext.Vendors.OrderBy(n => n.VendorName).Where(a => a.IsDeleted == false).ToList();

            List<Vendor> newList = new();

            foreach (var v in vendorList)
            {
                if (char.Parse(v.VendorName.Substring(0, 1).ToLower()) >= 's')
                    if (char.Parse(v.VendorName.Substring(0, 1).ToLower()) <= 'z')
                        newList.Add(v);
            }
            HttpContext.Session.SetString("selectedTab", "StoZ");

            return View("AtoE", newList);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
