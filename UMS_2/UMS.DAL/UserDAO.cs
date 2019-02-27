using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Entities;
using System.Web;
using System.Net.Http;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;

namespace UMS.DAL
{
    public static class UserDAO
    {
        public static int Save(UserDTO u)
        {
            String query = "";
            int count = 0;
            try
            {
                
                if (u.UserID > 0)
                {
                    query = String.Format(@"Update dbo.Users set Name='{0}',Password='{1}',Gender='{2}',Address='{3}',Age='{4}',NIC='{5}',DOB='{6}',IsCricket='{7}',Hockey='{8}',Chess='{9}',ImageName='{10}',CreatedOn='{11}',Email='{12}' where Login='" + u.login + "'", u.Name, u.password, u.gender, u.address, u.age, u.NIC, u.DOB, u.isCricket, u.Hockey, u.Chess, u.ImageName, u.CreatedOn, u.Email);
                    using (DBHelper helper = new DBHelper())
                    {
                        count = helper.ExecuteQuery(query);
                        if (count > 0)
                            return -2;
                    }
                }
                else
                {
                    String sql = String.Format(@"Select * from dbo.users where login = '"+u.login+"' or email = '"+u.Email+"'");
                    DBHelper db = new DBHelper();
                    SqlDataReader reader = db.ExecuteReader(sql);
                    if (reader.Read())
                    {
                        return -1;
                    }
                    else
                    {
                        query = String.Format(@"insert into dbo.users 
                        (Name, Login, Password, Gender, Address, Age, NIC, DOB, IsCricket, Hockey, Chess, ImageName, CreatedOn, Email) 
                        Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')",
                        u.Name, u.login, u.password, u.gender, u.address, u.age, u.NIC, u.DOB, u.isCricket, u.Hockey, u.Chess, u.ImageName,
                        u.CreatedOn, u.Email);
                        using (DBHelper helper = new DBHelper())
                        {
                             count = helper.ExecuteQuery(query);
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return count;
        }

        public static UserDTO Edit(String login)
        {
            String query = "";
            SqlDataReader reader;
            UserDTO obj = new UserDTO();
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                query = String.Format(@"Select * from dbo.users where login = '" + login + "'");
                using (DBHelper helper = new DBHelper())
                {
                    reader = helper.ExecuteReader(query);

                    dt.Load(reader);
                }
                foreach (DataRow row in dt.Rows)
                {
                    obj.UserID = (Int32)row["UserId"];
                    obj.Name = row["Name"].ToString();
                    obj.login = row["login"].ToString();
                    obj.password = row["password"].ToString();
                    obj.gender = Convert.ToChar( row["gender"]);
                    obj.address = row["address"].ToString();
                    obj.age = (Int32) row["age"];
                    obj.NIC = row["NIC"].ToString();
                    obj.DOB = (DateTime) row["DOB"];
                    obj.isCricket = (bool) row["isCricket"];
                    obj.Hockey = (bool) row["hockey"];
                    obj.Chess = (bool) row["chess"];
                    obj.ImageName = row["ImageName"].ToString();
                    obj.CreatedOn = (DateTime) row["CreatedOn"];
                    obj.Email = row["email"].ToString();

                }
                return obj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return obj;
        }

        public static UserDTO Home(String login)
        {
            UserDTO obj = new UserDTO();
            String query = String.Format(@"Select * from dbo.users where login = '"+login+"'");
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    SqlDataReader reader = helper.ExecuteReader(query);
                    if (reader.Read())
                    {
                        obj.UserID = (Int32)reader.GetValue(0);
                        obj.Name = (String)reader.GetValue(1);
                        obj.login = (String)reader.GetValue(2);
                        obj.password = (String)reader.GetValue(3);
                        obj.gender = (char)reader.GetOrdinal("gender");
                        obj.address = (String)reader.GetValue(5);
                        obj.age = (Int32)reader.GetValue(6);
                        obj.NIC = (String)reader.GetValue(7);
                        obj.DOB = (DateTime)reader.GetValue(8);
                        obj.isCricket = (bool)reader.GetValue(9);
                        obj.Hockey = (bool)reader.GetValue(10);
                        obj.Chess = (bool)reader.GetValue(11);
                        obj.ImageName = (String)reader.GetValue(12);
                        obj.CreatedOn = (DateTime)reader.GetValue(13);
                        obj.Email = (String)reader.GetValue(14);
                    }
                }
                return obj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return obj;
        }

        private static String getLogin(String email)
        {
            String login = "";
            String sql = String.Format(@"Select * from dbo.users where email = '" + email + "'");
            try
            {
                using (DBHelper db = new DBHelper())
                {
                    SqlDataReader reader = db.ExecuteReader(sql);
                    if (reader.Read())
                    {
                        login = (String)reader.GetValue(1);
                        return login;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return login;
        }
        public static String resetPassword(String email, String pwd)
        {
            int count = 0;
            String login = "";
            String query = String.Format(@"Update dbo.users set password = '{0}' where email = '"+email+"'", pwd);
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    count = helper.ExecuteQuery(query);
                    if(count > 0)
                    {
                        login = getLogin(email);
                        return login;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return login;
        }

        public static bool validateEmail(String email)
        {
            String query = String.Format(@"Select * from dbo.users where email = '"+email+"'");
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    SqlDataReader reader = helper.ExecuteReader(query);
                    if (reader.Read())
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
        public static bool login(String login, String pwd)
        {
            String query = String.Format(@"Select * from dbo.users where login = '"+login+"' and password = '"+pwd+"'");
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    SqlDataReader reader = helper.ExecuteReader(query);
                    if (reader.Read())
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            return false;
        }
        public static bool ValidateAdmin(String login, String password)
        {
            String query = String.Format(@"Select * from dbo.admin where login = '"+login+"' and password = '"+password+"'");
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    SqlDataReader reader = helper.ExecuteReader(query);
                    if (reader.Read())
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public static List<UserDTO> getAllUsers()
        {
            List<UserDTO> list = new List<UserDTO>();
            String query = String.Format(@"Select * from dbo.users");
            using (DBHelper helper = new DBHelper())
            {
                SqlDataReader reader = helper.ExecuteReader(query);
                while (reader.Read())
                {
                    UserDTO obj = new UserDTO();
                    obj.UserID = (Int32)reader.GetValue(0);
                    obj.Name = (String)reader.GetValue(1);
                    obj.login = (String)reader.GetValue(2);
                    obj.password = (String)reader.GetValue(3);
                    obj.gender = (char)reader.GetOrdinal("gender");
                    obj.address = (String)reader.GetValue(5);
                    obj.age = (Int32)reader.GetValue(6);
                    obj.NIC = (String)reader.GetValue(7);
                    obj.DOB = (DateTime)reader.GetValue(8);
                    obj.isCricket = (bool)reader.GetValue(9);
                    obj.Hockey = (bool)reader.GetValue(10);
                    obj.Chess = (bool)reader.GetValue(11);
                    obj.ImageName = (String)reader.GetValue(12);
                    obj.CreatedOn = (DateTime)reader.GetValue(13);
                    obj.Email = (String)reader.GetValue(14);
                    list.Add(obj);
                }

                return list;
            }
        }
    }
}
