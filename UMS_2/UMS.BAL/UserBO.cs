using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UMS.Entities;

namespace UMS.BAL
{
    public static class UserBO
    {
        public static int Save(UserDTO dto)
        {
            return UMS.DAL.UserDAO.Save(dto);
        }
        public static UserDTO Edit(String login)
        {
             return UMS.DAL.UserDAO.Edit(login);
        }

        public static bool validateEmail(String email)
        {
            return UMS.DAL.UserDAO.validateEmail(email);
        }
        public static UserDTO Home(String login)
        {
            return UMS.DAL.UserDAO.Home(login);
        }

        public static bool login(String login, String pwd)
        {
            return UMS.DAL.UserDAO.login(login, pwd);
        }

        public static String resetPassword(String email, String pwd)
        {
            return UMS.DAL.UserDAO.resetPassword(email, pwd);
        }

        public static bool Email(String toEmailAddress, String subject, String body)
        {
            
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                MailAddress to = new MailAddress(toEmailAddress);
                mail.To.Add(to);
                MailAddress from = new MailAddress("arbabkhan7901@gmail.com", "Admin");
                mail.From = from;
                mail.Subject = subject;
                mail.Body = body;
                var sc = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new System.Net.NetworkCredential("arbabkhan7901@gmail.com", "Lovefamily1997"),
                    EnableSsl = true
                };
                sc.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool validateAdmin(String login, String pwd)
        {
            return UMS.DAL.UserDAO.ValidateAdmin(login, pwd);
        }

        public static List<UMS.Entities.UserDTO> getAllUsers()
        {
            return UMS.DAL.UserDAO.getAllUsers();
        }
    }
}
