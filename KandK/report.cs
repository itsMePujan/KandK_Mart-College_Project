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

namespace KandK
{
    public partial class report : UserControl
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=mart;Integrated Security=True");
        public report()
        {
            InitializeComponent();
            customerload();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                report1.DataSource = null;
                report1.Refresh();
                SqlCommand sqlCommand = new SqlCommand("[dbo].[yearwisesalesreport]", con);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@DateStart", SqlDbType.Date).Value = startdate.Value;
                sqlCommand.Parameters.AddWithValue("@DateEnd", SqlDbType.Date).Value = enddate.Value;
                DataTable dataTable = new DataTable();
                con.Open();
                dataTable.Load(sqlCommand.ExecuteReader());
                report1.DataSource = dataTable;
                con.Close();
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            report1.DataSource = null;
            report1.Refresh();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[productwisesalesreport]", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@productname", SqlDbType.Date).Value = "%"+product_txt.Text+"%";
            DataTable dataTable = new DataTable();
            con.Open();
            dataTable.Load(sqlCommand.ExecuteReader());
            report1.DataSource = dataTable;
            con.Close();

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void customerload()
        {
            customergrid.DataSource = null;
            customergrid.Refresh();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[customerName]", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            con.Open();
            dataTable.Load(sqlCommand.ExecuteReader());
            customergrid.DataSource = dataTable;
            con.Close();
        }

        private void customergrid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
               txtbox_customername.Text= (customergrid.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            catch (Exception ex)
            {

            }
        }

        private void txtbox_customername_TextChanged(object sender, EventArgs e)
        {
            report1.DataSource = null;
            report1.Refresh();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[customerwisesalesreport]", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@customername", SqlDbType.Date).Value = "%" + txtbox_customername.Text + "%";
            DataTable dataTable = new DataTable();
            con.Open();
            dataTable.Load(sqlCommand.ExecuteReader());
            report1.DataSource = dataTable;
            con.Close();
        }

        private void product_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (char.IsDigit(ch) && ch != 8 && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}
