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
using Project_CT246.DAO;

namespace Project_CT246
{
    public partial class frmDrink : Form
    {
        public frmDrink()
        {
            InitializeComponent();
            grBoxDrinkInfo.Enabled = false;
            HienThiDLDataGridView();
            HienThiDLTextBox();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QuanCaPhe;Integrated Security=True");

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Sửa")
            {
                grBoxDrinkInfo.Enabled = true;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                txtID.ReadOnly = false;
                btnEdit.Text = "Lưu";
            }

            else
            {
                conn.Open();
                string query = "update dbo.Drink set idCategory = '" + txtCategory.Text + "' , name = N'" + txtNameDrink.Text + "' , price = '" + txtPrice.Text + "' where id = '" + txtID.Text + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                btnEdit.Text = "sửa";
                grBoxDrinkInfo.Enabled = false;
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;

                HienThiDLDataGridView();
                HienThiDLTextBox();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchname = txtDrinkSearch.Text.ToString();
            string query = "SELECT * FROM dbo.Drink  WHERE name like N'%"+ searchname +"%'";
            
            dgvDrinkList.DataSource = DataProvider.Instance.ExcuteQuery(query);

        }

        private void HienThiDLDataGridView()
        {
            string query = "SELECT a.id,a.idCategory,a.name,a.price,b.nameCategory FROM dbo.Drink AS a, dbo.Category AS b WHERE a.idCategory= b.id";

            
            dgvDrinkList.DataSource = DataProvider.Instance.ExcuteQuery(query);
            HienThiDLTextBox();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Thêm")
            {
                grBoxDrinkInfo.Enabled = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                txtID.Text = "";
                txtCategory.Text = "";
                txtNameDrink.Text = "";
                txtPrice.Text = "";
                btnAdd.Text = "Lưu";
            }
            else
            {
                conn.Open();
                string query = "INSERT INTO Drink (idCategory, name, price) VALUES ('" + txtCategory.Text + "', N'" + txtNameDrink.Text + "', '" + txtPrice.Text + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                btnAdd.Text = "Thêm";
                grBoxDrinkInfo.Enabled = false;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                HienThiDLDataGridView();
                HienThiDLTextBox();

            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            conn.Open();
            string query = "delete dbo.Drink  where id = '" + txtID.Text + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            HienThiDLDataGridView();
            HienThiDLTextBox();
        }
        private void HienThiDLTextBox()
        {
            txtID.DataBindings.Clear();
            txtID.DataBindings.Add("Text", dgvDrinkList.DataSource, "id");

            txtCategory.DataBindings.Clear();
            txtCategory.DataBindings.Add("Text", dgvDrinkList.DataSource, "idCategory");

            txtNameDrink.DataBindings.Clear();
            txtNameDrink.DataBindings.Add("Text", dgvDrinkList.DataSource, "name");

            txtPrice.DataBindings.Clear();
            txtPrice.DataBindings.Add("Text", dgvDrinkList.DataSource, "price");
        }
    }
}
