

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_App.Models
{
    public partial class BranchTypeMV
    {


        public int BranchTypeID { get; set; }
        [Required(ErrorMessage = "Required*")]
        [Display(Name = "Branch Type")]
        public string BranchType { get; set; }


    }
}
