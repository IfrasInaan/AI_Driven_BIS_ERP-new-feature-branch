using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_App.Models
{
    public partial class SupplierReturnInvoiceDetailMV
    {
        public int SupplierReturnInvoiceDetailID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int SupplierReturnInvoiceID { get; set; }
        public int SupplierInvoiceDetailID { get; set; }
        public int ProductID { get; set; }
        public int PurchaseReturnQuantity { get; set; }
        public double PurchaseReturnUnitPrice { get; set; }
    }
}
