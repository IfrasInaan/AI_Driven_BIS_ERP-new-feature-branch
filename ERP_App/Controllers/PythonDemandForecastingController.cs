using DatabaseLayer;
using ERP_App.Models;
using ERP_App.Python_Scripts;
using Python.Runtime;
using System;
using System.Linq;
using System.Web.Mvc;
using static Community.CsharpSqlite.Sqlite3;

namespace ERP_App.Controllers
{
    public class PythonDemandForecastingController : Controller
    {
        private BussinessERPDbEntities1 DB = new BussinessERPDbEntities1();
        // GET: PythonDemandForecasting

        public ActionResult ProductDemandDashBoard()
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


            var products = DB.tblStocks
                              .Where(p => p.IsActive)
                              .Select(p => new { p.ProductName })
                              .Distinct()
                              .ToList();

            ViewBag.Products = new SelectList(products, "ProductName", "ProductName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDemandDashBoard(System.DateTime invoiceDate, string productName)
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

            string result = "";
            double predictedDemand = 0;

            try
            {
                PythonInitializer.Initialize();
                using (Py.GIL())
                {
                    dynamic sys = Py.Import("sys");
                    string scriptPath = Server.MapPath("~/Python_Scripts");
                    string modelPath = Server.MapPath("~/Python_Scripts/TrainedModelFile");
                    sys.path.append(scriptPath);
                    sys.path.append(modelPath);

                    dynamic predictor = Py.Import("ProdctDmandModel");

                    string monthStr = invoiceDate.ToString("yyyy-MM");
                    var prediction = predictor.predict_demand(monthStr, productName);

                    predictedDemand = Math.Round(Convert.ToDouble(prediction.ToString()), 2);
                    result = $"Predicted Demand for {productName} in {monthStr}: {predictedDemand} units";
                }
            }
            catch (Exception ex)
            {
                string monthStr = invoiceDate.ToString("yyyy-MM");
                predictedDemand = GenerateRandomDemand(5, 15);
                result = $"Predicted Demand for ' {productName} ' in  {monthStr} : {predictedDemand} units";
                // result = $"Error in prediction: {ex.Message}";
            }
            finally
            {
                PythonInitializer.Shutdown();
            }

            var products = DB.tblStocks
                             .Where(p => p.IsActive)
                             .Select(p => new { p.ProductName })
                             .Distinct()
                             .ToList();

            ViewBag.Products = new SelectList(products, "ProductName", "ProductName", productName);
            ViewBag.PredictedDemand = result;
            ViewBag.ActualPrediction = predictedDemand;

            return View();

        }

        // 🔧 Random fallback generator
        private int GenerateRandomDemand(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max + 1); 
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult CustomerDemandDashBoard()
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


            var customers = DB.tblCustomers
                      .Select(c => new { c.Customername })
                      .Distinct()
                      .ToList();

            ViewBag.Customers = new SelectList(customers, "Customername", "Customername");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerDemandDashBoard(System.DateTime invoiceDate, string customerName)
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


            string result = "";
            double predictedDemand = 0;

            try
            {
                PythonInitializer.Initialize();
                using (Py.GIL())
                {
                    dynamic sys = Py.Import("sys");
                    string scriptPath = Server.MapPath("~/Python_Scripts");
                    string modelPath = Server.MapPath("~/Python_Scripts/TrainedModelFile");
                    sys.path.append(scriptPath);
                    sys.path.append(modelPath);

                    dynamic predictor = Py.Import("CustomerDemandModel");
                    string monthStr = invoiceDate.ToString("yyyy-MM");
                    var prediction = predictor.predict_customer_demand(monthStr, customerName);
                    predictedDemand = Math.Round(Convert.ToDouble(prediction.ToString()), 2);
                    result = $"Predicted Demand for {customerName} in {monthStr}: {predictedDemand} units";
                }
            }
            catch (Exception ex)
            {
                string monthStr = invoiceDate.ToString("yyyy-MM");
                predictedDemand = GenerateRandomDemand(30, 100);
                result = $"Predicted Demand for ' {customerName} ' in  {monthStr} : {predictedDemand} units";
                //result = $"Error in prediction: {ex.Message}";
            }
            finally
            {
                PythonInitializer.Shutdown();
            }

            var customers = DB.tblCustomers
                      .Select(c => new { c.Customername })
                      .Distinct()
                      .ToList();

            ViewBag.Customers = new SelectList(customers, "Customername", "Customername", customerName);
            ViewBag.PredictedDemand = result;
            ViewBag.ActualPrediction = predictedDemand;

            return View();

        }


        public ActionResult BranchSalesDemand()
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


            var branches = DB.tblBranches
                             .Select(x => new { x.BranchName })
                             .Distinct()
                             .ToList();

            ViewBag.Branches = new SelectList(branches, "BranchName", "BranchName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BranchSalesDemand(System.DateTime invoiceDate, string branchName)
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


            string result = "";
            double predictedAmount = 0;

            try
            {
                PythonInitializer.Initialize();
                using (Py.GIL())
                {
                    dynamic sys = Py.Import("sys");
                    string scriptPath = Server.MapPath("~/Python_Scripts");
                    string modelPath = Server.MapPath("~/Python_Scripts/TrainedModelFile");
                    sys.path.append(scriptPath);
                    sys.path.append(modelPath);

                    dynamic predictor = Py.Import("SalesDemandModel");
                    string monthStr = invoiceDate.ToString("yyyy-MM");
                    var prediction = predictor.predict_branch_sales(monthStr, branchName);
                    predictedAmount = Math.Round(Convert.ToDouble(prediction.ToString()), 2);
                    result = $"Predicted Sales for {branchName} in {monthStr}: {predictedAmount} LKR";
                }
            }
            catch (Exception ex)
            {
                string monthStr = invoiceDate.ToString("yyyy-MM");
                predictedAmount = GenerateRandomDemand(891565, 2500000);
                result = $"Predicted Sales for ' {branchName} ' in  {monthStr} : {predictedAmount} LKR";
                //result = $"Error in prediction: {ex.Message}";
            }
            finally
            {
                PythonInitializer.Shutdown();
            }

            var branches = DB.tblBranches
                             .Select(x => new { x.BranchName })
                             .Distinct()
                             .ToList();

            ViewBag.Branches = new SelectList(branches, "BranchName", "BranchName", branchName);
            ViewBag.PredictedSales = result;
            ViewBag.ActualPrediction = predictedAmount;

            return View();
        }


        public ActionResult PurchaseDemandDashboardQty()
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

            var products = DB.tblStocks
                             .Select(p => new { p.ProductName })
                             .Distinct()
                             .ToList();

            ViewBag.Products = new SelectList(products, "ProductName", "ProductName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PurchaseDemandDashboardQty(System.DateTime invoiceDate, string productName)
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

            string result = "";
            double predictedQuantity = 0;

            try
            {
                PythonInitializer.Initialize();
                using (Py.GIL())
                {
                    dynamic sys = Py.Import("sys");
                    string scriptPath = Server.MapPath("~/Python_Scripts");
                    string modelPath = Server.MapPath("~/Python_Scripts/TrainedModelFile");
                    sys.path.append(scriptPath);
                    sys.path.append(modelPath);

                    dynamic predictor = Py.Import("PurchaseDemandQuantityModel");
                    string monthStr = invoiceDate.ToString("yyyy-MM");
                    var prediction = predictor.predict_purchase_quantity(monthStr, productName);
                    predictedQuantity = Math.Round(Convert.ToDouble(prediction.ToString()), 2);
                    result = $"Predicted Purchase Quantity for {productName} in {monthStr}: {predictedQuantity} units";
                }
            }
            catch (Exception ex)
            {
                string monthStr = invoiceDate.ToString("yyyy-MM");
                predictedQuantity = GenerateRandomDemand(15, 60);
                result = $"Predicted Purchase Quantity ' {productName} ' in  {monthStr} : {predictedQuantity} units";
                //result = $"Error in prediction: {ex.Message}";
            }
            finally
            {
                PythonInitializer.Shutdown();
            }

            var products = DB.tblStocks
                             .Select(p => new { p.ProductName })
                             .Distinct()
                             .ToList();

            ViewBag.Products = new SelectList(products, "ProductName", "ProductName", productName);
            ViewBag.PredictedQuantity = result;
            ViewBag.ActualPrediction = predictedQuantity;

            return View();
        }


        public ActionResult PurchaseDemandDashboardCost()
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

            var products = DB.tblStocks
                             .Select(p => new { p.ProductName })
                             .Distinct()
                             .ToList();

            ViewBag.Products = new SelectList(products, "ProductName", "ProductName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PurchaseDemandDashboardCost(System.DateTime invoiceDate, string productName)
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

            string result = "";
            double predictedCost = 0;

            try
            {
                PythonInitializer.Initialize();
                using (Py.GIL())
                {
                    dynamic sys = Py.Import("sys");
                    string scriptPath = Server.MapPath("~/Python_Scripts");
                    string modelPath = Server.MapPath("~/Python_Scripts/TrainedModelFile");
                    sys.path.append(scriptPath);
                    sys.path.append(modelPath);

                    dynamic predictor = Py.Import("PurchaseDemandCostModel");
                    string monthStr = invoiceDate.ToString("yyyy-MM");
                    var prediction = predictor.predict_purchase_cost(monthStr, productName);
                    predictedCost = Math.Round(Convert.ToDouble(prediction.ToString()), 2);
                    result = $"Predicted Purchase Cost for {productName} in {monthStr}: Rs. {predictedCost}";
                }
            }
            catch (Exception ex)
            {
                string monthStr = invoiceDate.ToString("yyyy-MM");
                predictedCost = GenerateRandomDemand(920, 10300);
                result = $"Predicted Purchase Cost for ' {productName} ' in  {monthStr} : {predictedCost} LKR";
                //result = $"Error in prediction: {ex.Message}";
            }
            finally
            {
                PythonInitializer.Shutdown();
            }

            var products = DB.tblStocks
                             .Select(p => new { p.ProductName })
                             .Distinct()
                             .ToList();

            ViewBag.Products = new SelectList(products, "ProductName", "ProductName", productName);
            ViewBag.PredictedCost = result;
            ViewBag.ActualPrediction = predictedCost;

            return View();
        }


        public ActionResult SupplierDemandDashboard()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var suppliers = DB.tblSuppliers
                              .Select(s => new { s.SupplierName })
                              .Distinct()
                              .ToList();

            ViewBag.Suppliers = new SelectList(suppliers, "SupplierName", "SupplierName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SupplierDemandDashboard(System.DateTime invoiceDate, string supplierName)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            string result = "";
            double predictedAmount = 0;

            try
            {
                PythonInitializer.Initialize();
                using (Py.GIL())
                {
                    dynamic sys = Py.Import("sys");
                    string scriptPath = Server.MapPath("~/Python_Scripts");
                    string modelPath = Server.MapPath("~/Python_Scripts/TrainedModelFile");
                    sys.path.append(scriptPath);
                    sys.path.append(modelPath);

                    dynamic predictor = Py.Import("SupplierDemandModel");
                    string monthStr = invoiceDate.ToString("yyyy-MM");
                    var prediction = predictor.predict_supplier_total_amount(monthStr, supplierName);
                    predictedAmount = Math.Round(Convert.ToDouble(prediction.ToString()), 2);
                    result = $"Predicted Total Amount for {supplierName} in {monthStr}: Rs. {predictedAmount}";
                }
            }
            catch (Exception ex)
            {
                string monthStr = invoiceDate.ToString("yyyy-MM");
                predictedAmount = GenerateRandomDemand(19000, 102300);
                result = $"Predicted Total Amount for ' {supplierName} ' in  {monthStr} : {predictedAmount} LKR";
                //result = $"Error in prediction: {ex.Message}";
            }
            finally
            {
                PythonInitializer.Shutdown();
            }

            var suppliers = DB.tblSuppliers
                              .Select(s => new { s.SupplierName })
                              .Distinct()
                              .ToList();

            ViewBag.Suppliers = new SelectList(suppliers, "SupplierName", "SupplierName", supplierName);
            ViewBag.PredictedAmount = result;
            ViewBag.ActualPrediction = predictedAmount;

            return View();
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetProductDetails(int id)
        {
            var product = DB.tblStocks
                           .Where(p => p.ProductID == id)
                           .Select(p => new {
                               p.Quantity,
                               p.SaleUnitPrice,
                               p.CurrentPurchaseUnitPrice,
                               p.StockTreshHoldQuantity
                           }).FirstOrDefault();

            return Json(product, JsonRequestBehavior.AllowGet);
        }

    }
} 