using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_App.Models
{
    public partial class StockMV
    {        
    
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        [Display(Name ="Product")]
        [Required(ErrorMessage ="*Required")]
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
        public System.DateTime ExpiryDate { get; set; }
        [Display(Name = "Manufacture Date")]
        [Required(ErrorMessage = "*Required")]
        [DataType(DataType.Date)]
        public System.DateTime ManufactureDate { get; set; }
        [Display(Name = "Treshhold Quantity")]
        [Required(ErrorMessage = "*Required")]
        public int StockTreshHoldQuantity { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Create By")]
        public string CreateBy { get; set; }
        public bool IsActive { get; set; }
    
       
    }
}
