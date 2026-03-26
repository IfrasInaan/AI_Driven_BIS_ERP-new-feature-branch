using DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class EmployeeController : Controller
    {
        BussinessERPDbEntities1 DB = new BussinessERPDbEntities1();
        // GET: Employee
        public ActionResult BranchEmployees()
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
            var employees = DB.tblEmployees.Where(e => e.CompanyID == companyid && e.BranchID == branchid).ToList();
            var list = new List<EmployeeMV>();
            foreach (var employee in employees)
            {
                list.Add(new EmployeeMV()
                {
                    Address = employee.Address,
                    BranchID = employee.BranchID,
                    CNIC = employee.CNIC,
                    CompanyID = employee.CompanyID,
                    ContactNo = employee.ContactNo,
                    Description = employee.Description,
                    Designation = employee.Designation,
                    Email = employee.Email,
                    EmployeeID = employee.EmployeeID,
                    MonthlySalary = employee.MonthlySalary,
                    Name = employee.Name,
                    Photo = employee.Photo,
                    UserID = employee.UserID
                });
            }
            return View(list);
        }

        public ActionResult NewBranchEmployee()
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
            var employee = new EmployeeMV();
            employee.CompanyID = companyid;
            employee.BranchID = branchid;
            employee.UserID = 0;
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewBranchEmployee(EmployeeMV employeemv)
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

            try
            {
                if (ModelState.IsValid)
                {
                    var checkemployee = DB.tblEmployees.Where(e => e.CNIC == employeemv.CNIC.Trim()).FirstOrDefault();
                    if (checkemployee == null)
                    {
                        var newemployee = new tblEmployee();
                        newemployee.Address = employeemv.Address;
                        newemployee.BranchID = branchid;
                        newemployee.CNIC = employeemv.CNIC;
                        newemployee.CompanyID = companyid;
                        newemployee.ContactNo = employeemv.ContactNo;
                        newemployee.Description = employeemv.Description;
                        newemployee.Designation = employeemv.Designation;
                        newemployee.Email = employeemv.Email;
                        newemployee.MonthlySalary = employeemv.MonthlySalary;
                        newemployee.Name = employeemv.Name;
                        newemployee.Photo = "~/Content/Template/img/user/employee.png";
                        newemployee.UserID = employeemv.UserID;
                        DB.tblEmployees.Add(newemployee);
                        DB.SaveChanges();
                        return RedirectToAction("BranchEmployees");
                    }
                    else
                    {
                        ModelState.AddModelError("CNIC", "Already Exist!");
                    }
                }
                return View(employeemv);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", "Must be Filled All Field with correct data!");
                return View(employeemv);
            }
        }

        public ActionResult EditBranchEmployee(int? employeeid)
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
            var employee = DB.tblEmployees.Find(employeeid);
            var editemployee = new EmployeeMV();
            editemployee.Address = employee.Address;
            editemployee.BranchID = employee.BranchID;
            editemployee.CNIC = employee.CNIC;
            editemployee.CompanyID = employee.CompanyID;
            editemployee.ContactNo = employee.ContactNo;
            editemployee.Description = employee.Description;
            editemployee.Designation = employee.Designation;
            editemployee.Email = employee.Email;
            editemployee.EmployeeID = employee.EmployeeID;
            editemployee.MonthlySalary = employee.MonthlySalary;
            editemployee.Name = employee.Name;
            editemployee.Photo = employee.Photo;
            editemployee.UserID = employee.UserID;
            return View(editemployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBranchEmployee(EmployeeMV employeemv)
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

            try
            {
                if (ModelState.IsValid)
                {
                    var checkemployee = DB.tblEmployees.Where(e => e.CNIC == employeemv.CNIC.Trim() && e.EmployeeID != employeemv.EmployeeID).FirstOrDefault();
                    if (checkemployee == null)
                    {
                        var editemployee = DB.tblEmployees.Find(employeemv.EmployeeID);
                        editemployee.Address = employeemv.Address;
                        editemployee.CNIC = employeemv.CNIC;
                        editemployee.ContactNo = employeemv.ContactNo;
                        editemployee.Description = employeemv.Description;
                        editemployee.Designation = employeemv.Designation;
                        editemployee.Email = employeemv.Email;
                        editemployee.MonthlySalary = employeemv.MonthlySalary;
                        editemployee.Name = employeemv.Name;
                        //newemployee.Photo = "~/Content/Template/img/user/employee.png";
                        DB.Entry(editemployee).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        return RedirectToAction("BranchEmployees");
                    }
                    else
                    {
                        ModelState.AddModelError("CNIC", "Already Exist!");
                    }
                }
                return View(employeemv);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", "Must be Filled All Field with correct data!");
                return View(employeemv);
            }
        }

        public ActionResult ShowBranchFocalPerson(int? branchID)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var focalperson = new FocalPersonMV();
            focalperson.employeeMV = new EmployeeMV();
            focalperson.userMV = new UserMV();
            var employees = DB.tblEmployees.Where(e => e.BranchID == branchID && e.Designation.Contains("Focal"));
            foreach (var employee in employees)
            {
                var user = DB.tblUsers.Where(u => u.UserID == employee.UserID && u.IsActive == true).FirstOrDefault();
                if (user != null)
                {
                    focalperson.CompanyID = employee.CompanyID;
                    focalperson.BranchID = employee.BranchID;
                    // Employee Detials
                    focalperson.employeeMV.Name = employee.Name;
                    focalperson.employeeMV.ContactNo = employee.ContactNo;
                    focalperson.employeeMV.Photo = employee.Photo;
                    focalperson.employeeMV.Email = employee.Email;
                    focalperson.employeeMV.Address = employee.Address;
                    focalperson.employeeMV.CNIC = employee.CNIC;
                    focalperson.employeeMV.Designation = employee.Designation;
                    focalperson.employeeMV.Description = employee.Description;
                    focalperson.employeeMV.MonthlySalary = employee.MonthlySalary;
                    // User Detials
                    focalperson.userMV.UserID = user.UserID;
                    focalperson.userMV.UserType = user.tblUserType.UserType;
                    focalperson.userMV.FullName = user.FullName;
                    focalperson.userMV.Email = user.Email;
                    focalperson.userMV.ContactNo = user.ContactNo;
                    focalperson.userMV.UserName = user.UserName;
                    focalperson.userMV.IsActive = user.IsActive;
                    focalperson.userMV.Address = user.Address;
                }
            }
            return View(focalperson);
        }


        public ActionResult CreateBranchFocalPerson(int? branchID)
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
            var branch = DB.tblBranches.Find(branchID);
            var focalperonmv = new FocalPersonMV();
            focalperonmv.employeeMV = new EmployeeMV();
            focalperonmv.userMV = new UserMV();
            focalperonmv.BranchID = branch.BranchID;
            focalperonmv.CompanyID = branch.CompanyID;
            focalperonmv.userMV.UserTypeID = 3;
            return View(focalperonmv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBranchFocalPerson(FocalPersonMV focalpersonmv)
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
                    if (ModelState.IsValid)
                    {
                        var newuser = new tblUser();
                        newuser.UserTypeID = 3;
                        newuser.FullName = focalpersonmv.employeeMV.Name;
                        newuser.Email = focalpersonmv.employeeMV.Email;
                        newuser.ContactNo = focalpersonmv.employeeMV.ContactNo;
                        newuser.UserName = focalpersonmv.userMV.UserName;
                        newuser.Password = focalpersonmv.userMV.Password;
                        newuser.IsActive = true;
                        newuser.Address = focalpersonmv.employeeMV.Address;
                        DB.tblUsers.Add(newuser);
                        DB.SaveChanges();
                        var newemployee = new tblEmployee();
                        newemployee.Address = focalpersonmv.employeeMV.Address;
                        newemployee.BranchID = focalpersonmv.BranchID;
                        newemployee.CNIC = focalpersonmv.employeeMV.CNIC;
                        newemployee.CompanyID = focalpersonmv.CompanyID;
                        newemployee.ContactNo = focalpersonmv.employeeMV.ContactNo;
                        newemployee.Description = focalpersonmv.employeeMV.Description;
                        newemployee.Designation = focalpersonmv.employeeMV.Designation;
                        newemployee.Email = focalpersonmv.employeeMV.Email;
                        newemployee.MonthlySalary = focalpersonmv.employeeMV.MonthlySalary;
                        newemployee.Name = focalpersonmv.employeeMV.Name;
                        newemployee.Photo = "~/Content/Template/img/user/employee.png";
                        newemployee.UserID = newuser.UserID;
                        DB.tblEmployees.Add(newemployee);
                        DB.SaveChanges();
                        transaction.Commit();
                    }
                    return RedirectToAction("AllCompanyBranchs", "Branch");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ModelState.AddModelError("Name", "Must be Filled All Field with correct data!");
                    return View(focalpersonmv);
                }
            }
        }
    }
}