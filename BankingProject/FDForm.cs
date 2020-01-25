using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankingProject
{
    public partial class b : Form
    {
        public b()
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
        //function to accept account no intrest rate etc and start FD calculation
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                banking_dbEntities1 dbe = new banking_dbEntities1();
                decimal accno = Convert.ToDecimal(accnotxt.Text);
                var accounts = dbe.userAccounts.Where(x => x.Account_No == accno).SingleOrDefault();
                FD fdform = new FD();
                fdform.Accoun_no = (accnotxt.Text);
                fdform.mode = comboBox1.SelectedItem.ToString();
                fdform.rupees = Convert.ToDecimal(rupeestxt.Text);
                fdform.period = Convert.ToInt32(periodtxt.Text);
                fdform.intrest_rate = Convert.ToDecimal(intresttxt.Text);
                fdform.start_date = DateTime.UtcNow.ToString("MM/dd/yyy");
                fdform.Maturity_date = (DateTime.UtcNow.AddDays(Convert.ToInt32(periodtxt.Text))).ToString("MM/dd/yyy");
                fdform.Maturity_amount = ((Convert.ToDecimal(rupeestxt.Text) * Convert.ToInt32(periodtxt.Text) * Convert.ToDecimal(intresttxt.Text)) /
                    (100 * 12 * 30)) + (Convert.ToDecimal(rupeestxt.Text));
                dbe.FDs.Add(fdform);
                decimal amount = Convert.ToDecimal(rupeestxt.Text);
                decimal totalamount = Convert.ToDecimal(accounts.balance);
                decimal fdamount = totalamount - amount;
                accounts.balance = fdamount;
                dbe.Entry(accounts).State = EntityState.Modified;
                //dbe.userAccounts.Add(accounts);
                dbe.SaveChanges();
                //MessageBox.Show("FD Started Now");
                MessageBox.Show("FD Started Now", "FD information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }

        private void accnotxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter only numbers", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //to not allow values except numbers and backspace
        private void rupeestxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter only numbers", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //to not allow values except numbers and backspace
        private void periodtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter only numbers", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //to not allow values except numbers and backspace
        private void intresttxt_KeyPress(object sender, KeyPressEventArgs e)
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
