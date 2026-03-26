using DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        private BussinessERPDbEntities1 DB = new BussinessERPDbEntities1();
        public ActionResult AllBranchSupplier()
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


            var list = new List<SupplierMV>();
            var suppliers = DB.tblSuppliers.Where(c => c.BranchID == branchid && c.CompanyID == companyid).ToList();
            foreach (var supplier in suppliers)
            {
                var gsupplier = new SupplierMV();
                gsupplier.SupplierID = supplier.SupplierID;
                gsupplier.SupplierName = supplier.SupplierName;
                gsupplier.SupplierEmail = supplier.SupplierEmail;
                gsupplier.SupplierConatctNo = supplier.SupplierConatctNo;
                gsupplier.SupplierAddress = supplier.SupplierAddress;
                gsupplier.Discription = supplier.Discription;
                gsupplier.BranchID = supplier.BranchID;
                gsupplier.CompanyID = supplier.CompanyID;
                gsupplier.UserID = supplier.UserID;
                gsupplier.CreatedBy = supplier.tblUser.UserName;
                list.Add(gsupplier);
            }

            return View(list);
        }

        public ActionResult CreateBranchSupplier()
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


            var supplierMV = new SupplierMV();
            return View(supplierMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBranchSupplier(SupplierMV supplierMV)
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
                var checksupplier = DB.tblSuppliers.Where(u => u.BranchID == branchid && u.CompanyID == companyid && u.SupplierName == supplierMV.SupplierName).FirstOrDefault();
                if (checksupplier == null)
                {
                    var newsupplier = new tblSupplier();
                    newsupplier.SupplierName = supplierMV.SupplierName;
                    newsupplier.SupplierAddress = supplierMV.SupplierAddress;
                    newsupplier.SupplierConatctNo = supplierMV.SupplierConatctNo;
                    newsupplier.SupplierEmail = supplierMV.SupplierEmail;
                    newsupplier.Discription = supplierMV.Discription;
                    newsupplier.CompanyID = companyid;
                    newsupplier.BranchID = branchid;
                    newsupplier.UserID = userid;

                    DB.tblSuppliers.Add(newsupplier);
                    DB.SaveChanges();
                    return RedirectToAction("AllBranchSupplier");
                }
                else
                {
                    ModelState.AddModelError("SupplierName", "Already Exist");
                }

            }

            return View(supplierMV);
        }

        public ActionResult EditBranchSupplier(int? id)
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

            var supplier = DB.tblSuppliers.Find(id);
            var supplierMV = new SupplierMV();
            supplierMV.SupplierID = supplier.SupplierID;
            supplierMV.SupplierName = supplier.SupplierName;
            supplierMV.SupplierAddress = supplier.SupplierAddress;
            supplierMV.SupplierConatctNo = supplier.SupplierConatctNo;
            supplierMV.SupplierEmail = supplier.SupplierEmail;
            supplierMV.Discription = supplier.Discription;
            supplierMV.CompanyID = supplier.CompanyID;
            supplierMV.BranchID = supplier.BranchID;
            supplierMV.UserID = supplier.UserID;

            return View(supplierMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBranchSupplier(SupplierMV supplierMV)
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
                var checksupplier = DB.tblSuppliers.Where(u => u.BranchID == branchid && u.CompanyID == companyid && u.SupplierName == supplierMV.SupplierName && u.SupplierID != supplierMV.SupplierID ).FirstOrDefault();
                if (checksupplier == null)
                {
                    var editsupplier = DB.tblSuppliers.Find(supplierMV.SupplierID);
                    editsupplier.SupplierName = supplierMV.SupplierName;
                    editsupplier.SupplierAddress = supplierMV.SupplierAddress;
                    editsupplier.SupplierConatctNo = supplierMV.SupplierConatctNo;
                    editsupplier.SupplierEmail = supplierMV.SupplierEmail;
                    editsupplier.Discription = supplierMV.Discription;
                    editsupplier.CompanyID = companyid;
                    editsupplier.BranchID = branchid;
                    editsupplier.UserID = userid;

                    DB.Entry(editsupplier).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllBranchSupplier");
                }
                else
                {
                    ModelState.AddModelError("SupplierName", "Already Exist");
                }

            }

            return View(supplierMV);
        }

        public JsonResult GetSelectedSupplierDetails(int? id)
        {
            var data = new Supplier();
            if (id > 0)
            {
                var supplier = DB.tblSuppliers.Find(id);
                data.ContactNo = supplier.SupplierConatctNo;
                data.Address = supplier.SupplierAddress;

            }
            else
            {
                data.ContactNo = "";
                data.Address = "";
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }

    class Supplier
    {
        public string ContactNo { get; set; }
        public string Address { get; set;}
    }

}