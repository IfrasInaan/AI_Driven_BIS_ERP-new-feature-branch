using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_App.Models
{
    public partial class CustomerMV
    {
    
        public int CustomerID { get; set; }
        [Display(Name ="Customer")]
        public string Customername { get; set; }
        [Display(Name ="Contact No")]
        public string CustomerContact { get; set; }
        [Display(Name ="Area")]
        public string CustomerArea { get; set; }
        [Display(Name ="Address")]
        public string CustomerAddress { get; set; }
        public string Description { get; set; }
        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public int UserID { get; set; }

        public string CreatedBy { get; set; }
    }
}
