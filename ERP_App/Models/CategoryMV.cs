
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_App.Models
{
    public partial class CategoryMV
    {
       
    
        public int CategoryID { get; set; }
        [Display(Name = "Category")]
        public string categoryName { get; set; }
        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        [Display(Name ="Create By")]
        public string CreatedBy { get; set; }
    
        
    }
}
