using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
   public class BAL
    {

        public bool isUserExist(string login,string nic,string email)
        {
            string connString = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "select * from dbo.Users where Login='"+login+"' OR NIC='"+nic+"' OR Email='"+email+"' ";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    return true;
                return false;
            }

        }

        public bool isAdminExist(string login, string pass)
        {
            string connString = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "select * from dbo.Admin where Login='" + login + "' AND Password='" + pass + "'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    return true;
                return false;
            }

        }

        public bool saveUser(UserDTO u)
        {
            string connstring = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                string query = String.Format(@"insert into dbo.Users Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", u.name,u.login,u.password,u.gender,u.address,u.age,u.nic,u.dob,u.cricket,u.hockey,u.chess,u.imageName,u.createdOn,u.email);
                SqlCommand command = new SqlCommand(query,conn);

                int rec = command.ExecuteNonQuery();
                if (rec > 0)
                    return true;
           }
            return false;
        }


        public UserDTO getUser(string login)
        {
            UserDTO u = new UserDTO();
            string connstring = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                string query = "select * from dbo.Users where Login='" + login + "'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                u.login = login;
                u.name = reader.GetString(reader.GetOrdinal("Name"));
                u.password = reader.GetString(reader.GetOrdinal("Password"));
                char g= Convert.ToChar(reader.GetString(reader.GetOrdinal("Gender")));
                u.gender = g;
                u.address = reader.GetString(reader.GetOrdinal("Address"));
                u.age = reader.GetInt32(reader.GetOrdinal("Age"));
                u.nic = reader.GetString(reader.GetOrdinal("NIC"));
                u.dob = reader.GetDateTime(reader.GetOrdinal("DOB"));
                u.email = reader.GetString(reader.GetOrdinal("Email"));
                u.cricket = Convert.ToInt16(reader.GetValue(9));
                u.hockey = Convert.ToInt16(reader.GetValue(10));
                u.chess = Convert.ToInt16(reader.GetValue(11));
                u.imageName = reader.GetString(reader.GetOrdinal("ImageName"));
                u.createdOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"));
            }
            return u;
        }

        public bool UpdateUser(UserDTO u)
        {
            string connstring = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                string query = String.Format(@"Update dbo.Users set Name='{0}',Password='{1}',Gender='{2}',Address='{3}',Age='{4}',NIC='{5}',DOB='{6}',IsCricket='{7}',Hockey='{8}',Chess='{9}',ImageName='{10}',CreatedOn='{11}',Email='{12}' where Login='"+u.login+"'",u.name,u.password,u.gender,u.address,u.age,u.nic,u.dob,u.cricket,u.hockey,u.chess,u.imageName,u.createdOn,u.email);
                SqlCommand command = new SqlCommand(query,conn);

                int rec = command.ExecuteNonQuery();
                if (rec > 0)
                    return true;
           }
            return false;
        }

        public UserDTO getUserByEmail(string email)
        {
            UserDTO u = new UserDTO();
            string connstring = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                string query = "select * from dbo.Users where Email='" + email + "'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                u.login = reader.GetString(reader.GetOrdinal("Login"));
                u.name = reader.GetString(reader.GetOrdinal("Name"));
                u.password = reader.GetString(reader.GetOrdinal("Password"));
                char g = Convert.ToChar(reader.GetString(reader.GetOrdinal("Gender")));
                u.gender = g;
                u.address = reader.GetString(reader.GetOrdinal("Address"));
                u.age = reader.GetInt32(reader.GetOrdinal("Age"));
                u.nic = reader.GetString(reader.GetOrdinal("NIC"));
                u.dob = reader.GetDateTime(reader.GetOrdinal("DOB"));
                u.email = email;
                u.cricket = Convert.ToInt16(reader.GetValue(9));
                u.hockey = Convert.ToInt16(reader.GetValue(10));
                u.chess = Convert.ToInt16(reader.GetValue(11));
                u.imageName = reader.GetString(reader.GetOrdinal("ImageName"));
                u.createdOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"));
            }
            return u;
        }


        public bool sendEmail(String toEmailAddress, String subject,String body)
        {
            try
            {
                String fromDisplayEmail = "bsef15m014@gmail.com";
                String fromPassword = "punjabuniversity";
                String fromDisplayName = "ADMIN";
                MailAddress fromAddress =new MailAddress(fromDisplayEmail,fromDisplayName);
                MailAddress toAddress = new MailAddress(toEmailAddress);
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address,fromPassword)


                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
            {
                    smtp.Send(message);
            }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


        public DataTable GetUsersGrid()
        {
            DataTable dt = new DataTable("Users");

            DataColumn dc = new DataColumn("UserID", typeof(System.Int32));
            dt.Columns.Add(dc);

            dc = new DataColumn("Name", typeof(System.String));
            dt.Columns.Add(dc);

            dc = new DataColumn("Login", typeof(System.String));
            dt.Columns.Add(dc);

            dc = new DataColumn("Address", typeof(System.String));
            dt.Columns.Add(dc);

            dc = new DataColumn("Age", typeof(System.Int16));
            dt.Columns.Add(dc);

            DataRow dr;

            string connString = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment4; User Id=sa; Password=12345;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "select * from dbo.Users";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    dr = dt.NewRow();
                    dr["UserID"] = reader.GetInt32(reader.GetOrdinal("UserID"));
                    dr["Name"]= reader.GetString(reader.GetOrdinal("Name"));
                    dr["Login"] = reader.GetString(reader.GetOrdinal("Login"));
                    dr["Address"] = reader.GetString(reader.GetOrdinal("Address"));
                    dr["Age"] = reader.GetInt32(reader.GetOrdinal("Age"));
                    dt.Rows.Add(dr);
                }

            }

            return dt;
        }
    }
}
