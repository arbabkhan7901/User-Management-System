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
    public partial class EnterResetCode : Form
    {
        private String Code;
        private UserDTO user = new UserDTO();
        public EnterResetCode()
        {
            InitializeComponent();
        }

        public EnterResetCode(String code,UserDTO u)
        {
            InitializeComponent();
            Code = code;
            user = u;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String enteredCode = textBox1.Text;
            if(enteredCode=="")
            {
                MessageBox.Show("Cannot Accept Empty.");
            }
            else if(enteredCode==Code)
            {
                updatePassword up = new updatePassword(user);
                this.Close();
                up.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Code");
            }
        }
    }
}
