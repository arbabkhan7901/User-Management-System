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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainScreen m = new MainScreen();
            this.Close();
            m.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            BAL b = new BAL();
            if(login=="" || password=="")
            {
                MessageBox.Show("Cannot accept empty values.");
            }

            else if(b.isUserExist(login, "",""))
            {
                UserDTO u = b.getUser(login);
                Home h = new Home(u);
                this.Close();
                h.Show();
            }
            else
            {
                MessageBox.Show("Doesn't Exist.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string email = textBox3.Text;
            if(email=="")
            {
                MessageBox.Show("Cannot Accept empty.");
            }
            else
            {
                BAL b = new BAL();
                if(b.isUserExist("","",email))
                {
                    Random r = new Random();
                    int c = r.Next(100, 1000);
                    String code = c.ToString();
                    if(b.sendEmail(email, "Recovery Code","Your recovery code is "+code))
                    {
                        UserDTO user = new UserDTO();
                        user = b.getUserByEmail(email);
                        EnterResetCode erc = new EnterResetCode(code,user);
                        this.Close();
                        erc.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Doesn't Exist.");
                }
                
            }
        }
    }
}
