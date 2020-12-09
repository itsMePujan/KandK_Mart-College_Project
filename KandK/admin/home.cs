using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace KandK.admin
{
    public partial class home : UserControl
    {
        public home()
        {
            InitializeComponent();
        }
        string constring = "Data Source=.;Initial Catalog=mart;Integrated Security=True";
        SqlDataAdapter da;

        private void countproduct()
        {
            int id;
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string sql = " SELECT COUNT(*) as Count FROM product ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            id = Convert.ToInt32(reader["Count"]);
            txt_count.Text = id.ToString();
            con.Close();
        }
        private void countsupplier()
        {
            int id;
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string sql = " SELECT COUNT(*) as Count FROM Supplier ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            id = Convert.ToInt32(reader["Count"]);
            txt_supplier.Text = id.ToString();
            con.Close();
        }
        private void admin()
        {
            int id;
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string sql = " SELECT COUNT(*) as Count FROM User_detail where usertype = 'Admin' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            id = Convert.ToInt32(reader["Count"]);
            txt_totaladmins.Text = id.ToString();
            con.Close();
        }
        private void employee()
        {
            int id;
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string sql = " SELECT COUNT(*) as Count FROM User_detail where usertype = 'Employee' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            id = Convert.ToInt32(reader["Count"]);
            txt_totalemployees.Text = id.ToString();
            con.Close();
        }
        private void Customers()
        {
            int id;
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string sql = " SELECT COUNT(*) as Count FROM Customer  ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            id = Convert.ToInt32(reader["Count"]);
            txt_totalcustomer.Text = id.ToString();
            con.Close();
        }

        private void home_Load(object sender, EventArgs e)
        {
            countproduct();
            countsupplier();
            admin();
            employee();
            Customers();
        }
    }
}
