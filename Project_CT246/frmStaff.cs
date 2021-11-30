using Project_CT246.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CT246
{
    public partial class frmStaff : Form
    {
        public frmStaff()
        {
            InitializeComponent();
            LayDuLieuQuyen();
            LayDuLieuLenDataGridView();
            LayDuLieuLenTextBox();
            grBoxInfo.Enabled = false;
        }
        SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QuanCaPhe;Integrated Security=True");
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Sửa")
            {
                grBoxInfo.Enabled = true;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Text = "Lưu";
            }
            else
            {
                conn.Open();
                
                string query = "UPDATE dbo.Account SET name = N'" + txtNameUser.Text + "', username = '" + txtUsername.Text + "',pass = '" + txtPass.Text + "', birthday = '" + txtBirthday.Text + "', address = N'" + txtAddress.Text + "', phonenumber = '" + txtPhonenumber.Text + "', role_id = '" + cboRole.SelectedValue + "' WHERE username = '" + txtUsername.Text + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                LayDuLieuLenDataGridView();
                LayDuLieuLenTextBox();

                //MessageBox.Show("Sửa thành công.");

                btnEdit.Text = "Sửa";
                grBoxInfo.Enabled = false;
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                LayDuLieuLenTextBox();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();

            string query = "DELETE dbo.Account WHERE username = '" + txtUsername.Text + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            LayDuLieuLenDataGridView();
            LayDuLieuLenTextBox();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Thêm")
            {
                grBoxInfo.Enabled = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;

                txtNameUser.Text = "";
                txtUsername.Text = "";
                txtPass.Text = "";
                txtBirthday.Text = "";
                txtAddress.Text = "";
                txtPhonenumber.Text = "";
                cboRole.Text = "";

                btnAdd.Text = "Lưu";
            }
            else
            {
                conn.Open();
                
                string query = "INSERT INTO dbo.Account (username, pass, name, role_id, birthday, address, phonenumber) values ('" + txtUsername.Text + "', '" + txtPass.Text + "', N'" + txtNameUser.Text + "', '" + cboRole.SelectedValue + "', '" + txtBirthday.Text + "', N'" + txtAddress.Text + "', '" + txtPhonenumber.Text + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                LayDuLieuLenDataGridView();

                btnAdd.Text = "Thêm";
                grBoxInfo.Enabled = false;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                LayDuLieuQuyen();
                LayDuLieuLenTextBox();
            }
        }
        private void LayDuLieuQuyen()
        {
            conn.Open();
            string query = "SELECT *FROM dbo.RoleUser";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            conn.Close();
            DataTable tb = new DataTable();
            adap.Fill(tb);

            cboRole.DataSource = tb;
            cboRole.DisplayMember = "nameRole";
            cboRole.ValueMember = "id";
        }

        private void LayDuLieuLenDataGridView()
        {

            string query = "SELECT username, a.name , birthday, address, phonenumber, pass, b.nameRole FROM Account a, RoleUser b WHERE a.role_id = b.id";
      
            dgvUserList.DataSource = DataProvider.Instance.ExcuteQuery(query);
            
        }

        private void LayDuLieuLenTextBox()
        {
            txtNameUser.DataBindings.Clear();
            txtNameUser.DataBindings.Add("Text", dgvUserList.DataSource, "name");

            txtUsername.DataBindings.Clear();
            txtUsername.DataBindings.Add("Text", dgvUserList.DataSource, "username");

            txtPass.DataBindings.Clear();
            txtPass.DataBindings.Add("Text", dgvUserList.DataSource, "pass");

            txtBirthday.DataBindings.Clear();
            txtBirthday.DataBindings.Add("Text", dgvUserList.DataSource, "birthday");

            txtAddress.DataBindings.Clear();
            txtAddress.DataBindings.Add("Text", dgvUserList.DataSource, "address");

            txtPhonenumber.DataBindings.Clear();
            txtPhonenumber.DataBindings.Add("Text", dgvUserList.DataSource, "phonenumber");

            cboRole.DataBindings.Clear();
            cboRole.DataBindings.Add("Text", dgvUserList.DataSource, "nameRole");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
