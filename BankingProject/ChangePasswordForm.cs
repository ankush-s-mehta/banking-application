using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankingProject
{
    public partial class cPassForm : Form
    {
        public cPassForm()
        {
            InitializeComponent();
        }
        //to let the user change password
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                banking_dbEntities1 dbe = new banking_dbEntities1();
                if (oldpass.Text != string.Empty || newpass.Text != string.Empty || repass.Text != string.Empty)
                {
                    Admin_Table user1 = dbe.Admin_Table.FirstOrDefault(a => a.Username.Equals(usrtxt.Text));
                    if (user1 != null)
                    {
                        if (user1.Password.Equals(oldpass.Text))
                        {
                            user1.Password = newpass.Text;
                            dbe.SaveChanges();
                            //MessageBox.Show("Password changed successfully");
                            MessageBox.Show("Password changed successfully", "Password information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            //MessageBox.Show("Password Incorrect");
                            MessageBox.Show("Password Incorrect", "Password information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Password Incorrect");
                        MessageBox.Show("UserName is a required field", "Username Needed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //MessageBox.Show("Password Incorrect");
                    MessageBox.Show("Mandatory Values can not be blank", "Mandatory Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }
    }
}
