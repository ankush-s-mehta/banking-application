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
    public partial class MoneyTransferForm : Form
    {
        public MoneyTransferForm()
        {
            InitializeComponent();
            loaddate();
        }
        private void loaddate()
        {
            //throw new NotImplementedException();
            datelbl.Text = DateTime.UtcNow.ToString("MM/dd/yyyy");
        }
        //accepts account no as input and provies the account holder details
        private void DetailsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(fromacctxt.Text))
                {
                    MessageBox.Show("Enter Account Number", "Account information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                banking_dbEntities1 dbe = new banking_dbEntities1();
                decimal b = Convert.ToDecimal(fromacctxt.Text);
                var item = (from u in dbe.userAccounts
                            where u.Account_No == b
                            select u).FirstOrDefault();
                nametxt.Text = item.Name;
                amounttxt.Text = Convert.ToString(item.balance);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }
        //to commit the changes of transferring money from one account to the other
        private void TransferButton_Click(object sender, EventArgs e)
        {
            try
            {
                banking_dbEntities1 dbe = new banking_dbEntities1();
                decimal b = Convert.ToDecimal(fromacctxt.Text);
                var item = (from u in dbe.userAccounts
                            where u.Account_No == b
                            select u).FirstOrDefault();
                decimal b1 = Convert.ToDecimal(item.balance);
                decimal totalbal = Convert.ToDecimal(transfertxt.Text);
                decimal transferacc = Convert.ToDecimal(desaccounttxt.Text);
                if (b1 > totalbal)
                {
                    userAccount item2 = (from u in dbe.userAccounts
                                         where u.Account_No == transferacc
                                         select u).FirstOrDefault();
                    item2.balance = item2.balance + totalbal;
                    item.balance = item.balance - totalbal;
                    //dbe.SaveChanges();
                    Transfer transfer = new Transfer();
                    transfer.Account_no = Convert.ToDecimal(fromacctxt.Text);
                    transfer.ToTransfer = Convert.ToDecimal(desaccounttxt.Text);
                    transfer.Date = DateTime.UtcNow.ToString();
                    transfer.Name = nametxt.Text;
                    transfer.balance = Convert.ToDecimal(transfertxt.Text);

                    dbe.Transfers.Add(transfer);
                    dbe.SaveChanges();
                    //MessageBox.Show("Transfer Money Successfull");
                    MessageBox.Show("Transfer Money Successfull", "Money Transfer information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }
        //to not allow values except numbers and backspace
        private void fromacctxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter only numbers", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //to not allow values except numbers and backspace
        private void desaccounttxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter only numbers", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //to not allow values except numbers and backspace
        private void transfertxt_KeyPress(object sender, KeyPressEventArgs e)
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
