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
    public partial class ViewFDDetailsForm : Form
    {
        BindingList<FD> b1;
        banking_dbEntities1 dbe;
        public ViewFDDetailsForm()
        {
            InitializeComponent();
        }
        //show the FD details of all the accounts
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                b1 = new BindingList<FD>();
                dbe = new banking_dbEntities1();
                string date = dateTimePicker1.Value.ToString("MM/dd/yyyy");
                //MessageBox.Show(date);
                var item = dbe.FDs.Where(a => a.start_date.Equals(date));
                FDGridView.DataSource = item.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred");
            }
        }
    }
}
