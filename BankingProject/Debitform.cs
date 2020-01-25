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
    public partial class Debitform : Form
    {
        public Debitform()
        {
            InitializeComponent();
            LoadDate();
            LoadMode();
        }
        private void LoadMode()
        {
            //throw new NotImplementedException();
            comboBox1.Items.Add("Cash");
            comboBox1.Items.Add("Cheque");
        }

        private void LoadDate()
        {
            //throw new NotImplementedException();
            datelbl.Text = DateTime.UtcNow.ToString("MM/dd/yyyy");
        }
        //fetches the account details
        private void DetailButton_Click(object sender, EventArgs e)
        {
           try
            {
                if (String.IsNullOrEmpty(acctxt.Text))
                {
                    MessageBox.Show("Enter Account Number", "Account information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                banking_dbEntities1 dbe = new banking_dbEntities1();
                decimal b = Convert.ToDecimal(acctxt.Text);
                var item = (from u in dbe.userAccounts
                            where u.Account_No == b
                            select u).FirstOrDefault();
                nametxt.Text = item.Name;
                oldbaltxt.Text = Convert.ToString(item.balance);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }
        //commits the changes made related to amount of money to be debited from account holder
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                banking_dbEntities1 dbe = new banking_dbEntities1();
                NewAccountForm nacc = new NewAccountForm();
                debit dp = new debit();
                dp.Date = datelbl.Text;
                dp.AccountNo = Convert.ToDecimal(acctxt.Text);
                dp.Name = nametxt.Text;
                dp.OldBalance = Convert.ToDecimal(oldbaltxt.Text);
                dp.Mode = comboBox1.SelectedItem.ToString();
                dp.DebitaAmount = Convert.ToDecimal(amounttxt.Text);
                dbe.debits.Add(dp);
                dbe.SaveChanges();
                decimal b = Convert.ToDecimal(acctxt.Text);
                var item = (from u in dbe.userAccounts
                            where u.Account_No == b
                            select u).FirstOrDefault();
                item.balance = item.balance - Convert.ToDecimal(acctxt.Text);
                dbe.SaveChanges();
                //MessageBox.Show("Debited Money Succesuflly");
                MessageBox.Show("Debited Money Succesuflly", "Debit information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter only numbers", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //to not allow values except numbers and backspace
        private void amounttxt_KeyPress(object sender, KeyPressEventArgs e)
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
