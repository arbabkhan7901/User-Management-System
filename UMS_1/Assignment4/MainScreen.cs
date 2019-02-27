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
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            NewUser n = new NewUser();
            n.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            this.Hide();
            l.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Admin_Login a = new Admin_Login();
            this.Hide();
            a.Show();
        }
    }
}
