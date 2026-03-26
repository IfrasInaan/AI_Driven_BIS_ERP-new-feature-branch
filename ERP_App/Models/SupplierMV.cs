using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_App.Models
{
    public partial class SupplierMV
    {
    
        public int SupplierID { get; set; }
        [Display(Name ="Supplier")]
        [Required(ErrorMessage ="Required*")]
        public string SupplierName { get; set; }
        [Required(ErrorMessage = "Required*")]  
        [Display(Name = "Contact No")]
        public string SupplierConatctNo { get; set; }
        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Address")]
        public string SupplierAddress { get; set; }
        [Display(Name ="Email")]
        public string SupplierEmail { get; set; }
        public string Discription { get; set; }
        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public string CreatedBy { get; set; }
    }
}
