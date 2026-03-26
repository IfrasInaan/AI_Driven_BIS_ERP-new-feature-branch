using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_App.Models
{
    public partial class CustomerInvoiceDetailMV
    {        
        public int CustomerInvoiceDetailID { get; set; }
        public int CustomerInvoiceID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SaleQuantity { get; set; }
        public double SaleUnitPrice { get; set; }
        public double ItemCost { get; set; }


    }
}
