using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMS.Entities;

namespace UserManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult login()
        {
            AdminDTO dto = new AdminDTO();
            return View("Admin", dto);
        }
      public ActionResult Admin()
        {
            AdminDTO dto = new AdminDTO();
            dto.login = Request["login"];
            dto.Password = Request["password"];
            bool flag = UMS.BAL.UserBO.validateAdmin(dto.login, dto.Password);
            if (flag)
            {
               
                return Redirect("~/Admin/UserList");
            }
            else
            {
                Response.Write("<script> alert('Wrong Username or Password');</script>");
                return View(dto);
            }  
        }

        public ActionResult UserList()
        {
            List<UMS.Entities.UserDTO> list = new List<UMS.Entities.UserDTO>();
            list = UMS.BAL.UserBO.getAllUsers();
            return View("AdminHome", list);
        }
       
    }
}