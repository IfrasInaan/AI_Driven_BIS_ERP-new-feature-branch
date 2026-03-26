using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_App.Models
{
    public partial class CustomerReturnInvoiceDetailMV
    {
        public int CustomerReturnInvoiceDetailID { get; set; }
        public int CustomerInvoiceDetailID { get; set; }
        public int CustomerInvoiceID { get; set; }
        public int CustomerReturnInvoiceID { get; set; }
        public int ProductID { get; set; }
        public int SaleReturnQuantity { get; set; }
        public double SaleReturnUnitPrice { get; set; }
    
       
    }
}
