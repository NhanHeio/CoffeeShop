using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CT246.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        private string connectionSTR = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanCaPhe;Integrated Security=True";

        public DataTable ExcuteQuery(string query)
        {
            DataTable tb = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionSTR))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                
                adap.Fill(tb);
                conn.Close();
            }

            return tb;
        }

        public int ExcuteNonQuery(string query)
        {
            int data = 0;
            using (SqlConnection conn = new SqlConnection(connectionSTR))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteNonQuery();
                conn.Close();
            }

            return data;
        }

        public Object ExcuteScalar(string query)
        {
            object data = 0;
            using (SqlConnection conn = new SqlConnection(connectionSTR))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteScalar();
                conn.Close();
            }

            return data;
        }
    }
}
