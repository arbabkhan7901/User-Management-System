using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Entities
{
   public class UserDTO
    {
        public int UserID { get; set;}
        public String Name { get; set; }
        public String login { get; set; }
        public String password { get; set; }
        public char gender { get; set; }
        public String address { get; set; }
        public int age { get; set; }
        public String NIC { get; set; }
        public DateTime DOB { get; set; }
        public Boolean isCricket { get; set; }
        public Boolean Hockey { get; set; }
        public Boolean Chess { get; set; }
        public String ImageName { get; set; }
        public DateTime CreatedOn { get; set; }
        public String Email { get; set; }
    }
}
