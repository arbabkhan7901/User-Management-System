using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserManagementSystem.Controllers
{
    public class ExistingController : Controller
    {
        public ActionResult Exist()
        {
            return View();
        }
        public ActionResult Email()
        {
            String email = Request["email"];
            bool flag1 = UMS.BAL.UserBO.validateEmail(email);
            if (flag1)
            {
                TempData["email"] = email;
                String subject = "Reset Password Code";
                Random rnd = new Random();
                int num = rnd.Next(12000, 90000);
                Session["Code"] = num.ToString();
                String body = num.ToString();
                bool flag = UMS.BAL.UserBO.Email(email, subject, body);
                return View();
            }
            else
            {
                Response.Write("<script> alert('Wrong Email');</script>");
                return View("Exist");
            }
            
        }
        public ActionResult UpdatePassword()
        {
            String code = Request["code"];
            String temp = (String)Session["code"];
            if (code == temp)
                return View();
            else
            {
                Response.Write("<script> alert('Wrong Code');</script>");
                return View("Email");
            }
                
        }
        public ActionResult login()
        {
            String login = Request["login"];
            String pwd = Request["password"];
            bool flag = UMS.BAL.UserBO.login(login,pwd);
            if (!flag)
            {
                Response.Write("<script> alert('Wrong username or password');</script>");
            }
            else
            {
                TempData["login"] = login;
                return RedirectToAction("Home", "User", null); ;
            }
            return View("Exist");
        }
        public ActionResult ResetPassword()
        {
            String pwd = Request["code"];
            String email = (String)TempData["email"];
            String login = UMS.BAL.UserBO.resetPassword(email, pwd);
            TempData["login"] = login;
            return RedirectToAction("Home", "User", null);
        }
    }
}