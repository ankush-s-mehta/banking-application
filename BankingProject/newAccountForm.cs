using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankingProject
{
    public partial class NewAccountForm : Form
    {
        string gender = string.Empty;
        string m_status = string.Empty;
        decimal no;
        banking_dbEntities1 BSE;
        MemoryStream ms;

        public NewAccountForm()
        {
            InitializeComponent();
            LoadDate();
            LoadAccount();
            LoadState();
        }

        private void LoadState()
        {
            comboBox1.Items.Add("Rajasthan");
        }

        private void LoadAccount()
        {
            BSE = new banking_dbEntities1();
            var item = BSE.userAccounts.ToArray();
            no = item.LastOrDefault().Account_No + 1;
            accnotext.Text = Convert.ToString(no);
        }

        private void LoadDate()
        {
            //throw new NotImplementedException();
            //datelbl.Text = DateTime.Now.ToString("MM/dd/YYYY");
            //dateTimePicker1.Text = DateTime.Now.ToString("MM/dd/YYYY");
            dateTimePicker1.Value = DateTime.Now;
        }
        //to upload photo and save to db
        private void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openbdlg = new OpenFileDialog();
                if (openbdlg.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(openbdlg.FileName);
                    pictureBox1.Image = img;
                    ms = new MemoryStream();
                    img.Save(ms, img.RawFormat);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }
        //save new account details of the user and store to db
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (maleradio.Checked)
                {
                    gender = "male";
                }
                else if (femaleradio.Checked)
                {
                    gender = "female";
                }
                else if (otherradio.Checked)
                {
                    gender = "other";
                }
                if (marriedradio.Checked)
                {
                    m_status = "married";
                }
                else if (unmarriedradio.Checked)
                {
                    m_status = "unmarried";
                }
                BSE = new banking_dbEntities1();
                userAccount acc = new userAccount();
                acc.Account_No = Convert.ToDecimal(accnotext.Text);
                acc.Name = nametext.Text;
                acc.DOB = dateTimePicker1.Value.ToString();
                acc.Phoneno = phonetext.Text;
                acc.Address = addresstext.Text;
                acc.District = districttext.Text;
                acc.State = comboBox1.SelectedItem.ToString();
                acc.Gender = gender;
                acc.martial_status = m_status;
                acc.MotherName = mothertext.Text;
                acc.FatherName = fathertext.Text;
                acc.balance = Convert.ToDecimal(balancetext.Text);
                //acc.Date = datel
                acc.Picture = ms.ToArray();
                BSE.userAccounts.Add(acc);
                BSE.SaveChanges();
                //MessageBox.Show("file saved");
                MessageBox.Show("File Saved", "File information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }
        //to not allow values except numbers and backspace
        private void phonetext_KeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter only numbers", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //to not allow values except numbers and backspace
        private void balancetext_KeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter only numbers", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}