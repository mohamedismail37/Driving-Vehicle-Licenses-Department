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
using Microsoft.Win32;

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

                    string keyPath = @"HKEY_CURRENT_USER\Software\DVLD";
                    string valueName1 = "UserName";
                    string valueName2 = "Password";

                    if (cbRemeberMe.Checked)
                    {
                        string valueData1 = txtUserName.Text;
                        string valueData2 = txtPassword.Text;

                        try
                        {
                            Registry.SetValue(keyPath, valueName1, valueData1, RegistryValueKind.String);
                            Registry.SetValue(keyPath, valueName2, valueData2, RegistryValueKind.String);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occured: {ex.Message}");
                        }

                    }
                    else
                    {
                        try
                        {
                            string keyPath2 = @"Software\DVLD";
                            // Open the registry key in read/write mode with explicit registry view
                            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                            {
                                using (RegistryKey key = baseKey.OpenSubKey(keyPath2 , true))
                                {
                                    if (key != null)
                                    {
                                        // Delete the specified value
                                        key.DeleteValue(valueName1);
                                        key.DeleteValue(valueName2);
                                    }
                                }
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("UnauthorizedAccessException: Run the program with administrative privileges.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred: {ex.Message}");
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
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
            string valueName1 = "UserName"; // here the value that you want to read
            string valueName2 = "Password";
            try
            {
                // Read the value to the Registry
                string UserName = Registry.GetValue(keyPath, valueName1, null) as string;
                string Password = Registry.GetValue(keyPath, valueName2, null) as string;

                if (UserName != null)
                {
                    txtUserName.Text = UserName;
                }
                if (Password != null)
                {
                    txtPassword.Text = Password;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured: {ex.Message}");
            }
        }
    }
}
