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
    public partial class supplier : UserControl
    {
        public supplier()
        {
            InitializeComponent();
        }
        string constring = "Data Source=.;Initial Catalog=mart;Integrated Security=True";
        SqlCommand cmd;
        SqlDataAdapter da;
        int ctryid;
        int catid;
        private void supplier_Load(object sender, EventArgs e)
        {
            supp();
            countryload();
            categoryload();
        }
        private void supp()
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd2 = new SqlCommand("[dbo].[suppliersearch]", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@like", SqlDbType.VarChar).Value = "%" + txtBox_search.Text + "%";
            cmd2.ExecuteNonQuery();
            SqlDataAdapter DA = new SqlDataAdapter(cmd2);
            DataSet ds = new DataSet();
            DA.Fill(ds, "Gridviewload");
            dataGridView1.DataSource = ds.Tables["GridViewload"];
            con.Close();
        }
        private void countryload()
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string sql = "select CountryName from Country";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            DataTable Table = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(Table);
            foreach (DataRow dr in Table.Rows)
            {
                cbo_country.Items.Add(dr["CountryName"].ToString());
            }
            con.Close();
            cbo_country.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void categoryload()
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            cmd = new SqlCommand("Select categoryName from category order by categoryName", con);
            cmd.ExecuteNonQuery();
            DataTable Table = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(Table);
            foreach (DataRow dr in Table.Rows)
            {
                cbo_categoryid.Items.Add(dr["categoryName"].ToString());
            }
            con.Close();
            cbo_categoryid.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void clearload()
        {
            txtBox_companyname.Text = string.Empty;
            cbo_categoryid.Text = string.Empty;
            txtBox_Address.Text = string.Empty;
            txtBox_phone.Text = string.Empty;
            cbo_country.Text = string.Empty;
            txtbox_email.Text = string.Empty;
            txtBox_contactName.Text = string.Empty;
        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            clearload();
        }

        private void cbo_country_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedcountry = cbo_country.SelectedItem.ToString();
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string sql = " select Countryid from Country where CountryName =@sel ";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@sel", selectedcountry);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            ctryid = Convert.ToInt32(reader["Countryid"]);
            //  MessageBox.Show(""+ctryid);
            con.Close();
        }

        private void cbo_categoryid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedcountry = cbo_categoryid.SelectedItem.ToString();
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string sql = " select categoryid from category where categoryName =@sel ";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@sel", cbo_categoryid.SelectedItem);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            catid = Convert.ToInt32(reader["categoryid"]);
            //  MessageBox.Show(""+catid);

            con.Close();
        }

        private void txtBox_search_TextChanged(object sender, EventArgs e)
        {
            supp();
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);

            if (txtBox_companyname.Text != "" && txtBox_contactName.Text != "" & txtbox_email.Text != "" && txtBox_phone.Text != "" && txtBox_Address.Text != "")
            {
                DialogResult d = MessageBox.Show("Do you really wanna Delete information from database?", "Warning Message !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.Yes)
                {
                    string sql = "  select * from Supplier where CompanyName = @companyname";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@companyname", txtBox_companyname.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "checkcompany");
                    if (ds.Tables["checkcompany"].Rows.Count == 0)
                    {

                        string insert = "insert into Supplier([CompanyName],[Categoryid],[ContactName],[phone],[CountryId],[Address],[Email]) "
                                    +"values(@CompanyName,@Categoryid,@ContactName,@Phone,@CountryId,@Address,@Email)";
                        SqlCommand cmd1 = new SqlCommand(insert, con);

                        cmd1.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = txtBox_companyname.Text;
                        cmd1.Parameters.Add("@Categoryid", SqlDbType.Int).Value = catid;
                        cmd1.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = txtBox_contactName.Text;
                        cmd1.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = txtBox_phone.Text;
                        cmd1.Parameters.Add("@CountryId", SqlDbType.Int).Value = ctryid;
                        cmd1.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txtBox_Address.Text;
                        cmd1.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtbox_email.Text;



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
                            MessageBox.Show("Supplier Added Successfully");
                            supp();

                        }
                    }
                    else
                    {
                        MessageBox.Show("Company Name Already in Database :");
                    }
                }
            }
            else
            {
                MessageBox.Show("You must fill all the information");
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            DialogResult da = MessageBox.Show("Do you really wanna Update Supplier", "Update Warining!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (da == DialogResult.Yes)
            {
                if (txtBox_contactName.Text != "" && txtBox_companyname.Text != "" & txtbox_email.Text != "" && txtBox_phone.Text != "" && txtbox_email.Text != "")
                {
                    SqlConnection con = new SqlConnection(constring);
                    string update = "update Supplier set CompanyName = @CompanyName,Categoryid= @Categoryid,ContactName= @ContactName,Phone= @Phone,CountryId= @CountryId,Address=@Address,Email=@Email  where SupplierId = @id";
                    SqlCommand cmd1 = new SqlCommand(update, con);

                    cmd1.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = txtBox_companyname.Text;
                    cmd1.Parameters.Add("@Categoryid", SqlDbType.Int).Value = catid;
                    cmd1.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = txtBox_contactName.Text;
                    cmd1.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = txtBox_phone.Text;
                    cmd1.Parameters.Add("@CountryId", SqlDbType.Int).Value = ctryid;
                    cmd1.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txtBox_Address.Text;
                    cmd1.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtbox_email.Text;
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
                        MessageBox.Show("Supplier Updated Successfully");
                        supp();

                    }

                }
                else
                {
                    MessageBox.Show("You have to select atleast one data to update");
                }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            int i = id;
            DialogResult d = MessageBox.Show("Do you really wanna Delete information from database?", "Warning Message !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {


                if (txtBox_contactName.Text != "" && txtBox_companyname.Text != "" & txtbox_email.Text != "" && txtBox_phone.Text != "" && txtbox_email.Text != "")
                {

                    {
                        SqlConnection con = new SqlConnection(constring);

                        string update = "delete  from  Supplier where SupplierId = @id";
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
                            MessageBox.Show("Supplier deleted Successfully");
                            supp();

                        }



                    }

                }
                else
                {
                    MessageBox.Show("You must fill all the information to delete from database");
                }
            }
        }
        int id;
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            }
            catch
            {

            }


            txtBox_companyname.Text = (dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            cbo_categoryid.SelectedItem = (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            txtBox_contactName.Text = (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
            txtBox_phone.Text = (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            cbo_country.SelectedItem = (dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
            txtBox_Address.Text = (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
            txtbox_email.Text = (dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
        }

        private void txtBox_contactName_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (char.IsDigit(ch) && ch != 8 && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void txtBox_phone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBox_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8 && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}
