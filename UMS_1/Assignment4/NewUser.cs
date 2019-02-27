
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
    public partial class NewUser : Form
    {
        private UserDTO editUser = null;
        private int AdminFlag=0;
        public NewUser()
        {
            InitializeComponent();
        }

        public NewUser(UserDTO u)
        {
            InitializeComponent();
            editUser = u;
        }

        public NewUser(UserDTO u,int flag)
        {
            InitializeComponent();
            editUser = u;
            AdminFlag = flag;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string login = textBox4.Text;
            string password = textBox2.Text;
            string email = textBox3.Text;
            string address = textBox5.Text;
            string gender = comboBox1.Text;
            string nic = maskedTextBox1.Text;
            if ( name == "")
            {
                MessageBox.Show("Name cannot be empty.");
            }

            else if (login == "")
            {
                MessageBox.Show("Login cannot be empty.");
            }

            else if (password == "")
            {
                MessageBox.Show("Password cannot be empty.");
            }
            else if (address == "")
            {
                MessageBox.Show("Address cannot be empty.");
            }
            else if (email == "")
            {
                MessageBox.Show("Email cannot be empty.");
            }
            else if ( gender == "")
            {
                MessageBox.Show("Gender cannot be empty.");
            }
            else if (!maskedTextBox1.MaskCompleted)
            {
                MessageBox.Show("NIC cannot be empty.");
            }
            else if (pictureBox1.Image == null)
            {
                MessageBox.Show("Picture cannot be empty.");
            }
            else
            {
                string applicationBasePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                string pathToSaveImage=applicationBasePath+@"\images\";
                string uniqueName = Guid.NewGuid().ToString() + ".jpg";
                string imgPath = pathToSaveImage + uniqueName;
                pictureBox1.Image.Save(imgPath);

                char g;
                UserDTO user = new UserDTO();
                user.login = login;
                user.name = name;
                user.password = password;
                user.address = address;
                if (gender == "Male")
                    g = 'M';
                else
                    g = 'F';
                user.gender = g;
                user.age = Convert.ToInt32(numericUpDown1.Value);
                user.nic = nic;
                user.dob = dateTimePicker1.Value;
                user.cricket = 0; user.hockey = 0; user.chess = 0;

                if (checkBox1.Checked)
                    user.cricket = 1;
                if (checkBox2.Checked)
                    user.hockey = 1;
                if (checkBox3.Checked)
                    user.chess = 1;

                user.imageName = pictureBox1.Name;
                user.createdOn = DateTime.Now;
                user.imageName = uniqueName;
                user.email = email;

                BAL b = new BAL();
                if (editUser == null)
                {
                    if (b.isUserExist(login, nic, email))
                    {
                        MessageBox.Show("User Already Exist");
                    }
                    else
                    {
                        if (b.saveUser(user))
                        {
                            MessageBox.Show("Added");
                            Home h = new Home(user);
                            this.Close();
                            h.Show();
                        }
                        else
                        {
                            MessageBox.Show("Not Added");
                        }

                    }
                }
                else
                {

                    int f = 1;
                    if (user.login != editUser.login)
                    {
                        if (b.isUserExist(user.login, "", ""))
                        {
                            f = 0;
                            MessageBox.Show("User with Login Already Exist");
                            return;
                        }
                    }

                    else if (user.nic != editUser.nic && f==1)
                    {
                        if (b.isUserExist("", user.nic, ""))
                        {
                            f = 0;
                            MessageBox.Show("User with NIC Already Exist");
                            return;
                        }
                    }
                    else if (user.email != editUser.email && f==1)
                    {
                        if (b.isUserExist("", "", user.email))
                        {
                            f = 0;
                            MessageBox.Show("User with Email Already Exist");
                            return;
                        }
                    }
                   if(f==1)
                    {
                        if (b.UpdateUser(user))
                        {
                            MessageBox.Show("Updated");
                            if (AdminFlag == 1)
                            {
                                Admin_Home ah = new Admin_Home();
                                this.Close();
                                ah.Show();
                            }
                            else
                            {
                                Home h = new Home(user);
                                this.Close();
                                h.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Not Updated");
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (AdminFlag == 1)
            {
                Admin_Home ah = new Admin_Home();
                this.Close();
                ah.Show();

            }

            else if(editUser!=null)
            {
                Home h = new Home(editUser);
                this.Close();
                h.Show();

            }
            else
            {
                Home h = new Home();
                this.Close();
                h.Show();
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var res = openFileDialog1.ShowDialog();
            if(res==System.Windows.Forms.DialogResult.OK)
            {
                var filepath = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(filepath);
            }
        }

        private void NewUser_Load(object sender, EventArgs e)
        {
            String applicationBasePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            System.IO.Directory.CreateDirectory(applicationBasePath + @"\images\");

            if (editUser != null)
            {
                textBox1.Text=editUser.name;
                textBox4.Text=editUser.login;
                textBox2.Text = editUser.password;
                textBox5.Text=editUser.address;
                textBox3.Text = editUser.email;
                string g;
                if (editUser.gender == 'M')
                    g = "Male";
                else
                    g = "Female";
                comboBox1.Text=g;
                maskedTextBox1.Text=editUser.nic;
                numericUpDown1.Value = editUser.age;
                checkBox1.Checked = Convert.ToBoolean(editUser.cricket);
                checkBox2.Checked = Convert.ToBoolean(editUser.hockey);
                checkBox3.Checked = Convert.ToBoolean(editUser.chess);
                string BasePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                string filePath = BasePath + @"\images\" + editUser.imageName;
                pictureBox1.Image = Image.FromFile(filePath);

            }
        }
    }
}
