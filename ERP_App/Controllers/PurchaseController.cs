using DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class PurchaseController : Controller
    {
        private BussinessERPDbEntities1 DB = new BussinessERPDbEntities1();
        // GET: Purchase
        public ActionResult PurchaseStockProducts()
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
            var stock = DB.tblPurchaseCartDetails.Where(p => p.CompanyID == companyid && p.BranchID == branchid && p.UserID == userid).ToList();
            var purchaseitems = new PurchaseCartMV();
            var list = new List<PurchaseItemsMV>();

            double subTotal = 0;
            foreach (var product in stock)
            {
                var item = new PurchaseItemsMV();
                //item.BranchID = product.BranchID;
                // item.CategoryID = product.tblStock.tblCategory.CategoryID;
                //item.CompanyID = product.CompanyID;
                item.CreateBy = product.tblUser.UserName;
                item.PreviousPurchaseUnitPrice = product.PreviousPurchaseUnitPrice;
                item.CurrentPurchaseUnitPrice = product.purchaseUnitPrice;
                item.Description = product.Description;
                item.ExpiryDate = product.ExpiryDate;
                item.ManufactureDate = product.ManufactureDate;
                //item.IsActive = product.IsActive;
                item.ProductID = product.ProductID;
                var getproduct = DB.tblStocks.Find(product.ProductID);
                item.ProductName = getproduct != null ? getproduct.ProductName : " ?";
                item.Quantity = product.PurchaseQuantity;
                item.SaleUnitPrice = (double)product.SaleUnitPrice;
                item.PurchaseCartDetailID = product.PurchaseCartDetailID;

                item.UserID = product.UserID;
                item.CategoryName = product.tblStock.tblCategory.categoryName;

                list.Add(item);
                subTotal = subTotal + (double)product.purchaseUnitPrice * product.PurchaseQuantity;
            }



            purchaseitems.PurchaseItemsList = list;
            purchaseitems.OrderSummary = new PurchaseCartSummaryMV() { SubTotal = subTotal };
            ViewBag.ProductID = new SelectList(DB.tblStocks.Where(s => s.BranchID == branchid && s.CompanyID == companyid).ToList(), "ProductID", "ProductName", "0");
            ViewBag.SupplierID = new SelectList(DB.tblSuppliers.Where(s => s.BranchID == branchid && s.CompanyID == companyid).ToList(), "SupplierID", "SupplierName", "0");


            return View(purchaseitems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PurchaseStockProducts(PurchaseCartMV purchaseCartMV)
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

            if (ModelState.IsValid)
            {
                var checkproductincart = DB.tblPurchaseCartDetails.Where(i => i.ProductID == purchaseCartMV.ProductID && i.CompanyID == companyid && i.BranchID == branchid && i.UserID == userid).FirstOrDefault();
                if (checkproductincart == null)
                {
                    var item = new tblPurchaseCartDetail();
                    item.BranchID = branchid;
                    item.CompanyID = companyid;
                    item.ProductID = purchaseCartMV.ProductID;
                    item.PurchaseQuantity = purchaseCartMV.PurchaseQuantity;
                    item.purchaseUnitPrice = purchaseCartMV.CurrentPurchaseUnitPrice;
                    item.SaleUnitPrice = purchaseCartMV.SaleUnitPrice;
                    item.PreviousPurchaseUnitPrice = purchaseCartMV.PreviousPurchaseUnitPrice;
                    item.ManufactureDate = purchaseCartMV.ManufactureDate;
                    item.ExpiryDate = purchaseCartMV.ExpiryDate;
                    item.Description = purchaseCartMV.Description;
                    item.UserID = userid;
                    DB.tblPurchaseCartDetails.Add(item);
                    DB.SaveChanges();


                    purchaseCartMV.ProductID = 0;
                    purchaseCartMV.PurchaseQuantity = 0;
                    purchaseCartMV.CurrentPurchaseUnitPrice = 0;
                    purchaseCartMV.SaleUnitPrice = 0;
                    purchaseCartMV.PreviousPurchaseUnitPrice = 0;
                    purchaseCartMV.ManufactureDate = DateTime.Now;
                    purchaseCartMV.ExpiryDate = DateTime.Now;
                    purchaseCartMV.Description = string.Empty;

                }
                else
                {
                    ModelState.AddModelError("ProductID", "Already in Purchase Cart");

                }
            }
            var stock = DB.tblPurchaseCartDetails.Where(p => p.CompanyID == companyid && p.BranchID == branchid && p.UserID == userid).ToList();
            var purchaseitems = new PurchaseCartMV();
            var list = new List<PurchaseItemsMV>();

            foreach (var product in stock)
            {
                var item = new PurchaseItemsMV();
                item.CreateBy = product.tblUser.UserName;
                item.PreviousPurchaseUnitPrice = product.PreviousPurchaseUnitPrice;
                item.CurrentPurchaseUnitPrice = product.purchaseUnitPrice;
                item.Description = product.Description;
                item.ExpiryDate = product.ExpiryDate;
                item.ManufactureDate = product.ManufactureDate;
                //item.IsActive = product.IsActive;
                item.ProductID = product.ProductID;
                var getproduct = DB.tblStocks.Find(product.ProductID);
                item.ProductName = getproduct != null ? getproduct.ProductName : " ?";
                item.Quantity = product.PurchaseQuantity;
                item.SaleUnitPrice = (double)product.SaleUnitPrice;
                item.PurchaseCartDetailID = product.PurchaseCartDetailID;

                item.UserID = product.UserID;
                item.CategoryName = product.tblStock.tblCategory.categoryName;

                list.Add(item);
            }
            purchaseCartMV.PurchaseItemsList = list;

            ViewBag.ProductID = new SelectList(DB.tblStocks.Where(s => s.BranchID == branchid && s.CompanyID == companyid).ToList(), "ProductID", "ProductName", purchaseCartMV.ProductID);
            ViewBag.SupplierID = new SelectList(DB.tblSuppliers.Where(s => s.BranchID == branchid && s.CompanyID == companyid).ToList(), "SupplierID", "SupplierName", purchaseCartMV.SupplierID);

            return View(purchaseCartMV);
        }


        public ActionResult DeletePurchaseCartItem(int? id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var pitem = DB.tblPurchaseCartDetails.Find(id);
            DB.Entry(pitem).State = System.Data.Entity.EntityState.Deleted;
            DB.SaveChanges();
            return RedirectToAction("PurchaseStockProducts");
        }

        public ActionResult CheckoutPurchase(int? supplierid,
            bool ispaymentispaid,
            float? estimatedtax,
            float? shippingfee,
            float subtotal)
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

            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var datetime = DateTime.Now;
                    var supplier = DB.tblSuppliers.Find(supplierid);
                    if (supplier == null)
                    {
                        ModelState.AddModelError("SupplierID", "Please Select Supplier!");
                        transaction.Rollback();
                    }
                    float totalamount = (float)subtotal + (float)estimatedtax + (float)shippingfee;
                    string invoiceno = "PUR" + datetime.ToString("yyyymmss") + userid;
                    var invoiceheader = new tblSupplierInvoice();

                    invoiceheader.SupplierID = (int)supplierid;
                    invoiceheader.CompanyID = companyid;
                    invoiceheader.BranchID = branchid;
                    invoiceheader.InvoiceNo = invoiceno;
                    invoiceheader.TotalAmount = totalamount;
                    invoiceheader.InvoiceDate = datetime;
                    invoiceheader.Description = " ";
                    invoiceheader.UserID = userid;
                    invoiceheader.subtotalamount = subtotal;
                    invoiceheader.shippingfee = (float)estimatedtax;
                    invoiceheader.estimatedtax = (float)shippingfee;
                    DB.tblSupplierInvoices.Add(invoiceheader);
                    DB.SaveChanges();

                    var purchasestock = DB.tblPurchaseCartDetails.Where(p => p.CompanyID == companyid && p.BranchID == branchid && p.UserID == userid).ToList();

                    foreach (var product in purchasestock)
                    {
                        var purchaseitem = new tblSupplierInvoiceDetail();
                        purchaseitem.SupplierInvoiceID = invoiceheader.SupplierID;
                        purchaseitem.ProductID = product.ProductID;
                        purchaseitem.PurchaseQuantity = product.PurchaseQuantity;
                        purchaseitem.purchaseUnitPrice = product.purchaseUnitPrice;
                        purchaseitem.previouspurchaseunitprice = product.PreviousPurchaseUnitPrice;
                        purchaseitem.manfacturedate = (DateTime)product.ManufactureDate;
                        purchaseitem.expirydate = (DateTime)product.ExpiryDate;
                        DB.tblSupplierInvoiceDetails.Add(purchaseitem);
                        DB.SaveChanges();

                        var stockproduct = DB.tblStocks.Find(product.ProductID);
                        stockproduct.ManufactureDate = (DateTime)product.ManufactureDate;
                        stockproduct.ExpiryDate = (DateTime)product.ExpiryDate;
                        stockproduct.Quantity = stockproduct.Quantity + product.PurchaseQuantity;
                        stockproduct.CurrentPurchaseUnitPrice = product.purchaseUnitPrice;
                        stockproduct.SaleUnitPrice = (double)product.SaleUnitPrice;
                        DB.Entry(stockproduct).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();

                    }

                    // Purchase Products Debit Transaction : Purchase Activity
                    int FinancialYearID = 0;
                    var financial = DB.tblFinancialYears.Where(s => s.IsActive == true).FirstOrDefault();
                    if (financial == null)
                    {
                        ModelState.AddModelError("ProductID", "Financial Year is Not Set!");
                        transaction.Rollback();
                    }
                    FinancialYearID = financial.FinancialYearID;

                    int AccountHeadID = 0;
                    int AccountControlID = 0;
                    int AccountSubControlID = 0;

                    var debitentry = DB.tblAccountSettings.Where(s => s.AccountActivityID == 1 && s.CompanyID == companyid && s.BranchID == branchid).FirstOrDefault();
                    if (debitentry == null)
                    {
                        ModelState.AddModelError("ProductID", "First Set Account Flow!");
                        transaction.Rollback();
                    }
                    AccountHeadID = debitentry.AccountHeadID;
                    AccountControlID = debitentry.AccountControlID;
                    AccountSubControlID = debitentry.AccountSubControlID;

                    var setdebitentry = new tblTransaction();
                    setdebitentry.FinancialYearID = FinancialYearID;
                    setdebitentry.AccountHeadID = AccountHeadID;
                    setdebitentry.AccountControlID = AccountControlID;
                    setdebitentry.AccountSubControlID = AccountSubControlID;
                    setdebitentry.InvoiceNo = invoiceno;
                    setdebitentry.CompanyID = companyid;
                    setdebitentry.BranchID = branchid;
                    setdebitentry.Credit = 0;
                    setdebitentry.Debit = totalamount;
                    setdebitentry.TransectionDate = datetime;
                    setdebitentry.TransectionTitle = "Purchase From " + supplier.SupplierName;
                    setdebitentry.UserID = userid;
                    DB.tblTransactions.Add(setdebitentry);
                    DB.SaveChanges();

                    // Credit Entry : Payment Pending Activity
                    var Creditentry = DB.tblAccountSettings.Where(s => s.AccountActivityID == 5 && s.CompanyID == companyid && s.BranchID == branchid).FirstOrDefault();
                    if (Creditentry == null)
                    {
                        ModelState.AddModelError("ProductID", "First Set Account Flow!");
                        transaction.Rollback();
                    }
                    AccountHeadID = Creditentry.AccountHeadID;
                    AccountControlID = Creditentry.AccountControlID;
                    AccountSubControlID = Creditentry.AccountSubControlID;

                    var setcreditentry = new tblTransaction();
                    setcreditentry.FinancialYearID = FinancialYearID;
                    setcreditentry.AccountHeadID = AccountHeadID;
                    setcreditentry.AccountControlID = AccountControlID;
                    setcreditentry.AccountSubControlID = AccountSubControlID;
                    setcreditentry.InvoiceNo = invoiceno;
                    setcreditentry.CompanyID = companyid;
                    setcreditentry.BranchID = branchid;
                    setcreditentry.Credit = totalamount;
                    setcreditentry.Debit = 0;
                    setcreditentry.TransectionDate = datetime;
                    setcreditentry.TransectionTitle = "Purchase Payment Is Pending (" + supplier.SupplierName + ")";
                    setcreditentry.UserID = userid;
                    DB.tblTransactions.Add(setcreditentry);
                    DB.SaveChanges();



                    if (ispaymentispaid == true)
                    {
                        invoiceno = "PPP" + DateTime.Now.ToString("yyyymmss") + userid;
                        var invoicepayment = new tblSupplierPayment();
                        invoicepayment.SupplierID = (int)supplierid;
                        invoicepayment.SupplierInvoiceID = invoiceheader.SupplierInvoiceID;
                        invoicepayment.CompanyID = companyid;
                        invoicepayment.BranchID = branchid;
                        invoicepayment.InvoiceNo = invoiceno;
                        invoicepayment.TotalAmount = totalamount;
                        invoicepayment.PaymentAmount = totalamount;
                        invoicepayment.RemainingBalance = 0;
                        invoicepayment.UserID = userid;
                        invoicepayment.InvoiceDate = DateTime.Now;
                        DB.tblSupplierPayments.Add(invoicepayment);
                        DB.SaveChangesAsync();

                        // Payment Debit Transaction : Purchase Payment Pending > 5

                        debitentry = DB.tblAccountSettings.Where(s => s.AccountActivityID == 5 && s.CompanyID == companyid && s.BranchID == branchid).FirstOrDefault();
                        if (debitentry == null)
                        {
                            ModelState.AddModelError("ProductID", "First Set Account Flow!");
                            transaction.Rollback();
                        }
                        AccountHeadID = debitentry.AccountHeadID;
                        AccountControlID = debitentry.AccountControlID;
                        AccountSubControlID = debitentry.AccountSubControlID;

                        setdebitentry = new tblTransaction();
                        setdebitentry.FinancialYearID = FinancialYearID;
                        setdebitentry.AccountHeadID = AccountHeadID;
                        setdebitentry.AccountControlID = AccountControlID;
                        setdebitentry.AccountSubControlID = AccountSubControlID;
                        setdebitentry.InvoiceNo = invoiceno;
                        setdebitentry.CompanyID = companyid;
                        setdebitentry.BranchID = branchid;
                        setdebitentry.Credit = 0;
                        setdebitentry.Debit = totalamount;
                        setdebitentry.TransectionDate = datetime;
                        setdebitentry.TransectionTitle = "Purchase Payment is Transfer(" + supplier.SupplierName + ")";
                        setdebitentry.UserID = userid;
                        DB.tblTransactions.Add(setdebitentry);
                        DB.SaveChanges();


                        // Payment Credit Entry : Purchase Payment Paid > 6
                        Creditentry = DB.tblAccountSettings.Where(s => s.AccountActivityID == 6 && s.CompanyID == companyid && s.BranchID == branchid).FirstOrDefault();
                        if (Creditentry == null)
                        {
                            ModelState.AddModelError("ProductID", "First Set Account Flow!");
                            transaction.Rollback();
                        }
                        AccountHeadID = Creditentry.AccountHeadID;
                        AccountControlID = Creditentry.AccountControlID;
                        AccountSubControlID = Creditentry.AccountSubControlID;

                        setcreditentry = new tblTransaction();
                        setcreditentry.FinancialYearID = FinancialYearID;
                        setcreditentry.AccountHeadID = AccountHeadID;
                        setcreditentry.AccountControlID = AccountControlID;
                        setcreditentry.AccountSubControlID = AccountSubControlID;
                        setcreditentry.InvoiceNo = invoiceno;
                        setcreditentry.CompanyID = companyid;
                        setcreditentry.BranchID = branchid;
                        setcreditentry.Credit = totalamount;
                        setcreditentry.Debit = 0;
                        setcreditentry.TransectionDate = datetime;
                        setcreditentry.TransectionTitle = "Purchase Payment Is Paid (" + supplier.SupplierName + ")";
                        setcreditentry.UserID = userid;
                        DB.tblTransactions.Add(setcreditentry);
                        DB.SaveChanges();
                    }
                    DB.Database.ExecuteSqlCommand("TRUNCATE TABLE tblPurchaseCartDetail");
                    transaction.Commit();
                    return RedirectToAction("PrintPurchaseInvoive", new { supplierinvoiceid = invoiceheader.SupplierInvoiceID });
                }
                catch
                {
                    transaction.Rollback();
                }
            }

            return View();
        }

        public ActionResult PrintPurchaseInvoive(int? supplierinvoiceid)
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

            var supplierinvoice = DB.tblSupplierInvoices.Find(supplierinvoiceid);


            var purchaseinvoice = new PrintPurchaseInvoiceMV();


            // Set Purchase Invoice Header Details
            var invoiceheader = new SupplierInvoiceMV();
            invoiceheader.SupplierInvoiceID = supplierinvoice.SupplierInvoiceID;
            invoiceheader.SupplierID = supplierinvoice.SupplierID;
            invoiceheader.CompanyID = supplierinvoice.CompanyID;
            invoiceheader.BranchID = supplierinvoice.BranchID;
            invoiceheader.InvoiceNo = supplierinvoice.InvoiceNo;
            invoiceheader.TotalAmount = supplierinvoice.TotalAmount;
            invoiceheader.InvoiceDate = supplierinvoice.InvoiceDate;
            invoiceheader.Description = supplierinvoice.Description;
            invoiceheader.subtotalamount = supplierinvoice.subtotalamount;
            invoiceheader.shippingfee = supplierinvoice.shippingfee;
            invoiceheader.estimatedtax = supplierinvoice.estimatedtax;
            purchaseinvoice.InvoiceHeader = invoiceheader;


            // Set Purchase Invoice Branch
            var branch = new BranchMV();
            branch.BranchName = supplierinvoice.tblBranch.BranchName;
            branch.BranchContact = supplierinvoice.tblBranch.BranchContact;
            branch.BranchAddress = supplierinvoice.tblBranch.BranchAddress;
            purchaseinvoice.branch = branch;


            // Set Purchase invoice supplier
            var supplier = new SupplierMV();
            supplier.SupplierName = supplierinvoice.tblSupplier.SupplierName;
            supplier.SupplierConatctNo = supplierinvoice.tblSupplier.SupplierConatctNo;
            supplier.SupplierAddress = supplierinvoice.tblSupplier.SupplierAddress;
            supplier.SupplierEmail = supplierinvoice.tblSupplier.SupplierEmail;
            purchaseinvoice.supplier = supplier;

            var purchaseitems = new List<SupplierInvoiceDetailMV>();
            foreach (var item in supplierinvoice.tblSupplierInvoiceDetails)
            {
                var product = new SupplierInvoiceDetailMV();
                product.SupplierInvoiceDetailID = item.SupplierInvoiceDetailID;
                product.SupplierInvoiceID = item.SupplierInvoiceID;
                product.ProductID = item.ProductID;
                product.ProductName = item.tblStock.ProductName;
                product.PurchaseQuantity = item.PurchaseQuantity;
                product.purchaseUnitPrice = item.purchaseUnitPrice;
                product.previouspurchaseunitprice = item.previouspurchaseunitprice;
                product.manfacturedate = item.manfacturedate;
                product.expirydate = item.expirydate;
                product.ItemCost = (item.PurchaseQuantity * item.purchaseUnitPrice);
                purchaseitems.Add(product);
            }
            purchaseinvoice.InvoiceDetails = purchaseitems;


            return View(purchaseinvoice);
        }


        public ActionResult AllPurchases()
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

            var purchaselist = new List<PrintPurchaseInvoiceMV>();

            var allpurchase = DB.tblSupplierInvoices.Where(s => s.CompanyID == companyid && s.BranchID == branchid);
            foreach (var supplierinvoice in allpurchase)
            {
                var purchaseinvoice = new PrintPurchaseInvoiceMV();

                // Set Purchase Invoice Header Details
                var invoiceheader = new SupplierInvoiceMV();
                invoiceheader.SupplierInvoiceID = supplierinvoice.SupplierInvoiceID;
                invoiceheader.SupplierID = supplierinvoice.SupplierID;
                invoiceheader.CompanyID = supplierinvoice.CompanyID;
                invoiceheader.BranchID = supplierinvoice.BranchID;
                invoiceheader.InvoiceNo = supplierinvoice.InvoiceNo;
                invoiceheader.TotalAmount = supplierinvoice.TotalAmount;
                invoiceheader.InvoiceDate = supplierinvoice.InvoiceDate;
                invoiceheader.Description = supplierinvoice.Description;
                invoiceheader.subtotalamount = supplierinvoice.subtotalamount;
                invoiceheader.shippingfee = supplierinvoice.shippingfee;
                invoiceheader.estimatedtax = supplierinvoice.estimatedtax;
                purchaseinvoice.InvoiceHeader = invoiceheader;


                // Set Purchase Invoice Branch
                var branch = new BranchMV();
                branch.BranchName = supplierinvoice.tblBranch.BranchName;
                branch.BranchContact = supplierinvoice.tblBranch.BranchContact;
                branch.BranchAddress = supplierinvoice.tblBranch.BranchAddress;
                purchaseinvoice.branch = branch;


                // Set Purchase invoice supplier
                var supplier = new SupplierMV();
                supplier.SupplierName = supplierinvoice.tblSupplier.SupplierName;
                supplier.SupplierConatctNo = supplierinvoice.tblSupplier.SupplierConatctNo;
                supplier.SupplierAddress = supplierinvoice.tblSupplier.SupplierAddress;
                supplier.SupplierEmail = supplierinvoice.tblSupplier.SupplierEmail;
                purchaseinvoice.supplier = supplier;

                var purchaseitems = new List<SupplierInvoiceDetailMV>();
                foreach (var item in supplierinvoice.tblSupplierInvoiceDetails)
                {
                    var product = new SupplierInvoiceDetailMV();
                    product.SupplierInvoiceDetailID = item.SupplierInvoiceDetailID;
                    product.SupplierInvoiceID = item.SupplierInvoiceID;
                    product.ProductID = item.ProductID;
                    product.ProductName = item.tblStock.ProductName;
                    product.PurchaseQuantity = item.PurchaseQuantity;
                    product.purchaseUnitPrice = item.purchaseUnitPrice;
                    product.previouspurchaseunitprice = item.previouspurchaseunitprice;
                    product.manfacturedate = item.manfacturedate;
                    product.expirydate = item.expirydate;
                    product.ItemCost = (item.PurchaseQuantity * item.purchaseUnitPrice);
                    purchaseitems.Add(product);
                }

                var supplierpayment = DB.tblSupplierPayments.Where(p=>p.SupplierInvoiceID == supplierinvoice.SupplierInvoiceID).ToList();
                if(supplierpayment != null)
                {
                    if(supplierpayment.Count > 0)
                    {
                        purchaseinvoice.PaidAmount = supplierpayment.Sum(s => s.PaymentAmount);
                    }
                }

                purchaseinvoice.InvoiceDetails = purchaseitems;
                purchaselist.Add(purchaseinvoice);
            }
            return View(purchaselist);
        }

    }
}