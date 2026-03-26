using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_App.Models
{
    public class StockMV2
    {
        [Key] // Add this to satisfy view generation
        public int ProductID { get; set; }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "*Required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int Quantity { get; set; }

        [Display(Name = "Sale Unit Price")]
        [Required(ErrorMessage = "*Required")]
        public double SaleUnitPrice { get; set; }

        [Display(Name = "Purchase Unit Price")]
        [Required(ErrorMessage = "*Required")]
        public double CurrentPurchaseUnitPrice { get; set; }

        [Display(Name = "Expiry Date")]
        [Required(ErrorMessage = "*Required")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "Manufacture Date")]
        [Required(ErrorMessage = "*Required")]
        [DataType(DataType.Date)]
        public DateTime ManufactureDate { get; set; }

        [Display(Name = "Threshold Quantity")]
        [Required(ErrorMessage = "*Required")]
        public int StockTreshHoldQuantity { get; set; }

        public string Description { get; set; }
        public int UserID { get; set; }

        [Display(Name = "Created By")]
        public string CreateBy { get; set; }

        public bool IsActive { get; set; }
    }
}