using Project_CT246.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Project_CT246
{
    public partial class frmBill : Form
    {
        public static DateTime date;
        public frmBill()
        {
            InitializeComponent();
            HienThiDLDataGridView();
        }
        private void HienThiDLDataGridView()
        {
            string query = "SELECT * FROM dbo.Bill";


            dtgvListBill.DataSource = DataProvider.Instance.ExcuteQuery(query);
            HienThiDLTextBox();
        }

        private void HienThiDLDataGridViewDetail(int id)
        {
            string query = "SELECT nameDrink,quantity,price FROM dbo.BillInfo WHERE idBill ="+ id ;
            dtgvBillDetail.DataSource = DataProvider.Instance.ExcuteQuery(query);
            
        }

        private void HienThiDLTextBox()
        {
            txtIdBill.DataBindings.Clear();
            txtIdBill.DataBindings.Add("Text", dtgvListBill.DataSource, "id");

            txtSumPrice.DataBindings.Clear();
            txtSumPrice.DataBindings.Add("Text", dtgvListBill.DataSource, "totalPrice");

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txtIdBill.Text.ToString());
            HienThiDLDataGridViewDetail(id);
            string query = "SELECT dateChecking FROM dbo.Bill WHERE id =" + id;
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                date = DateTime.Parse(row["dateChecking"].ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txtIdBill.Text.ToString());
            string query = "DELETE dbo.BillInfo WHERE idBill ="+id;
            int resultBillInfo = DataProvider.Instance.ExcuteNonQuery(query);
            string query1 = "DELETE dbo.Bill WHERE id =" + id;
            int resultBill = DataProvider.Instance.ExcuteNonQuery(query1);
            if(resultBill > 0 && resultBillInfo > 0)
            {
                MessageBox.Show("Xóa Bill Thành Công!");
            }
            HienThiDLDataGridView();
            HienThiDLDataGridViewDetail(id);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDialog print = new PrintDialog();
            print.Document = printDocument1;
            print.UseEXDialog = true;
            DialogResult printResult = print.ShowDialog();

            if(printResult == DialogResult.OK)
            {
                printDocument1.DocumentName = "Printing Bill";
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Hóa đơn",new Font("Arial",25, FontStyle.Bold),Brushes.Black, 100, 0);
            e.Graphics.DrawString("Nhân viên: " + frmLogin.user, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 0, 40);
            e.Graphics.DrawString("Ngày tạo hóa đơn: "+ date, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 0, 60);
            e.Graphics.DrawString("Tổng tiền: " + txtSumPrice.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 0, 80);
            Bitmap printDocumentBitmap = new Bitmap(this.dtgvBillDetail.Width, this.dtgvBillDetail.Height);
            dtgvBillDetail.DrawToBitmap(printDocumentBitmap, new Rectangle(0,0, this.dtgvBillDetail.Width, this.dtgvBillDetail.Height));
            e.Graphics.DrawImage(printDocumentBitmap,0,100);
        }
    }
}
