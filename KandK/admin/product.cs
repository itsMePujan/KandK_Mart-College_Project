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
    public partial class product : UserControl
    {
        public product()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=mart;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        int cid;
        int sid;
        int id;
        private void product_Load(object sender, EventArgs e)
        {
            productload();
            categoryload();
            supplierload();
        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            fieldClear();
        }
        private void fieldClear()
        {
            txtbox_productname.Text = string.Empty;
            cbo_supplier.Text = string.Empty;
            cbo_category.Text = string.Empty;
            txtBox_stock.Text = string.Empty;
            txtBox_CostPrice.Text = string.Empty;
            txtBox_SellingPrice.Text = string.Empty;
        }
        private void productload()
        {

            con.Open();
            SqlCommand cmd2 = new SqlCommand("[dbo].[Products]", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@productname", SqlDbType.Int).Value = "%%";
            cmd2.ExecuteNonQuery();
            SqlDataAdapter DA = new SqlDataAdapter(cmd2);
            DataSet ds = new DataSet();
            DA.Fill(ds, "Gridviewload");
            dataGridView1.DataSource = ds.Tables["GridViewload"];
            con.Close();

        }
        private void categoryload()
        {
            con.Open();
            cmd = new SqlCommand("Select categoryName from category order by categoryName", con);
            cmd.ExecuteNonQuery();
            DataTable Table = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(Table);
            foreach (DataRow dr in Table.Rows)
            {
                cbo_category.Items.Add(dr["categoryName"].ToString());
            }
            con.Close();
            cbo_category.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void supplierload()
        {
            con.Open();
            cmd = new SqlCommand("Select CompanyName from  Supplier order by CompanyName", con);
            cmd.ExecuteNonQuery();
            DataTable Table = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(Table);
            foreach (DataRow dr in Table.Rows)
            {
                cbo_supplier.Items.Add(dr["CompanyName"].ToString());
            }
            con.Close();
            cbo_supplier.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void txtBox_search_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("[dbo].[Products]", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@productname", SqlDbType.Int).Value = "%" + txtBox_search.Text + "%";
            cmd2.ExecuteNonQuery();
            SqlDataAdapter DA = new SqlDataAdapter(cmd2);
            DataSet ds = new DataSet();
            DA.Fill(ds, "Gridviewload");
            dataGridView1.DataSource = ds.Tables["GridViewload"];
            con.Close();
        }

        private void txtBox_search_MouseClick(object sender, MouseEventArgs e)
        {
            txtBox_search.Text = string.Empty;
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtbox_productname.Text = (dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            cbo_category.SelectedItem = (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            cbo_supplier.SelectedItem = (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
            txtBox_stock.Text = (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            txtBox_CostPrice.Text = (dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
            txtBox_SellingPrice.Text = (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
            try
            {
                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            }
            catch
            {

            }
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            DialogResult da = MessageBox.Show("Do you really wanna New Product", "Update Warining!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (da == DialogResult.Yes)
            {
                if (txtbox_productname.Text != "" && txtBox_stock.Text != "" & txtBox_CostPrice.Text != "")
                {


                    string insert = "insert into product([ProductName],[categoryid],[supplierId],[stock],[costPrice],[sellingPrice]) "
                                    +"values(@productname,@categoryid,@supplierid,@stock,@costprice,@sellingprice)";
                    SqlCommand cmd1 = new SqlCommand(insert, con);
                    cmd1.Parameters.Add("@productname", SqlDbType.Text).Value = txtbox_productname.Text;
                    cmd1.Parameters.Add("@categoryid", SqlDbType.Int).Value = cid;
                    cmd1.Parameters.Add("@supplierId", SqlDbType.Int).Value = sid;
                    cmd1.Parameters.Add("@stock", SqlDbType.Int).Value = txtBox_stock.Text;
                    cmd1.Parameters.Add("@costprice", SqlDbType.Int).Value = txtBox_CostPrice.Text;
                    cmd1.Parameters.Add("@sellingprice", SqlDbType.Int).Value = txtBox_SellingPrice.Text;

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
                        MessageBox.Show("Product Added Successfully");
                        productload();
                    }

                }
                else
                {
                    MessageBox.Show("You have to Add all Information of Product");
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult da = MessageBox.Show("Do you really wanna Update Product", "Update Warining!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (da == DialogResult.Yes)
            {
                if (txtbox_productname.Text != "" && txtBox_stock.Text != "" & txtBox_CostPrice.Text != "")
                {

                    string update = "update product set ProductName = @ProductName,categoryid= @categoryid,supplierId= @supplierId,stock= @stock,costPrice= @costPrice,sellingPrice=@sellingPrice  where ProductId = @id";
                    SqlCommand cmd1 = new SqlCommand(update, con);

                    cmd1.Parameters.Add("@ProductName", SqlDbType.Text).Value = txtbox_productname.Text;
                    cmd1.Parameters.Add("@categoryid", SqlDbType.Int).Value = cid;
                    cmd1.Parameters.Add("@supplierId", SqlDbType.Int).Value = sid;
                    cmd1.Parameters.Add("@stock", SqlDbType.Int).Value = txtBox_stock.Text;
                    cmd1.Parameters.Add("@costPrice", SqlDbType.Int).Value = txtBox_CostPrice.Text;
                    cmd1.Parameters.Add("@sellingPrice", SqlDbType.Int).Value = txtBox_SellingPrice.Text;
                    cmd1.Parameters.Add("@id", SqlDbType.Int).Value = id;
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
                        MessageBox.Show("Product Updated Successfully");
                        productload();

                    }

                }
                else
                {
                    MessageBox.Show("You have to select atleast one data to update");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = id;
            DialogResult d = MessageBox.Show("Do you really wanna Delete information from database?", "Warning Message !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {


                if (txtbox_productname.Text != "" && txtBox_CostPrice.Text != "" & txtBox_stock.Text != "" && txtBox_stock.Text != "")
                {

                    {


                        string update = "delete  from  product where ProductId = @id";
                        SqlCommand cmd1 = new SqlCommand(update, con);
                        cmd1.Parameters.Add("@id", SqlDbType.Int).Value = i; //i;
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
                            MessageBox.Show("Product deleted Successfully");
                            productload();

                        }



                    }

                }
                else
                {
                    MessageBox.Show("You must fill all the information to delete from database");
                }
            }
        }

        private void cbo_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedcountry = cbo_category.SelectedItem.ToString();
            con.Open();
            string sql = " select categoryid from category where categoryName =@sel ";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@sel", cbo_category.SelectedItem);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            cid = Convert.ToInt32(reader["categoryid"]);

            con.Close();
        }

        private void cbo_supplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedcountry = cbo_category.SelectedItem.ToString();

            con.Open();
            string sql = " select SupplierId from Supplier where CompanyName =@sel ";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@sel", cbo_supplier.SelectedItem);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            sid = Convert.ToInt32(reader["SupplierId"]);

            con.Close();
        }

        private void txtbox_productname_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (char.IsDigit(ch) && ch != 8 && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void txtBox_stock_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8 && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}
