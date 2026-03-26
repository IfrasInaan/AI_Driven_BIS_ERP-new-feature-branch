using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class BranchDashboardController : Controller
    {
        private BussinessERPDbEntities1 DB = new BussinessERPDbEntities1();
        // GET: BranchDashboard
        public ActionResult BranchDash()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            var totalSuppliers = DB.tblSuppliers.Count();
            var totalProducts = DB.tblStocks.Count();
            var totalPurchaseOrders = DB.tblSupplierInvoices.Count();
            var totalCustomerInvoices = DB.tblCustomerInvoices.Count();
            
            var totalRevenue = DB.tblCustomerInvoices
            .AsEnumerable()
            .Sum(ci => (decimal?)ci.TotalAmount) ?? 0;

            ViewBag.TotalSuppliers = totalSuppliers;
            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalPurchaseOrders = totalPurchaseOrders;
            ViewBag.TotalCustomerInvoices = totalCustomerInvoices;
            ViewBag.TotalRevenue = totalRevenue;

            return View();
        }
    }
}