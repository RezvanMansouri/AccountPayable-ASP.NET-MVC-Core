using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment4.Models.DbGenerated;

namespace Assignment4.Models
{
    public class InvoicesByView
    {
        public List<Invoice> Invoices { get; set; }
        public InvoiceLineItem invoiceLineItem { get; set; }
        public Vendor vendor { get; set; }
        public int term { get; set; }

      //  public Invoice activeInvoice { get; set; }


    }
}
