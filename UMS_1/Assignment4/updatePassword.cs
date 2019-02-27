using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment4
{
    public partial class updatePassword : Form
    {
        private UserDTO user = new UserDTO();
        public updatePassword()
        {
            InitializeComponent();
        }
        public updatePassword(UserDTO u)
        {
            InitializeComponent();
            user = u;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pass = textBox1.Text;
            if(pass=="")
            {
                MessageBox.Show("Cannot Accept Empty.");
            }
            else
            {
                BAL b = new BAL();
                user.password = pass;
                if(b.UpdateUser(user))
                {
                    Home h = new Home(user);
                    this.Close();
                    h.Show();
                }
            }
        }
    }
}
