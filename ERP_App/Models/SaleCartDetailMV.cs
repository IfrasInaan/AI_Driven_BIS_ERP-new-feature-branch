using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP_App.Models
{
    public partial class SaleCartDetailMV
    {
        public int SaleCartDetailID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SaleQuantity { get; set; }
        public double SaleUnitPrice { get; set; }
        public double Total {  get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public int UserID { get; set; }


    }
}
