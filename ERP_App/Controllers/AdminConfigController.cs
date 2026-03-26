using DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class AdminConfigController : Controller
    {
        private BussinessERPDbEntities1 DB = new BussinessERPDbEntities1();
        
        // Branch Types Configuration
        // =======================================================================================================================
        public ActionResult AllBranchTypes()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }

            var list = new List<BranchTypeMV>();
            var branchtypes = DB.tblBranchTypes.ToList();
            foreach (var branchtype in branchtypes)
            {
                list.Add(new BranchTypeMV() { BranchTypeID = branchtype.BranchTypeID, BranchType = branchtype.BranchType });
            }
            return View(list);
        }

        public ActionResult CreateBranchType()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }

            var branchtypemv = new BranchTypeMV();
            return View(branchtypemv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBranchType(BranchTypeMV branchtypemv)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            if (ModelState.IsValid)
            {
                var checkbranchtype = DB.tblBranchTypes.Where(u => u.BranchType == branchtypemv.BranchType.Trim()).FirstOrDefault();
                if (checkbranchtype == null)
                {
                    var newbranchtype = new tblBranchType();
                    newbranchtype.BranchType = branchtypemv.BranchType;
                    DB.tblBranchTypes.Add(newbranchtype);
                    DB.SaveChanges();
                    return RedirectToAction("AllBranchTypes");
                }
                else
                {
                    ModelState.AddModelError("BranchType", "Already Exist");
                }

            }

            return View(branchtypemv);
        }

        public ActionResult EditBranchType(int? branchtypeid)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var UserTypeID = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out UserTypeID);
            if (UserTypeID != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }

            var editbranchtype = DB.tblBranchTypes.Find(branchtypeid);
            var branchtypemv = new BranchTypeMV();
            branchtypemv.BranchTypeID = editbranchtype.BranchTypeID;
            branchtypemv.BranchType = editbranchtype.BranchType;
            return View(branchtypemv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBranchType(BranchTypeMV branchtypemv)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            if (ModelState.IsValid)
            {
                var checkbranchtype = DB.tblBranchTypes.Where(bt => bt.BranchType == branchtypemv.BranchType.Trim() && bt.BranchTypeID == branchtypemv.BranchTypeID).FirstOrDefault();
                if (checkbranchtype == null)
                {
                    var editbranchtype = new tblBranchType();
                    editbranchtype.BranchType = branchtypemv.BranchType;
                    editbranchtype.BranchTypeID = branchtypemv.BranchTypeID;
                    DB.Entry(editbranchtype).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllBranchTypes");
                }
                else
                {
                    ModelState.AddModelError("BranchType", "Already Exist");
                }

            }

            return View(branchtypemv);
        }

        // Account Activity Configuration
        // =======================================================================================================================================
        public ActionResult AllAccountActivity()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }

            var list = new List<AccountActivityMV>();
            var accountactivities = DB.tblAccountActivities.ToList();
            foreach (var accountactivity in accountactivities)
            {
                list.Add(new AccountActivityMV() { AccountActivityID = accountactivity.AccountActivityID, Name = accountactivity.Name });
            }
            return View(list);
        }

        public ActionResult CreateAccountActivity()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }

            var accountactivitymv = new AccountActivityMV();
            return View(accountactivitymv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccountActivity(AccountActivityMV accountactivitymv)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            if (ModelState.IsValid)
            {
                var checkaccountactivity = DB.tblAccountActivities.Where(u => u.Name == accountactivitymv.Name.Trim()).FirstOrDefault();
                if (checkaccountactivity == null)
                {
                    var newaccountactivity = new tblAccountActivity();
                    newaccountactivity.Name = accountactivitymv.Name;
                    DB.tblAccountActivities.Add(newaccountactivity);
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountActivity");
                }
                else
                {
                    ModelState.AddModelError("AccountActivity", "Already Exist");
                }

            }

            return View(accountactivitymv);
        }

        public ActionResult EditAccountActivity(int? accountactivityid)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var UserTypeID = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out UserTypeID);
            if (UserTypeID != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }

            var editaccountactivity = DB.tblAccountActivities.Find(accountactivityid);
            var accountactivitymv = new AccountActivityMV();
            accountactivitymv.AccountActivityID = editaccountactivity.AccountActivityID;
            accountactivitymv.Name = editaccountactivity.Name;
            return View(accountactivitymv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountActivity(AccountActivityMV accountactivitymv)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            if (ModelState.IsValid)
            {
                var checkaccountactivity = DB.tblAccountActivities.Where(at => at.Name == accountactivitymv.Name.Trim() && at.AccountActivityID == accountactivitymv.AccountActivityID).FirstOrDefault();
                if (checkaccountactivity == null)
                {
                    var editaccountactivity = new tblAccountActivity();
                    editaccountactivity.Name = accountactivitymv.Name;
                    editaccountactivity.AccountActivityID = accountactivitymv.AccountActivityID;
                    DB.Entry(editaccountactivity).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountActivity");
                }
                else
                {
                    ModelState.AddModelError("Name", "Already Exist");
                }

            }

            return View(accountactivitymv);
        }

        // Account Head's 
        // =======================================================================================================================================
        public ActionResult AllAccountHeads()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }

            var list = new List<AccountHeadMV>();
            var accountheads = DB.tblAccountHeads.ToList();
            foreach (var accounthead in accountheads)
            {
                var addaccounthead = new AccountHeadMV();
                addaccounthead.AccountHeadID = accounthead.AccountHeadID;
                addaccounthead.AccountHeadName = accounthead.AccountHeadName;
                addaccounthead.Code = accounthead.Code;
                addaccounthead.UserID = accounthead.UserID;
                addaccounthead.CreatedBy = accounthead.tblUser.UserName;

                list.Add(addaccounthead);
            }
            return View(list);
        }

        public ActionResult CreateAccountHead()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }

            var accountheadmv = new AccountHeadMV();
            accountheadmv.UserID = userid;
            return View(accountheadmv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccountHead(AccountHeadMV accountheadmv)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            if (ModelState.IsValid)
            {
                var checkaccounthead = DB.tblAccountHeads.Where(u => u.AccountHeadName == accountheadmv.AccountHeadName.Trim()).FirstOrDefault();
                if (checkaccounthead == null)
                {
                    var newaccounthead = new tblAccountHead();
                    newaccounthead.AccountHeadName = accountheadmv.AccountHeadName;
                    newaccounthead.Code = accountheadmv.Code;
                    newaccounthead.UserID = userid;
                    DB.tblAccountHeads.Add(newaccounthead);
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountHeads");
                }
                else
                {
                    ModelState.AddModelError("AccountHeadName", "Already Exist!");
                }
            }
            return View(accountheadmv);
        }

        public ActionResult EditAccountHead(int? accountheadid)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            var accounthead = DB.tblAccountHeads.Find(accountheadid);
            var accountheadmv = new AccountHeadMV();
            accountheadmv.AccountHeadID = accounthead.AccountHeadID;
            accountheadmv.AccountHeadName = accounthead.AccountHeadName;
            accountheadmv.Code = accounthead.Code;
            accountheadmv.UserID = userid;
            return View(accountheadmv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountHead(AccountHeadMV accountheadmv)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            if (ModelState.IsValid)
            {
                var checkaccounthead = DB.tblAccountHeads.Where(u => u.AccountHeadName == accountheadmv.AccountHeadName.Trim() && u.AccountHeadID != accountheadmv.AccountHeadID).FirstOrDefault();
                if (checkaccounthead == null)
                {
                    var newaccounthead = new tblAccountHead();
                    newaccounthead.AccountHeadID = accountheadmv.AccountHeadID;
                    newaccounthead.AccountHeadName = accountheadmv.AccountHeadName;
                    newaccounthead.Code = accountheadmv.Code;
                    newaccounthead.UserID = userid;
                    DB.Entry(newaccounthead).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountHeads");
                }
                else
                {
                    ModelState.AddModelError("AccountHeadName", "Already Exist!");
                }
            }
            return View(accountheadmv);
        }

        public ActionResult AllFinanicalYears()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            { 
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            var list = new List<FinancialYearMV>();
            var financialyears = DB.tblFinancialYears.ToList();
            foreach (var financialyear in financialyears)
            {
                var addfinancialyear = new FinancialYearMV();
                addfinancialyear.FinancialYearID = financialyear.FinancialYearID;
                addfinancialyear.FinancialYear = financialyear.FinancialYear;
                addfinancialyear.StartDate = financialyear.StartDate;
                addfinancialyear.EndDate = financialyear.EndDate;
                addfinancialyear.IsActive = financialyear.IsActive;
                addfinancialyear.UserID = financialyear.UserID;
                addfinancialyear.CreatedBy = financialyear.tblUser.UserName;
                list.Add(addfinancialyear);
            }
            return View(list);
        }

        public ActionResult CreateFinancialYear()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }

            var financialyearmv = new FinancialYearMV();
            financialyearmv.UserID = userid;
            financialyearmv.StartDate = DateTime.Now;
            financialyearmv.EndDate = DateTime.Now.AddDays(365);
            return View(financialyearmv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFinancialYear(FinancialYearMV financialyearmv)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            if (ModelState.IsValid)
            {
                var checkfinancialyear = DB.tblFinancialYears.Where(u => u.FinancialYear == financialyearmv.FinancialYear.Trim()).FirstOrDefault();
                if (checkfinancialyear == null)
                {
                    var newfinancialyear = new tblFinancialYear();
                    newfinancialyear.FinancialYear = financialyearmv.FinancialYear;
                    newfinancialyear.StartDate = financialyearmv.StartDate;
                    newfinancialyear.EndDate = financialyearmv.EndDate;
                    newfinancialyear.IsActive = financialyearmv.IsActive;
                    newfinancialyear.UserID = userid;
                    DB.tblFinancialYears.Add(newfinancialyear);
                    DB.SaveChanges();
                    return RedirectToAction("AllFinanicalYears");
                }
                else
                {
                    ModelState.AddModelError("FinanicalYear", "Already Exist!");
                }
            }
            return View(financialyearmv);
        }

        public ActionResult EditFinancialYear(int? financialyearid)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            var financialyear = DB.tblFinancialYears.Find(financialyearid);

            var editfinancialyear = new FinancialYearMV();
            editfinancialyear.FinancialYearID = financialyear.FinancialYearID;
            editfinancialyear.FinancialYear = financialyear.FinancialYear;
            editfinancialyear.StartDate = financialyear.StartDate;
            editfinancialyear.EndDate = financialyear.EndDate;
            editfinancialyear.IsActive = financialyear.IsActive;
            editfinancialyear.UserID = financialyear.UserID;
            editfinancialyear.CreatedBy = financialyear.tblUser.UserName;
            return View(editfinancialyear);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFinancialYear(FinancialYearMV finacialyearmv)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if (usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            if (ModelState.IsValid)
            {
                var checkfinancialyear = DB.tblFinancialYears.Where(u => u.FinancialYear == finacialyearmv.FinancialYear.Trim() && u.FinancialYearID != finacialyearmv.FinancialYearID).FirstOrDefault();
                if (checkfinancialyear == null)
                {
                    var editfinancialyear = new tblFinancialYear();
                    editfinancialyear.FinancialYearID = finacialyearmv.FinancialYearID;
                    editfinancialyear.FinancialYear = finacialyearmv.FinancialYear;
                    editfinancialyear.StartDate = finacialyearmv.StartDate;
                    editfinancialyear.EndDate = finacialyearmv.EndDate;
                    editfinancialyear.IsActive = finacialyearmv.IsActive;
                    editfinancialyear.UserID = userid;
                    DB.Entry(editfinancialyear).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllFinanicalYears");
                }
                else
                {
                    ModelState.AddModelError("FinancialYear", "Already Exist!");
                }
            }
            return View(finacialyearmv);
        }
    }
}
    
