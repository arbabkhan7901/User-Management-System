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
    public partial class Home : Form
    {
        UserDTO u = null;
        private int AdminFlag=0;
        public Home()
        {
            InitializeComponent();
        }

        public Home(UserDTO user)
        {
            InitializeComponent();
            u = user;
        }

        public Home(UserDTO user,int flag)
        {
            InitializeComponent();
            u = user;
            AdminFlag = flag;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainScreen m = new MainScreen();
            this.Close();
            m.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewUser n = new NewUser(u,AdminFlag);
            this.Close();
            n.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            if (u != null)
            {
                label1.Text = u.login;
                string applicationBasePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                string filePath = applicationBasePath + @"\images\" + u.imageName;
                pictureBox1.Image = Image.FromFile(filePath);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
