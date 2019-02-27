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
    public partial class Admin_Home : Form
    {
        public Admin_Home()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainScreen m = new MainScreen();
            this.Close();
            m.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           if(e.ColumnIndex==5)
            {
                string login = (string)dataGridView1.CurrentRow.Cells[2].Value;
                BAL b = new BAL();
                UserDTO u = new UserDTO();
                u = b.getUser(login);
                int adminFlag = 1;
                Home h = new Home(u,adminFlag);
                this.Close();
                h.Show();


            }
        }

        private void Admin_Home_Load(object sender, EventArgs e)
        {
            BAL b = new BAL();
            DataTable dt = b.GetUsersGrid();
            dataGridView1.DataSource = dt;
        }
    }
}
