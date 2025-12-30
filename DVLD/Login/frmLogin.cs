using DVLD.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer;
using System.IO;

namespace DVLD
{
    public partial class frmLogin: Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            clsUser user = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            
            if (user != null)
            {
                if (user.IsActive == false)
                {
                    MessageBox.Show("Your account deactivated, Please contact your Admin", "Wrong Credintails", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    clsGlobalSettings.LoggedInUser = user;

                    if (cbRemeberMe.Checked)
                    {
                        File.WriteAllLines("rememberme.txt", new string[]
                        {
                            txtUserName.Text,
                            txtPassword.Text
                        });
                    }
                    else
                    {
                        if (File.Exists("rememberme.txt"))
                        {
                            File.Delete("rememberme.txt");
                        }
                    }

                    Form frm = new frmMain();
                    this.Hide();
                    frm.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid Username/Password", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (File.Exists("rememberme.txt"))
            {
                string[] data = File.ReadAllLines("rememberme.txt");
                if (data.Length >= 2)
                {
                    txtUserName.Text = data[0];
                    txtPassword.Text = data[1];
                }
            }
        }
    }
}
