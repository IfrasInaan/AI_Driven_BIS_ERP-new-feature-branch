using DatabaseLayer;
using ERP_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_App.Controllers
{
    public class UserController : Controller
    {
        private BussinessERPDbEntities1 DB = new BussinessERPDbEntities1();
        // GET: User
        public ActionResult AllUserTypes()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            if(usertypeid != 1)
            {
                return RedirectToAction("Admin", "Dashboard");
            }

            var list = new List<UserTypeMV>();
            var usertypes =  DB.tblUserTypes.ToList();
            foreach(var usertype in usertypes)
            {
                list.Add(new UserTypeMV() {UserTypeID = usertype.UserTypeID, UserType = usertype.UserType});
            }
            return View(list);
        }

        public ActionResult CreateUserType() 
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

            var usertypemv = new UserTypeMV();
            return View(usertypemv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUserType(UserTypeMV usertypemv) 
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
            if(ModelState.IsValid)
            {
                var checkusertype = DB.tblUserTypes.Where(u=>u.UserType == usertypemv.UserType.Trim()).FirstOrDefault();
                if(checkusertype == null)
                {
                    var newusertype = new tblUserType();
                    newusertype.UserType = usertypemv.UserType;
                    DB.tblUserTypes.Add(newusertype);
                    DB.SaveChanges();
                    return RedirectToAction("AllUserTypes");
                }
                else
                {
                    ModelState.AddModelError("UserType", "Already Exist");
                }

            }

            return View(usertypemv);
        }

        public ActionResult EditUserType(int? usertypeid)
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

            var editusertype = DB.tblUserTypes.Find(usertypeid);
            var usertypemv = new UserTypeMV();
            usertypemv.UserTypeID = editusertype.UserTypeID;
            usertypemv.UserType = editusertype.UserType;
            return View(usertypemv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserType(UserTypeMV usertypemv)
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
                var checkusertype = DB.tblUserTypes.Where(u => u.UserType == usertypemv.UserType.Trim() && u.UserTypeID == usertypemv.UserTypeID).FirstOrDefault();
                if (checkusertype == null)
                {
                    var editusertype = new tblUserType();
                    editusertype.UserType = usertypemv.UserType;
                    editusertype.UserTypeID = usertypemv.UserTypeID;
                    DB.Entry(editusertype).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllUserTypes");
                }
                else
                {
                    ModelState.AddModelError("UserType", "Already Exist");
                }

            }

            return View(usertypemv);
        }

    }
}