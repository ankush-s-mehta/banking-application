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
    public partial class balanceSheetForm : Form
    {
        public balanceSheetForm()
        {
            InitializeComponent();
        }
        //fetches debit deposit and transfer details of the account number
        protected void ShowButton_Click(object sender, EventArgs e)
        {
            try
            {
                banking_dbEntities1 dbe = new banking_dbEntities1();
                decimal b = Convert.ToDecimal(textBox1.Text);
                var item = (from u in dbe.debits
                            where u.AccountNo == b
                            select u);
                DebitGridView.DataSource = item.ToList();
                var item1 = (from u in dbe.Deposits
                             where u.AccountNo == b
                             select u);
                DepositGridView.DataSource = item1.ToList();
                var item2 = (from u in dbe.Transfers
                             where u.Account_no == b
                             select u);
                TransferGridView.DataSource = item2.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }
    }
}