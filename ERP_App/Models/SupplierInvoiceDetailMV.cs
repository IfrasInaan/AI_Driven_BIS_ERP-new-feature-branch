using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc.Routing.Constraints;

namespace ERP_App.Models
{
    public partial class SupplierInvoiceDetailMV
    {
        public int SupplierInvoiceDetailID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int PurchaseQuantity { get; set; }
        public double purchaseUnitPrice { get; set; }
        public double previouspurchaseunitprice { get; set; }
        public System.DateTime manfacturedate { get; set; }
        public System.DateTime expirydate { get; set; }
        public double ItemCost { get; set; }
    }
}
