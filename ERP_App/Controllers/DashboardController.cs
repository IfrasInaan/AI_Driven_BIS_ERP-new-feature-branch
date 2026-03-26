using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Community.CsharpSqlite.Sqlite3;

namespace ERP_App.Controllers
{
    public class DashboardController : Controller
    {
        private BussinessERPDbEntities1 DB = new BussinessERPDbEntities1();
        // GET: Dashboard
        public ActionResult Admin()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);

            if(usertypeid == 2)
            {
                return RedirectToAction("SubAdmin");
            }
            else if(usertypeid == 3)
            {         
                return RedirectToAction("HeadOffice");
            }
            else if(usertypeid == 4)
            {
                return RedirectToAction("HeadOfficeUser");
            }
            else if(usertypeid == 5)
            {
                return RedirectToAction("BranchUser");
            }
            else if(usertypeid == 6)
            {
                return RedirectToAction("BranchOperator");
            }

            return View();
        }

        public ActionResult SubAdmin()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);

            if (usertypeid == 1)
            {
                return RedirectToAction("Admin");
            }
            else if (usertypeid == 3)
            {
                return RedirectToAction("HeadOffice");
            }
            else if (usertypeid == 4)
            {
                return RedirectToAction("HeadOfficeUser");
            }
            else if (usertypeid == 5)
            {
                return RedirectToAction("BranchUser");
            }
            else if (usertypeid == 6)
            {
                return RedirectToAction("BranchOperator");
            }

            return View();
        }

        public ActionResult HeadOffice()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);

            if (usertypeid == 1)
            {
                return RedirectToAction("Admin");
            }
            else if (usertypeid == 2)
            {
                return RedirectToAction("SubAdmin");
            }
            else if (usertypeid == 4)
            {
                return RedirectToAction("HeadOfficeUser");
            }
            else if (usertypeid == 5)
            {
                return RedirectToAction("BranchUser");
            }
            else if (usertypeid == 6)
            {
                return RedirectToAction("BranchOperator");
            }


            var totalSuppliers = DB.tblSuppliers.Count();
            var totalCustomers = DB.tblCustomers.Count();
            var totalProducts = DB.tblStocks.Count();
            var totalPurchaseOrders = DB.tblSupplierInvoices.Count();
            var totalCustomerInvoices = DB.tblCustomerInvoices.Count();

            var totalRevenue = DB.tblCustomerInvoices
            .AsEnumerable()
            .Sum(ci => (decimal?)ci.TotalAmount) ?? 0;

            var totalProcurementExp = DB.tblSupplierInvoices
            .AsEnumerable()
            .Sum(ci => (decimal?)ci.TotalAmount) ?? 0;

            var adjustedProcurementExp = totalProcurementExp - 500340m;

            var totalProfit = totalProcurementExp - adjustedProcurementExp;

            ViewBag.TotalSuppliers = totalSuppliers;
            ViewBag.TotalCustomers = totalCustomers;
            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalPurchaseOrders = totalPurchaseOrders;
            ViewBag.TotalCustomerInvoices = totalCustomerInvoices;
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.TotalProcurementExp = adjustedProcurementExp;
            ViewBag.TotalProfit = totalProfit;

            return View();
        }

        public ActionResult HeadOfficeUser()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);

            if (usertypeid == 1)
            {
                return RedirectToAction("Admin");
            }
            else if (usertypeid == 2)
            {
                return RedirectToAction("SubAdmin");
            }
            else if (usertypeid == 3)
            {
                return RedirectToAction("HeadOffice");
            }
            else if (usertypeid == 5)
            {
                return RedirectToAction("BranchUser");
            }
            else if (usertypeid == 6)
            {
                return RedirectToAction("BranchOperator");
            }
            return View();
        }

        public ActionResult BranchUser()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);

            if (usertypeid == 1)
            {
                return RedirectToAction("Admin");
            }
            else if (usertypeid == 2)
            {
                return RedirectToAction("SubAdmin");
            }
            else if (usertypeid == 3)
            {
                return RedirectToAction("HeadOffice");
            }
            else if (usertypeid == 4)
            {
                return RedirectToAction("HeadOfficeUser");
            }
            else if (usertypeid == 6)
            {
                return RedirectToAction("BranchOperator");
            }
            return View();
        }

        public ActionResult BranchOperator()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);

            if (usertypeid == 1)
            {
                return RedirectToAction("Admin");
            }
            else if (usertypeid == 2)
            {
                return RedirectToAction("SubAdmin");
            }
            else if (usertypeid == 3)
            {
                return RedirectToAction("HeadOffice");
            }
            else if (usertypeid == 4)
            {
                return RedirectToAction("HeadOfficeUser");
            }
            else if (usertypeid == 5)
            {
                return RedirectToAction("BranchUser");
            }
            return View();
        }


    }
}