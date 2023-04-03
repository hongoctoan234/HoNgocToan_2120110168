using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        TBDTDBContext db = new TBDTDBContext();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            if (!Session["UserAdmin"].Equals(""))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.ErrorLogin = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            string strError = "";
            //string username = field["username"];
            //string password = XString.ToMD5(field["password"]);
            User user = db.Users.Where(m => m.Status == 1 && m.Access == 1 && m.UserName == username && m.Password == password).FirstOrDefault();

            if (user==null)
            {
                strError = "Thông tin đăng nhập không chính xác";
               
            }
            else 
            {
               
                    Session["UserId"] = user.Id;
                    Session["UserAdmin"] = user.UserName;
                    Session["FullName"] = user.FullName;
                    return RedirectToAction("Index", "Dashboard");
              
            }
            ViewBag.ErrorLogin = strError;
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserId"] = "";
            Session["UserAdmin"] = "";
            Session["FullName"] = "";
            return RedirectToAction("Login", "Auth");

        }
    }
}