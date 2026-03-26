
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_App.Models
{  
    public partial class CountryMV
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Keyword { get; set; }
        public int UserID { get; set; }
    }
}
