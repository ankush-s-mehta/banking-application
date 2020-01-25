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
    public partial class DepositForm : Form
    {
        public DepositForm()
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
        //To Update the deposit details and save into th db
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                banking_dbEntities1 context = new banking_dbEntities1();
                NewAccountForm acc = new NewAccountForm();
                Deposit dp = new Deposit();
                dp.Date = datelbl.Text;
                dp.AccountNo = Convert.ToDecimal(acctxt.Text);
                dp.Name = nametxt.Text;
                dp.OldBalance = Convert.ToDecimal(oldbaltxt.Text);
                dp.Mode = comboBox1.SelectedItem.ToString();
                dp.DipAmount = Convert.ToDecimal(amounttxt.Text);
                context.Deposits.Add(dp);
                context.SaveChanges();
                decimal b = Convert.ToDecimal(acctxt.Text);
                var item = (from u in context.userAccounts
                            where u.Account_No == b
                            select u).FirstOrDefault();

                item.balance = item.balance + Convert.ToDecimal(amounttxt.Text);
                context.SaveChanges();
                //    MessageBox.Show("Deposited Money Sucessfully");
                MessageBox.Show("Deposited Money Sucessfully", "Deposit information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }
        //fetches the account details
        private void DetailsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(acctxt.Text))
                {
                    MessageBox.Show("Enter Account Number", "Account information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                banking_dbEntities1 context = new banking_dbEntities1();
                decimal b = Convert.ToDecimal(acctxt.Text);
                var item = (from u in context.userAccounts
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
