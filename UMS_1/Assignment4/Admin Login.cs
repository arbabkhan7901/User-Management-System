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
    public partial class Admin_Login : Form
    {
        public Admin_Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string pass = textBox2.Text;
            if(login==""||pass=="")
            {
                MessageBox.Show("Cannot accept empty values.");
            }
            else
            {
                BAL b = new BAL();
                if(b.isAdminExist(login,pass))
                {
                    Admin_Home ah = new Admin_Home();
                    this.Close();
                    ah.Show();
                }
                else
                {
                    MessageBox.Show("Doesnot Exist");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainScreen m = new MainScreen();
            this.Close();
            m.Show();
        }
    }
}
