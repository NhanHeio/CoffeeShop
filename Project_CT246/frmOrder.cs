using Project_CT246.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Project_CT246
{
    public partial class frmOrder : Form
    {
        public frmOrder()
        {
            InitializeComponent();
            HienThiDLDataGridView();
            HienThiDLTextBox();
            LoadTempBill();

        }
        private string connectionSTR = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanCaPhe;Integrated Security=True";

        private void HienThiDLDataGridView()
        {
            string query = "SELECT a.id, b.nameCategory, a.name,a.price FROM dbo.Drink AS a, dbo.Category AS b WHERE a.idCategory= b.id";
            dtgvOrderDrinkList.DataSource = DataProvider.Instance.ExcuteQuery(query);
            HienThiDLTextBox();
        }
        private void LoadTempBill()
        {
            string query = "SELECT * FROM dbo.temp_bill";
            dtgvBill.DataSource = DataProvider.Instance.ExcuteQuery(query);
            float totalPrice = 0;
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                totalPrice += (float)double.Parse(row["totalPrice"].ToString());
            }
            //float discount = 0;
            //discount = (float)double.Parse(txtDiscount.Text.ToString());
            //totalPrice = totalPrice * (1 - (discount / 100));
            //CultureInfo culture = new CultureInfo("vi-VN");
            txtSumPrice.Text = totalPrice.ToString();
            
        }
        
        private void HienThiDLTextBox()
        {
            btnDelete.Enabled = true;

            txtCategory.DataBindings.Clear();
            txtCategory.DataBindings.Add("Text", dtgvOrderDrinkList.DataSource, "nameCategory");

            txtDrink.DataBindings.Clear();
            txtDrink.DataBindings.Add("Text", dtgvOrderDrinkList.DataSource, "name");

        }

        private void HienThiDLTextBoxBill()
        {

            txtCategory.DataBindings.Clear();
            txtDrink.DataBindings.Clear();
            txtDrink.DataBindings.Add("Text", dtgvOrderDrinkList.DataSource, "name");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchname = txtSearch.Text.ToString();
            string query = "SELECT * FROM dbo.Drink  WHERE name like N'%" + searchname + "%'";

            dtgvOrderDrinkList.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string nameDrink = txtDrink.Text.ToString();
            int quantity = Int32.Parse(nmudQuantity.Value.ToString());
            string query = "SELECT price FROM dbo.Drink WHERE name = N'" + nameDrink + "'";
            SqlConnection conn = new SqlConnection(connectionSTR);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            float priceDrink;
            reader.Read();
            
                priceDrink = (float)double.Parse(reader["price"].ToString());
            
            reader.Close();
            conn.Close();
            string sql = "INSERT INTO dbo.temp_bill VALUES(N'"+nameDrink+"',"+ quantity+",'"+ priceDrink +"',"+ quantity*priceDrink+")";
            int result = DataProvider.Instance.ExcuteNonQuery(sql);
            if(result > 0)
            {
                LoadTempBill();
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            string nameDrink = "";
            float price = 0;
            int quantity = 0;
            //float totalPrice = (float)double.Parse(txtSumPrice.Text.ToString());
            string totalPrice = txtSumPrice.Text.ToString();
            string query = "INSERT INTO Bill (totalPrice) VALUES('" + totalPrice + "')";
            //MessageBox.Show(query);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            int resultBillInfo = 0;
            int idBill = 0;
            if (result > 0)
            {
                string sql = "SELECT TOP(1) id FROM dbo.Bill order by id desc";
                DataTable dt = new DataTable();
                dt = DataProvider.Instance.ExcuteQuery(sql);
                foreach (DataRow row in dt.Rows)
                {
                    idBill = Int32.Parse(row["id"].ToString());
                }
            }
            
            string query1 = "SELECT * FROM dbo.temp_bill";
            DataTable data = new DataTable();
            data = DataProvider.Instance.ExcuteQuery(query1);
            //MessageBox.Show(idBill.ToString());
            foreach (DataRow row in data.Rows)
            {
                nameDrink = row["name"].ToString();
                price = (float)double.Parse(row["price"].ToString());
                quantity = Int32.Parse(row["quantity"].ToString());
                //totalPrice += (float)double.Parse(row["totalPrice"].ToString());
                //MessageBox.Show("nhan");
                string queryAddBill = "INSERT INTO dbo.BillInfo(idBill, nameDrink, price, quantity) VALUES(" + idBill + ", N'" + nameDrink + "'," + price + "," + quantity + ")";
                //MessageBox.Show(queryAddBill);
                resultBillInfo = DataProvider.Instance.ExcuteNonQuery(queryAddBill);

            }

            if (resultBillInfo > 0)
            {
                
                string queryTruncate = "DELETE FROM dbo.temp_bill";
                int resultTruncate = DataProvider.Instance.ExcuteNonQuery(queryTruncate);
                if(resultTruncate > 0)
                {
                    MessageBox.Show("Thanh toán thành công!");
                }
            }
            HienThiDLDataGridView();
            HienThiDLTextBox();
            LoadTempBill();
        }

        private void cancelSearch_Click(object sender, EventArgs e)
        {
            HienThiDLDataGridView();
            HienThiDLTextBox();
            LoadTempBill();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string nameDrink = txtDrink.Text.ToString();
            string query = "DELETE FROM dbo.temp_bill WHERE name = N'" + nameDrink + "'";
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            if (result > 0)
            {
                MessageBox.Show("Xóa mặt hàng thành công!");
            }
            //HienThiDLTextBox();
            LoadTempBill();
        }
    }
}
