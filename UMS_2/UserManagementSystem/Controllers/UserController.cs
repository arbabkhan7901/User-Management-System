using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using UMS.DAL;
using UMS.Entities;

namespace UserManagementSystem.Controllers
{
    public class UserController : Controller
    {
        public ActionResult start()
        {
            return View();
        }
        public ActionResult logout()
        {
            return View("start");
        }
        [HttpPost]
        public ActionResult Save()
        { 
            UserDTO dto = new UserDTO();
            var name = "";
            if (Request.Files["image"] != null)
            {
                var file = Request.Files["image"];
                if (file.FileName != "")
                {
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    name = Guid.NewGuid().ToString() + ext;
                    var rootPath = Server.MapPath("~/UploadedFiles");
                    var fileSavePath = System.IO.Path.Combine(rootPath, name);
                    file.SaveAs(fileSavePath);
                    
                }
            }
            if (name == "")
                dto.ImageName = (String)Session["image"];
            else
                dto.ImageName = name;
            dto.UserID = (Int32)Session["UserID"];
            dto.Name = Request["Name"];
            dto.login = Request["login"];
            dto.password = Request["password"];
            String str = Request["gender"];
            if (Request["gender"] == "male")
                dto.gender = 'M';
            else
                dto.gender = 'F';
            dto.address = Request["address"];
            dto.age = Convert.ToInt32(Request["age"]);
            dto.NIC = Request["NIC"];
            dto.DOB = Convert.ToDateTime(Request["DOB"]);
            if (Request["isCricket"] != null)
                dto.isCricket = true;
            else
                dto.isCricket = false;
            if (Request["Hockey"] != null)
                dto.Hockey = true;
            else
                dto.Hockey = false;
            if (Request["Chess"] != null)
                dto.Chess = true;
            else
                dto.Chess = false;

            dto.CreatedOn = DateTime.Now;
            dto.Email = Request["Email"];
            int count = UMS.BAL.UserBO.Save(dto);
            if (count == -1)
                Response.Write("<script> alert('User already Exist');</script>");
            else if (count > 0)
                Response.Write("<script> alert('Data Inserted Sucessfully');</script>");
            else if (count == -2)
                Response.Write("<script> alert('Data Updated Sucessfully');</script>");
            else
                Response.Write("<script> alert('Data Updated Sucessfully');</script>");
            if (count != -1)
                return View(dto);
            else
                return View("New", dto);
        }

        public ActionResult New()
        {
            UserDTO dto = new UserDTO();
            return View(dto);
        }

        public ActionResult Button()
        {
            UserDTO dto = new UserDTO();
            var name = "";
            if (Request.Files["image"] != null)
            {
                var file = Request.Files["image"];
                if (file.FileName != "")
                {
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    name = Guid.NewGuid().ToString() + ext;
                    var rootPath = Server.MapPath("~/UploadedFiles");
                    var fileSavePath = System.IO.Path.Combine(rootPath, name);
                    file.SaveAs(fileSavePath);

                }
            }
            dto.ImageName = name;
            dto.UserID = (Int32)Session["UserID"];
            dto.Name = Request["Name"];
            dto.login = Request["login"];
            dto.password = Request["password"];
            String str = Request["gender"];
            if (Request["gender"] == "male")
                dto.gender = 'M';
            else
                dto.gender = 'F';
            dto.address = Request["address"];
            dto.age = Convert.ToInt32(Request["age"]);
            dto.NIC = Request["NIC"];
            dto.DOB = Convert.ToDateTime(Request["DOB"]);
            if (Request["isCricket"] != null)
                dto.isCricket = true;
            else
                dto.isCricket = false;
            if (Request["Hockey"] != null)
                dto.Hockey = true;
            else
                dto.Hockey = false;
            if (Request["Chess"] != null)
                dto.Chess = true;
            else
                dto.Chess = false;

            dto.CreatedOn = DateTime.Now;
            dto.Email = Request["Email"];
            int id = (Int32)Session["UserID"];
            if (Session["isAdmin"] != null)
            {
                return RedirectToAction("UserList", "Admin", null);
            }
            if (id > 0)
            {
                return View("Save", dto);
            }
            else
                return View("Start");
        }
        public ActionResult Edit()
        {
            String login = (String)Session["login"];
            UserDTO dto = UMS.BAL.UserBO.Edit(login);
            ViewBag.date = dto.DOB;
            return View("New", dto);
        }

        public ActionResult Home()
        {
            String login = (String)TempData["login"];
            UserDTO dto = UMS.BAL.UserBO.Home(login);
            return View("Save", dto);
        }
    }
}