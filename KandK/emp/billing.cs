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

namespace KandK.emp
{
    public partial class billing : UserControl
    {
        public billing()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=mart;Integrated Security=True");
        private void billing_Load(object sender, EventArgs e)
        {
            productload();
            getordernumber();
            cust();
            cbo_unit.SelectedIndex = 0;
            cbo_unit.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void clearbtn()
        {
            txtBox_priceperunit.Text = string.Empty;
            txtBox_total.Text = string.Empty;

        }
        private void productload()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select ProductName from product ", con);
            cmd.ExecuteNonQuery();
            DataTable Table = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da = new SqlDataAdapter(cmd);
            da.Fill(Table);
            foreach (DataRow dr in Table.Rows)
            {
                cbo_product.Items.Add(dr["ProductName"].ToString());
            }
            con.Close();
            cbo_product.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void getordernumber()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select max(OrderNumber)+1 OrderNumber from [Order]";
            txt_ordernumber.Text = cmd.ExecuteScalar().ToString();
            con.Close();
        }

        private void cbo_unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            int unit = Convert.ToInt32(cbo_unit.SelectedItem);
            txtBox_total.Text = (p * unit).ToString();
        }
        int p;
        int customerid;
        int productid;
        private void cbo_product_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select * from product where ProductName = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = cbo_product.SelectedItem;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            productid = Convert.ToInt32(reader["ProductId"]);
            txtbox_stock.Text = (reader["stock"].ToString());
            txtBox_priceperunit.Text = (reader["sellingPrice"].ToString());
            p = Convert.ToInt32(reader["sellingPrice"].ToString());
            // MessageBox.Show("" + p);
            con.Close();
        }
        private void cust()
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("select C.CustomerId, C.firstname,C.lastname, C.sex,C.phone, Co.CountryName  from Customer C inner join Country Co on C.CountryId = Co.Countryid  where firstname like @cust", con);
            // cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@cust", SqlDbType.Int).Value = "%" + textBox1.Text + "%";
            cmd2.ExecuteNonQuery();
            SqlDataAdapter DA = new SqlDataAdapter(cmd2);
            DataSet ds = new DataSet();
            DA.Fill(ds, "Gridviewload");
            dataGridView1.DataSource = ds.Tables["GridViewload"];
            con.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                customerid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

            }
            catch (Exception ex)
            {

            }

            textBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            cust();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(txtBox_total.Text);
            try
            {
                if (textBox2.Text == "" || (Convert.ToInt32(textBox2.Text) <= total))
                {
                    textBox3.Text = "N/A";
                }
                else
                {
                    int received = Convert.ToInt32(textBox2.Text);
                    textBox3.Text = (received - total).ToString();
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  MessageBox.Show(""+cbo_unit.SelectedItem);
            DialogResult da = MessageBox.Show("Do you really wanna Place Order", "Update Warining!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (da == DialogResult.Yes)
            {
                if (textBox1.Text != "" && cbo_product.SelectedItem != "" && cbo_unit.SelectedIndex != 0)
                {
                    string insert = "[dbo].[placeorder]";
                    SqlCommand cmd1 = new SqlCommand(insert, con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    if(txt_ordernumber.Text == null)
                    {
                        cmd1.Parameters.Add("@OrderNumber", SqlDbType.Int).Value = 1;
                    }
                    else
                    {
                        cmd1.Parameters.Add("@OrderNumber", SqlDbType.Int).Value = Convert.ToInt32(txt_ordernumber.Text);
                    }
                    cmd1.Parameters.Add("@CustomerId", SqlDbType.Int).Value = Convert.ToInt32(customerid);
                    cmd1.Parameters.Add("@TotalAmount", SqlDbType.Int).Value = Convert.ToInt32(txtBox_total.Text);
                    cmd1.Parameters.Add("@ProductId", SqlDbType.Int).Value = Convert.ToInt32(productid);
                    cmd1.Parameters.Add("@UnitPrice", SqlDbType.Int).Value = Convert.ToInt32(txtBox_priceperunit.Text);
                    cmd1.Parameters.Add("@Quantity", SqlDbType.Int).Value =Convert.ToInt32(cbo_unit.SelectedItem);

           
                    try
                 {
                     con.Open();
                     cmd1.ExecuteNonQuery();
             
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show("" + ex);
             
                 }
                 finally
                 {
                     con.Close();
                     MessageBox.Show("Placed Order Successfully");
                     productload();
             
                 }

                }
                else
                {
                    MessageBox.Show("You have to Add all Information of Buyer ");
                }

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (char.IsDigit(ch) && ch != 8 && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8 && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void txtBox_total_TextChanged(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(txtBox_total.Text);
            try
            {
                if (textBox2.Text == "" || (Convert.ToInt32(textBox2.Text) <= total))
                {
                    textBox3.Text = "N/A";
                }
                else
                {
                    int received = Convert.ToInt32(textBox2.Text);
                    textBox3.Text = (received - total).ToString();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void txtBox_priceperunit_TextChanged(object sender, EventArgs e)
        {
            int priceperunit = Convert.ToInt32(txtBox_priceperunit.Text);
            int unit = Convert.ToInt32(cbo_unit.SelectedItem);
            txtBox_total.Text = (priceperunit * unit).ToString();
        }
    }
}
