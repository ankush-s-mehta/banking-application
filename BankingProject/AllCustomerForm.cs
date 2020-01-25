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
    public partial class AllCustomerForm : Form
    {
        public AllCustomerForm()
        {
            InitializeComponent();
            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                CustomerGridView.AutoGenerateColumns = false;
                banking_dbEntities1 bs = new banking_dbEntities1();
                var item = bs.userAccounts.ToList();
                CustomerGridView.DataSource = item;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }
    }
}
