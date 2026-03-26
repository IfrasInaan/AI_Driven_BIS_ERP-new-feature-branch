using DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class CustomerInvoiceController : Controller
    {
        private BussinessERPDbEntities1 DB = new BussinessERPDbEntities1();

        // GET: CustomerInvoice/SalesCart
        public ActionResult SaleCart()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["Username"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var userid = Convert.ToInt32(Session["UserId"]);
            var companyid = Convert.ToInt32(Session["CompanyID"]);
            var branchid = Convert.ToInt32(Session["BranchID"]);

            var cartItems = DB.tblSaleCartDetails.Where(s => s.CompanyID == companyid && s.BranchID == branchid && s.UserID == userid).ToList();

            var saleCart = new CustomerInvoiceMV
            {
                SaleItems = new List<SaleCartDetailMV>(),
                OrderSummary = new CustomerPaymentMV()
            };

            double subTotal = 0;
            foreach(var item in cartItems)
            {
                var product = DB.tblStocks.Find(item.ProductID);
                saleCart.SaleItems.Add(new SaleCartDetailMV
                {
                    SaleCartDetailID = item.SaleCartDetailID,
                    ProductID = item.ProductID,
                    ProductName = product?.ProductName ?? "Unknown",
                    SaleQuantity = item.SaleQuantity,
                    SaleUnitPrice = item.SaleUnitPrice,
                    Total = item.SaleQuantity * item.SaleUnitPrice
                });
                subTotal += item.SaleQuantity * item.SaleUnitPrice;
            }

            saleCart.OrderSummary.TotalAmount = subTotal;
            ViewBag.CustomerID = new SelectList(DB.tblCustomers.Where(c => c.BranchID == branchid), "CustomerID", "CustomerName");
            ViewBag.ProductID = new SelectList(DB.tblStocks.Where(s => s.BranchID == branchid), "ProductID", "ProductName");

            return View(saleCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(SaleCartDetailMV item)
        {
            if (ModelState.IsValid)
            {
                var existingItem = DB.tblSaleCartDetails
                    .FirstOrDefault(s => s.ProductID == item.ProductID
                        && s.UserID == item.UserID);

                if (existingItem == null)
                {
                    var newItem = new tblSaleCartDetail
                    {
                        ProductID = item.ProductID,
                        SaleQuantity = item.SaleQuantity,
                        SaleUnitPrice = item.SaleUnitPrice,
                        CompanyID = Convert.ToInt32(Session["CompanyID"]),
                        BranchID = Convert.ToInt32(Session["BranchID"]),
                        UserID = Convert.ToInt32(Session["UserID"])
                    };
                    DB.tblSaleCartDetails.Add(newItem);
                }
                else
                {
                    existingItem.SaleQuantity += item.SaleQuantity;
                }
                DB.SaveChanges();
            }
            return RedirectToAction("SaleCart");
        }

        public ActionResult RemoveCartItem(int id)
        {
            var item = DB.tblSaleCartDetails.Find(id);
            if (item != null)
            {
                DB.tblSaleCartDetails.Remove(item);
                DB.SaveChanges();
            }
            return RedirectToAction("SaleCart");
        }

        [HttpPost]
        public ActionResult CheckoutSale(int customerID, bool isPaid)
        {
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    var userid = Convert.ToInt32(Session["UserID"]);
                    var companyid = Convert.ToInt32(Session["CompanyID"]);
                    var branchid = Convert.ToInt32(Session["BranchID"]);
                    var datetime = DateTime.Now;

                    // Create Invoice Header
                    var invoice = new tblCustomerInvoice
                    {
                        CustomerID = customerID,
                        CompanyID = companyid,
                        BranchID = branchid,
                        InvoiceDate = datetime,
                        TotalAmount = DB.tblSaleCartDetails
                            .Where(s => s.UserID == userid)
                            .Sum(s => s.SaleQuantity * s.SaleUnitPrice),
                        UserID = userid,
                        InvoiceNo = "SALE" + datetime.ToString("yyyyMMddHHmmss")
                    };
                    DB.tblCustomerInvoices.Add(invoice);
                    DB.SaveChanges();

                    // Create Invoice Details and Update Stock
                    foreach (var cartItem in DB.tblSaleCartDetails.Where(s => s.UserID == userid))
                    {
                        // Add Invoice Detail
                        DB.tblCustomerInvoiceDetails.Add(new tblCustomerInvoiceDetail
                        {
                            CustomerInvoiceID = invoice.CustomerInvoiceID,
                            ProductID = cartItem.ProductID,
                            SaleQuantity = cartItem.SaleQuantity,
                            SaleUnitPrice = cartItem.SaleUnitPrice
                        });

                        // Update Stock
                        var stock = DB.tblStocks.Find(cartItem.ProductID);
                        stock.Quantity -= cartItem.SaleQuantity;
                    }

                    // Handle Payment
                    if (isPaid)
                    {
                        DB.tblCustomerPayments.Add(new tblCustomerPayment
                        {
                            CustomerInvoiceID = invoice.CustomerInvoiceID,
                            PaidAmount = invoice.TotalAmount,
                            InvoiceDate = datetime,
                            UserID = userid
                        });
                    }

                    // Clear Cart
                    DB.Database.ExecuteSqlCommand("DELETE FROM tblSaleCartDetails WHERE UserID = {0}", userid);

                    transaction.Commit();
                    return RedirectToAction("PrintInvoice", new { id = invoice.CustomerInvoiceID });
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /* public ActionResult PrintInvoice(int id)
         {
             var invoice = DB.tblCustomerInvoices.Find(id);
             var viewModel = new CustomerInvoiceMV
             {
                 InvoiceHeader = new CustomerInvoiceMV
                 {
                     InvoiceNo = invoice.InvoiceNo,
                     InvoiceDate = invoice.InvoiceDate,
                     TotalAmount = invoice.TotalAmount,
                     Customer = DB.tblCustomers.Find(invoice.CustomerID)?.Customername
                 },
                 InvoiceDetails = DB.tblCustomerInvoiceDetails
                     .Where(d => d.CustomerInvoiceID == id)
                     .Select(d => new CustomerInvoiceDetailMV
                     {
                         ProductName = d.tblStock.ProductName,
                         SaleQuantity = d.SaleQuantity,
                         SaleUnitPrice = d.SaleUnitPrice,
                         Total = d.SaleQuantity * d.SaleUnitPrice
                     }).ToList()
             };
             return View(viewModel);
         }

         public ActionResult AllSales()
         {
             var companyid = Convert.ToInt32(Session["CompanyID"]);
             var branchid = Convert.ToInt32(Session["BranchID"]);

             return View(DB.tblCustomerInvoices
                 .Where(i => i.CompanyID == companyid && i.BranchID == branchid)
                 .Select(i => new CustomerInvoiceMV
                 {
                     InvoiceNo = i.InvoiceNo,
                     InvoiceDate = i.InvoiceDate,
                     TotalAmount = i.TotalAmount,
                     Customer = i.tblCustomer.Customername
                 }).ToList());
         }
        */
        public ActionResult AllSales()
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

            var saleslist = new List<PrintSalesInvoiceMV>();

            var allsales = DB.tblCustomerInvoices.Where(s => s.CompanyID == companyid && s.BranchID == branchid);
            foreach (var customerinvoice in allsales)
            {
                var salesinvoice = new PrintSalesInvoiceMV();

                // Set Sales Invoice Header Details
                var invoiceheader = new CustomerInvoiceMV();
                invoiceheader.CustomerInvoiceID = customerinvoice.CustomerInvoiceID;
                invoiceheader.CustomerID = customerinvoice.CustomerID;
                invoiceheader.CompanyID = customerinvoice.CompanyID;
                invoiceheader.BranchID = customerinvoice.BranchID;
                invoiceheader.InvoiceNo = customerinvoice.CustomerInvoiceID;
                invoiceheader.Title = customerinvoice.Title;
                invoiceheader.TotalAmount = customerinvoice.TotalAmount;
                invoiceheader.InvoiceDate = customerinvoice.InvoiceDate;
                invoiceheader.Description = customerinvoice.Description;
                invoiceheader.UserID = customerinvoice.UserID;
                salesinvoice.InvoiceHeader = invoiceheader;

                // Set Sales Invoice Branch
                var branch = new BranchMV();
                branch.BranchName = customerinvoice.tblBranch.BranchName;
                branch.BranchContact = customerinvoice.tblBranch.BranchContact;
                branch.BranchAddress = customerinvoice.tblBranch.BranchAddress;
                salesinvoice.branch = branch;

                // Set Sales invoice customer
                var customer = new CustomerMV();
                customer.CustomerID = customerinvoice.tblCustomer.CustomerID;
                customer.Customername = customerinvoice.tblCustomer.Customername;
                customer.CustomerContact = customerinvoice.tblCustomer.CustomerContact;
                customer.CustomerAddress = customerinvoice.tblCustomer.CustomerAddress;
                customer.CustomerArea = customerinvoice.tblCustomer.CustomerArea;
                salesinvoice.customer = customer;

                var salesitems = new List<CustomerInvoiceDetailMV>();
                foreach (var item in customerinvoice.tblCustomerInvoiceDetails)
                {
                    var product = new CustomerInvoiceDetailMV();
                    product.CustomerInvoiceDetailID = item.CustomerInvoiceDetailID;
                    product.CustomerInvoiceID = item.CustomerInvoiceID;
                    product.ProductID = item.ProductID;
                    product.ProductName = item.tblStock.ProductName;
                    product.SaleQuantity = item.SaleQuantity;
                    product.SaleUnitPrice = item.SaleUnitPrice;
                    salesitems.Add(product);
                }

                var customerpayment = DB.tblCustomerPayments.Where(p => p.CustomerInvoiceID == customerinvoice.CustomerInvoiceID).ToList();
                if (customerpayment != null)
                {
                    if (customerpayment.Count > 0)
                    {
                        salesinvoice.PaidAmount = customerpayment.Sum(s => s.PaidAmount);
                    }
                }

                salesinvoice.InvoiceDetails = salesitems;
                saleslist.Add(salesinvoice);
            }
            return View(saleslist);
        }

        public ActionResult PrintSalesInvoive(int? customerinvoiceid)
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

            var customerinvoice = DB.tblCustomerInvoices.Find(customerinvoiceid);


            var salesinvoice = new PrintSalesInvoiceMV();


            // Set Sales Invoice Header Details
            var invoiceheader = new CustomerInvoiceMV();
            invoiceheader.CustomerInvoiceID = customerinvoice.CustomerInvoiceID;
            invoiceheader.CustomerID = customerinvoice.CustomerID;
            invoiceheader.CompanyID = customerinvoice.CompanyID;
            invoiceheader.BranchID = customerinvoice.BranchID;
            invoiceheader.InvoiceNo = customerinvoice.CustomerInvoiceID;
            invoiceheader.TotalAmount = customerinvoice.TotalAmount;
            invoiceheader.InvoiceDate = customerinvoice.InvoiceDate;
            invoiceheader.Description = customerinvoice.Description;
            salesinvoice.InvoiceHeader = invoiceheader;


            // Set Sale Invoice Branch
            var branch = new BranchMV();
            branch.BranchName = customerinvoice.tblBranch.BranchName;
            branch.BranchContact = customerinvoice.tblBranch.BranchContact;
            branch.BranchAddress = customerinvoice.tblBranch.BranchAddress;
            salesinvoice.branch = branch;


            // Set Sale invoice customer
            var customer = new CustomerMV();
            customer.Customername = customerinvoice.tblCustomer.Customername;
            customer.CustomerContact = customerinvoice.tblCustomer.CustomerContact;
            customer.CustomerAddress = customerinvoice.tblCustomer.CustomerAddress;
            customer.Description = customerinvoice.tblCustomer.Description;
            salesinvoice.customer = customer;

            var salesitems = new List<CustomerInvoiceDetailMV>();
            foreach (var item in customerinvoice.tblCustomerInvoiceDetails)
            {
                var product = new CustomerInvoiceDetailMV();
                product.CustomerInvoiceDetailID = item.CustomerInvoiceDetailID;
                product.CustomerInvoiceID = item.CustomerInvoiceID;
                product.ProductID = item.ProductID;
                product.ProductName = item.tblStock.ProductName;
                product.SaleQuantity = item.SaleQuantity;
                product.SaleUnitPrice = item.SaleUnitPrice;
                product.ItemCost = (item.SaleQuantity * item.SaleUnitPrice);
                salesitems.Add(product);
            }
            salesinvoice.InvoiceDetails = salesitems;


            return View(salesinvoice);
        }

    }
}