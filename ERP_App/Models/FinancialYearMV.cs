using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_App.Models
{
    public partial class FinancialYearMV
    {
            
        public int FinancialYearID { get; set; }
        [Display(Name ="Financial Year")]
        public string FinancialYear { get; set; }
        [Display(Name ="Start Date")]
        public System.DateTime StartDate { get; set; }
        [Display(Name ="End Date")]
        public System.DateTime EndDate { get; set; }
        [Display(Name ="Status")]
        public bool IsActive { get; set; }
        public int UserID { get; set; }
        [Display(Name ="Created By")]
        public string CreatedBy { get; set; }

    }
}
