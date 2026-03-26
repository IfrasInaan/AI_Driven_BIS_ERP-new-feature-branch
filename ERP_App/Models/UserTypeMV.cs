using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_App.Models
{
    public partial class UserTypeMV
    {       
        public int UserTypeID { get; set; }
        [Required(ErrorMessage ="Required*")]
        [Display(Name = "User Type")]
        public string UserType { get; set; }
    
    }
}
