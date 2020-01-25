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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void newAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewAccountForm newacc = new NewAccountForm();
            newacc.MdiParent = this;
            newacc.Show();
        }

        private void updateSearchAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdationForm up = new UpdationForm();
            up.MdiParent = this;
            up.Show();
        }

        private void allCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllCustomerForm allCust = new AllCustomerForm();
            allCust.MdiParent = this;
            allCust.Show();
        }

        private void depositToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DepositForm dep = new DepositForm();
            dep.MdiParent = this;
            dep.Show();
        }

        private void withdrawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debitform draw = new Debitform();
            draw.MdiParent = this;
            draw.Show();
        }

        private void transferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoneyTransferForm trans = new MoneyTransferForm();
            trans.MdiParent = this;
            trans.Show();
        }

        private void fDFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            b vfd = new b();
            vfd.MdiParent = this;
            vfd.Show();
        }

        private void balanceSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            balanceSheetForm bsheet = new balanceSheetForm();
            bsheet.MdiParent = this;
            bsheet.Show();
        }

        private void viewFDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFDDetailsForm fd = new ViewFDDetailsForm();
            fd.MdiParent = this;
            fd.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cPassForm cpass = new cPassForm();
            cpass.MdiParent = this;
            cpass.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
