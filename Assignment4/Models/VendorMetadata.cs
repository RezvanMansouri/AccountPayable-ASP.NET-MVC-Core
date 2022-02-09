using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Assignment4.Models
{
    //our metadata vendor class to change some of the validation form our database
    public class VendorMetadata
    {
        [RegularExpression("^(?i)[ a-z0-9]+$", ErrorMessage = "Name cannot contain special characters.")]
        public string VendorName { get; set; }

        [Required(ErrorMessage = "Please enter an Address")]
        public string VendorAddress1 { get; set; }

        [Required]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Enter a correct phone number in a (000) 000-8765")]
        [Remote("CheckPhone", "Validation")]
        public string VendorPhone { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Enter a state in a valid 2 letter state code ")]
        public string VendorState { get; set; }

        [Required]
        [RegularExpression(@"^\d\d\d\d\d$", ErrorMessage = "Enter a correct Zip code in 00000")]
        public string VendorZipCode { get; set; }

        [Required]
        public int DefaultTermsId { get; set; }

        [Required]
        public int DefaultAccountNumber { get; set; }
    }
}
