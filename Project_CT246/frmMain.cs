using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Project_CT246
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
            frmLogin f = new frmLogin();
            f.ShowDialog();
            
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin f = new frmLogin();
            f.ShowDialog();
        }

        private void quảnLýSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDrink f = new frmDrink();
            f.ShowDialog();
            this.Show();
        }

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmStaff staff = new frmStaff();
            staff.ShowDialog();
            this.Show();
        }

        private void kháchĐặtBànToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmOrder order = new frmOrder();
            order.ShowDialog();
            this.Show();
        }

        private void xuấtHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmBill bill = new frmBill();
            bill.ShowDialog();
            this.Show();
        }
    }
}
