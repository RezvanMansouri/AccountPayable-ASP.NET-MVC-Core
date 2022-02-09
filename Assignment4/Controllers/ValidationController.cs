using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment4.Models;


namespace Assignment4.Controllers
{
    public class ValidationController : Controller
    {
        private apContext _apContext;
        public ValidationController(apContext apx) => _apContext = apx;


        public IActionResult CheckPhone(string vendorPhone)
        {
            string msg = CheckIfPhoneisUnique(vendorPhone);
            if (string.IsNullOrEmpty(msg))
            {
                // not in use so...
                TempData["okPhone"] = true;
                return Json(true);
            }
            else
            {
                // otherwise email is in use so simply return an err msg:
                return Json(msg);
            }
        }
        private string CheckIfPhoneisUnique(string phone)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(phone))
            {
                var customer = _apContext.Vendors.Where(c => c.VendorPhone == phone).FirstOrDefault();
                if (customer != null)
                {
                    msg = $"The Phone Number {phone} is already in use.";
                }
            }
            return msg;
        }
    }
}
