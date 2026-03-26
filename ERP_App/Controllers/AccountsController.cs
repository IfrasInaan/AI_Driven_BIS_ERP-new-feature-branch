using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseLayer;
using ERP_App.Models;

namespace ERP_App.Controllers
{
    public class AccountsController : Controller
    {
        private BussinessERPDbEntities1 DB = new BussinessERPDbEntities1();

        // GET: tblAccountControls
        public ActionResult AllAccountControls()
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

            var list = new List<AccountControlMV>();
            var tblAccountControls = DB.tblAccountControls.Where(a=>a.BranchID == branchid && a.CompanyID == companyid).ToList();
            
            foreach(var account in tblAccountControls)
            {
                var accountcontrol = new AccountControlMV();

                accountcontrol.AccountControlID = account.AccountControlID;
                accountcontrol.AccountHeadID = account.AccountHeadID;
                var accounthead = DB.tblAccountHeads.Find(account.AccountHeadID);
                accountcontrol.AccountHead = accounthead.AccountHeadName;
                accountcontrol.AccountControlName = account.AccountControlName;
                accountcontrol.UserID = account.UserID;
                accountcontrol.CreateBy = account.tblUser.UserName;
                list.Add(accountcontrol);

            }                       
            return View(list);
        }

     
        public JsonResult GetAccountControls(int? accountheadID)
        {
            
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

            var list = new List<AccountControlMV>();
            var tblAccountControls = DB.tblAccountControls.Where(a => a.BranchID == branchid && a.CompanyID == companyid && a.AccountHeadID == accountheadID).ToList();

            foreach (var account in tblAccountControls)
            {
                var accountcontrol = new AccountControlMV();

                accountcontrol.AccountControlID = account.AccountControlID;
                accountcontrol.AccountHeadID = account.AccountHeadID;
                var accounthead = DB.tblAccountHeads.Find(account.AccountHeadID);
                accountcontrol.AccountHead = accounthead.AccountHeadName;
                accountcontrol.AccountControlName = account.AccountControlName;
                accountcontrol.UserID = account.UserID;
                accountcontrol.CreateBy = account.tblUser.UserName;
                list.Add(accountcontrol);

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccountSubControls(int? accountControlID)
        {

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

            var list = new List<AccountSubControlMV>();
            var tblAccountControls = DB.tblAccountSubControls.Where(a => a.BranchID == branchid && a.CompanyID == companyid && a.AccountControlID == accountControlID).ToList();

            foreach (var account in tblAccountControls)
            {
                var accountcontrol = new AccountSubControlMV();

                accountcontrol.AccountSubControlID = account.AccountSubControlID;             
                accountcontrol.AccountSubControlName = account.AccountSubControlName;               
                list.Add(accountcontrol);

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: tblAccountControls/Create
        public ActionResult CreateAccountControl()
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


            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads, "AccountHeadID", "AccountHeadName", "0");
          
            var account = new AccountControlMV();                    
            return View(account);
        }

        // POST: tblAccountControls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccountControl(AccountControlMV accountcontrolMV)
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

            accountcontrolMV.CompanyID = companyid;
            accountcontrolMV.BranchID = branchid;
            accountcontrolMV.UserID = userid;
            
            if (ModelState.IsValid)
            {
                var checkaccountcontrols = DB.tblAccountControls.Where(a=>a.AccountHeadID == accountcontrolMV.AccountHeadID && a.AccountControlName == accountcontrolMV.AccountControlName && a.CompanyID == companyid && a.BranchID == branchid).FirstOrDefault();
                if (checkaccountcontrols == null)
                {
                    var newaccountcontrol = new tblAccountControl();

                    newaccountcontrol.CompanyID = companyid;
                    newaccountcontrol.BranchID = branchid;
                    newaccountcontrol.AccountHeadID = accountcontrolMV.AccountHeadID;
                    newaccountcontrol.AccountControlName = accountcontrolMV.AccountControlName;
                    newaccountcontrol.UserID = userid;

                    DB.tblAccountControls.Add(newaccountcontrol);
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountControls");
                }
                else
                {
                    ModelState.AddModelError("AccountControlName", "Already Exists!");
                }
            }

            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads, "AccountHeadID", "AccountHeadName", accountcontrolMV.AccountHeadID);
            return View(accountcontrolMV );
        }

        // GET: tblAccountControls/Edit/5
        public ActionResult EditAccountControl(int? id)
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

            
            tblAccountControl tblAccountControl = DB.tblAccountControls.Find(id);
          
            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads, "AccountHeadID", "AccountHeadName", tblAccountControl.AccountHeadID);
            var account = new AccountControlMV();
            account.AccountControlID = tblAccountControl.AccountControlID;
            account.AccountControlName = tblAccountControl.AccountControlName;

            return View(account);
            
        }

        // POST: tblAccountControls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountControl(AccountControlMV accountcontrolMV)
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

            accountcontrolMV.CompanyID = companyid;
            accountcontrolMV.BranchID = branchid;
            accountcontrolMV.UserID = userid;
            if (ModelState.IsValid)
            {
                var checkaccountcontrols = DB.tblAccountControls.Where(a => a.AccountHeadID == accountcontrolMV.AccountHeadID && a.AccountControlID != accountcontrolMV.AccountControlID && a.AccountControlName == accountcontrolMV.AccountControlName && a.CompanyID == companyid && a.BranchID == branchid).FirstOrDefault();
                if (checkaccountcontrols == null)
                {
                    var editaccountcontrol = DB.tblAccountControls.Find(accountcontrolMV.AccountControlID);
                    editaccountcontrol.AccountControlName = accountcontrolMV.AccountControlName;
                    editaccountcontrol.AccountHeadID = accountcontrolMV.AccountHeadID;
                    editaccountcontrol.UserID = userid;
                    DB.Entry(editaccountcontrol).State = EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountControls");
                }
                else
                {
                    ModelState.AddModelError("AccountControlName", "Already Exists!");
                }
            }
            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads, "AccountHeadID", "AccountHeadName", accountcontrolMV.AccountHeadID);
            return View(accountcontrolMV);
        }


        public ActionResult AllAccountSubControls()
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

            var list = new List<AccountSubControlMV>();
            var accountsubControlslist = DB.tblAccountSubControls.Where(a => a.BranchID == branchid && a.CompanyID == companyid).ToList();

            foreach (var account in accountsubControlslist)
            {
                var accountsubcontrol = new AccountSubControlMV();

                accountsubcontrol.AccountSubControlID = account.AccountSubControlID;
                accountsubcontrol.AccountSubControlName = account.AccountSubControlName;
                accountsubcontrol.AccountControlID = account.AccountControlID;
                accountsubcontrol.AccountHeadID = account.AccountHeadID;
                var accounthead = DB.tblAccountHeads.Find(account.AccountHeadID);
                accountsubcontrol.AccountHead = accounthead.AccountHeadName;
                accountsubcontrol.AccountControl = account.tblAccountControl.AccountControlName;
                accountsubcontrol.UserID = account.UserID;
                accountsubcontrol.CreatedBy = account.tblUser.UserName;
                list.Add(accountsubcontrol);

            }
            return View(list);
        }


        // GET: tblAccountSubControls/Create
        public ActionResult CreateAccountSubControl()
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


            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads.ToList(), "AccountHeadID", "AccountHeadName", "0");
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.Where(c=>c.BranchID == branchid && c.CompanyID == companyid).ToList(), "AccountControlID", "AccountControlName", "0");

            var account = new AccountSubControlMV();
            return View(account);
        }

        // POST: tblAccountControls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccountSubControl(AccountSubControlMV accountsubcontrolMV)
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

            accountsubcontrolMV.CompanyID = companyid;
            accountsubcontrolMV.BranchID = branchid;
            accountsubcontrolMV.UserID = userid;

            if (ModelState.IsValid)
            {
                var checkaccountsubcontrols = DB.tblAccountSubControls.Where(a => a.AccountHeadID == accountsubcontrolMV.AccountHeadID && a.AccountHeadID == accountsubcontrolMV.AccountControlID &&  a.AccountSubControlName == accountsubcontrolMV.AccountSubControlName && a.CompanyID == companyid && a.BranchID == branchid).FirstOrDefault();
                if (checkaccountsubcontrols == null)
                {
                    var newaccountsubcontrol = new tblAccountSubControl();

                    newaccountsubcontrol.CompanyID = companyid;
                    newaccountsubcontrol.BranchID = branchid;
                    newaccountsubcontrol.AccountHeadID = accountsubcontrolMV.AccountHeadID;
                    newaccountsubcontrol.AccountControlID = accountsubcontrolMV.AccountControlID;
                    newaccountsubcontrol.AccountSubControlName = accountsubcontrolMV.AccountSubControlName;
                    newaccountsubcontrol.UserID = userid;

                    DB.tblAccountSubControls.Add(newaccountsubcontrol);
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountSubControls");
                }
                else
                {
                    ModelState.AddModelError("AccountSubControlName", "Already Exists!");
                }
            }

            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads, "AccountHeadID", "AccountHeadName", accountsubcontrolMV.AccountHeadID);
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.Where(c => c.BranchID == branchid && c.CompanyID == companyid).ToList(), "AccountControlID", "AccountControlName", accountsubcontrolMV.AccountControlID);

            return View(accountsubcontrolMV);
        }

        public ActionResult EditAccountSubControl(int? id)
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

            var accountsubcontrol = DB.tblAccountSubControls.Find(id);
            var accountsubcontrolMV = new AccountSubControlMV();
            accountsubcontrolMV.AccountSubControlID = accountsubcontrol.AccountSubControlID;
            accountsubcontrolMV.AccountHeadID = accountsubcontrol.AccountHeadID;
            accountsubcontrolMV.AccountControlID = accountsubcontrol.AccountControlID;
            accountsubcontrolMV.AccountSubControlName = accountsubcontrol.AccountSubControlName;

            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads.ToList(), "AccountHeadID", "AccountHeadName", accountsubcontrolMV.AccountHeadID);
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.Where(c => c.BranchID == branchid && c.CompanyID == companyid && c.AccountHeadID == accountsubcontrolMV.AccountHeadID).ToList(), "AccountControlID", "AccountControlName", accountsubcontrolMV.AccountControlID);

            return View(accountsubcontrolMV);
        }

        // POST: tblAccountControls/edit
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountSubControl(AccountSubControlMV accountsubcontrolMV)
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

            accountsubcontrolMV.CompanyID = companyid;
            accountsubcontrolMV.BranchID = branchid;
            accountsubcontrolMV.UserID = userid;

            if (ModelState.IsValid)
            {
                var checkaccountsubcontrols = DB.tblAccountSubControls.Where(a => a.AccountHeadID == accountsubcontrolMV.AccountHeadID && a.AccountHeadID == accountsubcontrolMV.AccountControlID && a.AccountSubControlName == accountsubcontrolMV.AccountSubControlName && a.CompanyID == companyid && a.BranchID == branchid && a.AccountSubControlID != accountsubcontrolMV.AccountSubControlID).FirstOrDefault();
                if (checkaccountsubcontrols == null)
                {
                    var editaccountsubcontrol = DB.tblAccountSubControls.Find(accountsubcontrolMV.AccountSubControlID);

                    editaccountsubcontrol.AccountHeadID = accountsubcontrolMV.AccountHeadID;
                    editaccountsubcontrol.AccountControlID = accountsubcontrolMV.AccountControlID;
                    editaccountsubcontrol.AccountSubControlName = accountsubcontrolMV.AccountSubControlName;
                    editaccountsubcontrol.UserID = userid;

                    DB.Entry(editaccountsubcontrol).State = EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountSubControls");
                }
                else
                {
                    ModelState.AddModelError("AccountSubControlName", "Already Exists!");
                }
            }

            ViewBag.AccountHeadID = new SelectList(DB.tblAccountHeads, "AccountHeadID", "AccountHeadName", accountsubcontrolMV.AccountHeadID);
            ViewBag.AccountControlID = new SelectList(DB.tblAccountControls.Where(c => c.BranchID == branchid && c.CompanyID == companyid && c.AccountHeadID == accountsubcontrolMV.AccountHeadID ).ToList(), "AccountControlID", "AccountControlName", accountsubcontrolMV.AccountControlID);

            return View(accountsubcontrolMV);
        }

    }
}
