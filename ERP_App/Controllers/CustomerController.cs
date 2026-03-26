using DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class CustomerController : Controller
    {
        private BussinessERPDbEntities1 DB = new BussinessERPDbEntities1();
        // GET: Customer
        public ActionResult AllBranchCustomer()
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

            
            var list = new List<CustomerMV>();
            var customers = DB.tblCustomers.Where(c => c.BranchID == branchid && c.CompanyID == companyid).ToList();
            foreach(var customer in customers)
            {
                var gcustomer = new CustomerMV();
                gcustomer.CustomerID = customer.CustomerID;
                gcustomer.Customername = customer.Customername;
                gcustomer.CustomerContact = customer.CustomerContact;
                gcustomer.CustomerArea = customer.CustomerArea;
                gcustomer.CustomerAddress = customer.CustomerAddress;
                gcustomer.Description = customer.Description;
                gcustomer.BranchID = customer.BranchID;
                gcustomer.CompanyID = customer.CompanyID;
                gcustomer.UserID = customer.UserID;
                gcustomer.CreatedBy = customer.tblUser.UserName;
                list.Add(gcustomer);
            }

            return View(list);
        }

        public ActionResult CreateBranchCustomer()
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


            var customermv = new CustomerMV();
            return View(customermv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBranchCustomer(CustomerMV customerMV)
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
                var checkcustomer = DB.tblCustomers.Where(u => u.BranchID == branchid && u.CompanyID == companyid && u.Customername == customerMV.Customername).FirstOrDefault();
                if (checkcustomer == null)
                {
                    var newcustomer = new tblCustomer();
                    newcustomer.Customername = customerMV.Customername;
                    newcustomer.Description = customerMV.Description;
                    newcustomer.CustomerContact = customerMV.CustomerContact;
                    newcustomer.CustomerArea = customerMV.CustomerArea;
                    newcustomer.CustomerAddress = customerMV.CustomerAddress;
                    newcustomer.CompanyID = companyid;
                    newcustomer.BranchID = branchid;
                    newcustomer.UserID = userid;
                    
                    DB.tblCustomers.Add(newcustomer);
                    DB.SaveChanges();
                    return RedirectToAction("AllBranchCustomer");
                }
                else
                {
                    ModelState.AddModelError("Customername", "Already Exist");
                }

            }

            return View(customerMV);
        }

        public ActionResult EditBranchCustomer(int? id)
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

            var customer = DB.tblCustomers.Find(id);
            var customermv = new CustomerMV();
            customermv.CustomerID = customer.CustomerID;
            customermv.Customername = customer.Customername;
            customermv.BranchID = customer.BranchID;
            customermv.CompanyID = customer.CompanyID;
            customermv.CustomerAddress = customer.CustomerAddress;
            customermv.CustomerArea = customer.CustomerArea;
            customermv.CustomerContact = customer.CustomerContact;
            customermv.Description = customer.Description;
            customermv.UserID = customer.CustomerID;

            return View(customermv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBranchCustomer(CustomerMV customerMV)
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
                var checkcustomer = DB.tblCustomers.Where(u => u.BranchID == branchid && u.CompanyID == companyid && u.Customername == customerMV.Customername && u.CustomerID != customerMV.CustomerID).FirstOrDefault();
                if (checkcustomer == null)
                {
                    var editcustomer = DB.tblCustomers.Find(customerMV.CustomerID);
                    editcustomer.Customername = customerMV.Customername;
                    editcustomer.Description = customerMV.Description;
                    editcustomer.CustomerContact = customerMV.CustomerContact;
                    editcustomer.CustomerArea = customerMV.CustomerArea;
                    editcustomer.CustomerAddress = customerMV.CustomerAddress;
                    editcustomer.CompanyID = companyid;
                    editcustomer.BranchID = branchid;
                    editcustomer.UserID = userid;

                    DB.Entry(editcustomer).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllBranchCustomer");
                }
                else
                {
                    ModelState.AddModelError("Customername", "Already Exist");
                }

            }

            return View(customerMV);
        }
        public JsonResult GetSelectedCustomerDetails(int? id)
        {
            var data = new Customer();
            if (id > 0)
            {
                var customer = DB.tblCustomers.Find(id);
                data.ContactNo = customer.CustomerContact;
                data.Address = customer.CustomerAddress;

            }
            else
            {
                data.ContactNo = "";
                data.Address = "";
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }

    class Customer
    {
        public string ContactNo { get; set; }
        public string Address { get; set; }
    }
}