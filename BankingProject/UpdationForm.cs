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
    public partial class UpdationForm : Form
    {
        banking_dbEntities1 dbe;
        MemoryStream ms;
        BindingList<userAccount> bi;
        public UpdationForm()
        {
            InitializeComponent();
        }
        //fetch the details of the account no entered
        private void DetailsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(acctxt.Text))
                {
                    MessageBox.Show("Enter Account Number", "Account information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                bi = new BindingList<userAccount>();
                dbe = new banking_dbEntities1();
                decimal accno = Convert.ToDecimal(acctxt.Text);
                var item = dbe.userAccounts.Where(a => a.Account_No == accno);
                foreach (var item1 in item)
                {
                    bi.Add(item1);
                }
                UpdateGridView.DataSource = bi;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Account does not exist or is invalid", "Does not exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //search customer with name and displaying details
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                bi = new BindingList<userAccount>();
                dbe = new banking_dbEntities1();
                var item = dbe.userAccounts.Where(a => a.Name == Nametxt.Text);
                foreach (var item1 in item)
                {
                    bi.Add(item1);
                }
                UpdateGridView.DataSource = bi;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Value entered is invalid or does not exist", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dbe = new banking_dbEntities1();
                decimal accno = Convert.ToDecimal(UpdateGridView.Rows[e.RowIndex].Cells[0].Value);
                var item = dbe.userAccounts.Where(a => a.Account_No == accno).FirstOrDefault();
                acctxt.Text = item.Account_No.ToString();
                Nametxt.Text = item.Name;
                mothertxt.Text = item.MotherName;
                fathertxt.Text = item.FatherName;
                phonetxt.Text = item.Phoneno;
                addresstxt.Text = item.Address;
                byte[] img = item.Picture;
                MemoryStream ms = new MemoryStream(img);
                disttxt.Text = item.District;
                statetxt.Text = item.State;
                if (item.Gender == "male")
                {
                    maleradio.Checked = true;
                }
                else if (item.Gender == "female")
                {
                    femaleradio.Checked = true;
                }
                else if (item.Gender == "other")
                {
                    otherradio.Checked = true;
                }
                if (item.martial_status == "married")
                {
                    marriedradio.Checked = true;
                }
                else if (item.martial_status == "unmarried")
                {
                    unmarriedradio.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }
        //upload the image
        private void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opendlg = new OpenFileDialog();
                if (opendlg.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(opendlg.FileName);
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
        //deletes the details of the account
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                bi.RemoveAt(UpdateGridView.SelectedRows[0].Index);
                dbe = new banking_dbEntities1();
                decimal a = Convert.ToDecimal(acctxt.Text);
                userAccount acc = dbe.userAccounts.First(s => s.Account_No.Equals(a));
                dbe.userAccounts.Remove(acc);
                dbe.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }
        //updates the account holder details
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                dbe = new banking_dbEntities1();
                decimal accountno = Convert.ToDecimal(acctxt.Text);
                userAccount useraccount = dbe.userAccounts.First(s => s.Account_No.Equals(accountno));
                useraccount.Account_No = Convert.ToDecimal(acctxt.Text);
                useraccount.Name = Nametxt.Text;
                useraccount.Date = dateTimePicker1.Value.ToString();
                useraccount.MotherName = mothertxt.Text;
                useraccount.FatherName = fathertxt.Text;
                useraccount.Phoneno = phonetxt.Text;
                if (maleradio.Checked == true)
                {
                    useraccount.Gender = "male";
                }
                else
                {
                    if (femaleradio.Checked == true)
                    {
                        useraccount.Gender = "female";
                    }
                }
                if (marriedradio.Checked == true)
                {
                    useraccount.martial_status = "married";
                }
                else
                {
                    if (unmarriedradio.Checked == true)
                    {
                        useraccount.martial_status = "unmarried";
                    }
                }
                Image img = pictureBox1.Image;
                if (img.RawFormat != null)
                {
                    if (ms != null)
                    {
                        img.Save(ms, img.RawFormat);
                        useraccount.Picture = ms.ToArray();
                    }
                }
                useraccount.Address = addresstxt.Text;
                useraccount.District = disttxt.Text;
                useraccount.State = statetxt.Text;
                dbe.SaveChanges();
                MessageBox.Show("Record updated", "Record information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }
        //to not allow values except numbers and backspace
        private void acctxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;
            if(!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter only numbers", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //to not allow values except numbers and backspace
        private void phonetxt_KeyPress(object sender, KeyPressEventArgs e)
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
