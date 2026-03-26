
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_App.Models
{
    public partial class CustomerInvoiceMV
    {
        [Key]
        public int CustomerInvoiceID { get; set; }
        public int CustomerID { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public int InvoiceNo { get; set; }
        public string Title { get; set; }
        public double TotalAmount { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }

        // Add missing properties for the SaleCart view
        public int ProductID { get; set; } // Added
        public int SaleQuantity { get; set; } // Added
        public double SaleUnitPrice { get; set; } // Added

        // Order summary for the cart
        public CustomerPaymentMV OrderSummary { get; set; } // Added

        // List of sale items in the cart
        public List<SaleCartDetailMV> SaleItems { get; set; } // Added
    }
}
