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
    public partial class Customer : UserControl
    {
        int id;
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=mart;Integrated Security=True");
        public Customer()
        {
            InitializeComponent();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            customerload();
            countryload();
            cbo_sex.SelectedIndex = 0;
            cbo_sex.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void customerload()
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("[dbo].[customersearch]", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@firstname", SqlDbType.Int).Value = "%";
            cmd2.ExecuteNonQuery();
            SqlDataAdapter DA = new SqlDataAdapter(cmd2);
            DataSet ds = new DataSet();
            DA.Fill(ds, "Getbyusername");
            dataGridView1.DataSource = ds.Tables["Getbyusername"];
            dataGridView1.Columns[0].Visible = false;
            con.Close();
        }
        private void fieldclear()
        {
            txtbox_firstname.Text = string.Empty;
            txtbox_lastname.Text = string.Empty;
            txtbox_phone.Text = string.Empty;
            txtbox_search.Text = string.Empty;
            cbo_sex.SelectedIndex = 0;
            txtbox_email.Text = string.Empty;

        }
        private void countryload()
        {

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

        private void btn_clear_Click(object sender, EventArgs e)
        {
            fieldclear();
        }
        int countryid;
        private void cbo_country_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedcountry = cbo_country.SelectedItem.ToString();
            con.Open();
            string sql = " select Countryid from Country where CountryName =@sel ";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@sel", selectedcountry);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            countryid = Convert.ToInt32(reader["Countryid"]);
            con.Close();
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Do you really wanna insert?", "Insert Warning !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {

                if (txtbox_firstname.Text != "" && txtbox_lastname.Text != "" & txtbox_email.Text != "" && cbo_sex.SelectedIndex != 0 && txtbox_phone.Text != "")
                {
                    string insert = "insert into Customer(firstname,lastname,sex,email,dob,phone,CountryId)"
                        +" values(@firstname,@lastname,@sex,@email,@dob,@phone,@CountryId)";
                    SqlCommand cmd1 = new SqlCommand(insert, con);
                    cmd1.Parameters.Add("@firstname", SqlDbType.Text).Value = txtbox_firstname.Text;
                    cmd1.Parameters.Add("@lastname", SqlDbType.Text).Value = txtbox_lastname.Text;
                    cmd1.Parameters.Add("@sex", SqlDbType.Text).Value = cbo_sex.SelectedItem;
                    cmd1.Parameters.Add("@email", SqlDbType.Text).Value = txtbox_email.Text;
                    cmd1.Parameters.Add("@dob", SqlDbType.Text).Value = dateTimePicker1.Text;
                    cmd1.Parameters.Add("@phone", SqlDbType.Text).Value = txtbox_phone.Text;
                    cmd1.Parameters.Add("@CountryId", SqlDbType.Int).Value = countryid;
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
                        MessageBox.Show("Customer Created Successfully");
                        customerload();
                    }
                }
                else
                {
                    MessageBox.Show("You must fill all the information");
                }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Do you really wanna Delete information from database?", "Warning Message !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {


                if (txtbox_firstname.Text != "" && txtbox_lastname.Text != "" & txtbox_email.Text != "" && cbo_sex.SelectedIndex != 0 && txtbox_phone.Text != "")
                {

                    {


                        string update = "delete  from Customer where CustomerId = @id";
                        SqlCommand cmd1 = new SqlCommand(update, con);
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
                            MessageBox.Show("User deleted Successfully");
                            customerload();

                        }



                    }

                }
                else
                {
                    MessageBox.Show("You must fill all the information to delete from database");
                }
            }
        }

        private void txtbox_search_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("[dbo].[customersearch]", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@firstname", SqlDbType.Int).Value = "%" + txtbox_search.Text + "%";
            cmd2.ExecuteNonQuery();
            SqlDataAdapter DA = new SqlDataAdapter(cmd2);
            DataSet ds = new DataSet();
            DA.Fill(ds, "Getbyusername");
            dataGridView1.DataSource = ds.Tables["Getbyusername"];
            dataGridView1.Columns[0].Visible = false;
            con.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try { id = Convert.ToInt32((dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())); }
            catch
            {

            }

            txtbox_firstname.Text = (dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            txtbox_lastname.Text = (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            cbo_sex.SelectedItem = (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
            txtbox_email.Text = (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            dateTimePicker1.Text = (dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
            txtbox_phone.Text = (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
            cbo_country.SelectedItem = (dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
        }

        private void btn_update_Click(object sender, EventArgs e)
        {

            DialogResult d = MessageBox.Show("Do you really wanna Update?", "Warning Message !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {

                if (txtbox_firstname.Text != "" && txtbox_lastname.Text != "" & txtbox_email.Text != "" && cbo_sex.SelectedIndex != 0 && txtbox_phone.Text != "")
                {


                    string update = "update Customer set firstname = @firstname,lastname= @lastname,sex= @sex,email= @email,dob= @dob,phone=@phn, CountryId = @countryid  where CustomerId = @od";
                    SqlCommand cmd1 = new SqlCommand(update, con);

                    cmd1.Parameters.Add("@firstname", SqlDbType.Text).Value = txtbox_firstname.Text;
                    cmd1.Parameters.Add("@lastname", SqlDbType.Text).Value = txtbox_lastname.Text;
                    cmd1.Parameters.Add("@sex", SqlDbType.Text).Value = cbo_sex.SelectedItem;
                    cmd1.Parameters.Add("@email", SqlDbType.Text).Value = txtbox_email.Text;
                    cmd1.Parameters.Add("@dob", SqlDbType.Text).Value = dateTimePicker1.Text;
                    cmd1.Parameters.Add("@phn", SqlDbType.Text).Value = txtbox_phone.Text;
                    cmd1.Parameters.Add("@od", SqlDbType.Int).Value = id;
                    cmd1.Parameters.Add("@CountryId", SqlDbType.Int).Value = countryid;
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
                        MessageBox.Show("Customer Updated Successfully");
                        customerload();

                    }



                }
                else
                {
                    MessageBox.Show("You have to select atleast one data to update");
                }



            }
        }
    }
}
