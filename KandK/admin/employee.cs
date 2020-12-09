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
    public partial class employee : UserControl
    {
        public employee()
        {
            InitializeComponent();
        }
        int id;
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=mart;Integrated Security=True");
        string constring = "Data Source=.;Initial Catalog=mart;Integrated Security=True";
        int countryid;
        private void btn_clear_Click(object sender, EventArgs e)
        {
            fieldclear();
        }
        private void fieldclear()
        {
            txtbox_firstname.Text = string.Empty;
            txtbox_lastname.Text = string.Empty;
            txtbox_phone.Text = string.Empty;
            txtbox_search.Text = string.Empty;
            txtbox_username.Text = string.Empty;
            cbo_sex.SelectedIndex = 0;
            cbo_usetype.SelectedIndex = 0;
            txtbox_email.Text = string.Empty;
            txtbox_salary.Text = string.Empty;
        }
        public void empoyee_load()
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("[dbo].[employee]", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@firstname", SqlDbType.Int).Value = "%";
            cmd2.ExecuteNonQuery();
            SqlDataAdapter DA = new SqlDataAdapter(cmd2);
            DataSet ds = new DataSet();
            DA.Fill(ds, "Getbyusername");
            dataGridView1.DataSource = ds.Tables["Getbyusername"];
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[0].Visible = false;
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

        private void employee_Load(object sender, EventArgs e)
        {
            cbo_usetype.SelectedIndex = 0;
            cbo_sex.SelectedIndex = 0;
            empoyee_load();
            countryload();
            cbo_sex.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo_usetype.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void txtbox_search_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * from User_detail where firstname like @Username and (usertype ='Admin' or usertype ='Employee')", con);
            cmd2.Parameters.AddWithValue("@Username", SqlDbType.Int).Value = "%" + txtbox_search.Text + "%"; //SEARCH BY NAME 
            cmd2.ExecuteNonQuery();
            SqlDataAdapter DA = new SqlDataAdapter(cmd2);
            DataSet ds = new DataSet();
            DA.Fill(ds, "Getbyusername");
            dataGridView1.DataSource = ds.Tables["Getbyusername"];
            con.Close();
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Do you really wanna insert?", "Insert Warning !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {

                if (txtbox_firstname.Text != "" && txtbox_lastname.Text != "" & txtbox_email.Text != "" && cbo_sex.SelectedIndex != 0 && txtbox_phone.Text != "" && txtbox_username.Text != "")
                {

                    SqlConnection con = new SqlConnection(constring);
                    string sql = "select username from User_detail where username = @user";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@user", txtbox_username.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "checkuser");
                    if (ds.Tables["checkuser"].Rows.Count == 0)
                    {
                        string insert = "insert into User_detail(firstname,lastname,sex,email,dob,phone,CountryId,username,[password],usertype,salary)"
                            +" values(@firstname,@lastname,@sex,@email,@dob,@phn,@CountryId,@username,@password,@usertype,@salary)";
                        SqlCommand cmd1 = new SqlCommand(insert, con);

                        cmd1.Parameters.Add("@firstname", SqlDbType.Text).Value = txtbox_firstname.Text;
                        cmd1.Parameters.Add("@lastname", SqlDbType.Text).Value = txtbox_lastname.Text;
                        cmd1.Parameters.Add("@sex", SqlDbType.Text).Value = cbo_sex.SelectedItem;
                        cmd1.Parameters.Add("@email", SqlDbType.Text).Value = txtbox_email.Text;
                        cmd1.Parameters.Add("@dob", SqlDbType.Text).Value = dateTimePicker1.Text;
                        cmd1.Parameters.Add("@phn", SqlDbType.Text).Value = txtbox_phone.Text;
                        cmd1.Parameters.Add("@CountryId", SqlDbType.Int).Value = countryid;
                        cmd1.Parameters.Add("@username", SqlDbType.Text).Value = txtbox_username.Text;
                        string defaultpass = "@dmin";
                        byte[] pass = System.Text.Encoding.UTF8.GetBytes(defaultpass);
                        cmd1.Parameters.Add("@password", SqlDbType.Text).Value = Md5hash(pass);
                        cmd1.Parameters.Add("@usertype", SqlDbType.Text).Value = cbo_usetype.SelectedItem;
                        cmd1.Parameters.Add("@salary", SqlDbType.Int).Value = txtbox_salary.Text;

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
                            MessageBox.Show("User Created Successfully");
                            empoyee_load();

                        }

                    }
                    else
                    {
                        MessageBox.Show("Username already exist");
                    }


                }
                else
                {
                    MessageBox.Show("You must fill all the information");
                }
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Do you really wanna Update?", "Warning Message !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {

                if (txtbox_firstname.Text != "" && txtbox_lastname.Text != "" & txtbox_email.Text != "" && cbo_sex.SelectedIndex != 0 && txtbox_phone.Text != "" && txtbox_username.Text != "")
                {


                    string update = "update User_detail set firstname = @firstname,lastname= @lastname,sex= @sex,email= @email,dob= @dob,phone=@phn, CountryId = @countryid, usertype=@usertype ,salary=@salary where userid = @od";
                    SqlCommand cmd1 = new SqlCommand(update, con);

                    cmd1.Parameters.Add("@firstname", SqlDbType.Text).Value = txtbox_firstname.Text;
                    cmd1.Parameters.Add("@lastname", SqlDbType.Text).Value = txtbox_lastname.Text;
                    cmd1.Parameters.Add("@sex", SqlDbType.Text).Value = cbo_sex.SelectedItem;
                    cmd1.Parameters.Add("@email", SqlDbType.Text).Value = txtbox_email.Text;
                    cmd1.Parameters.Add("@dob", SqlDbType.Text).Value = dateTimePicker1.Text;
                    cmd1.Parameters.Add("@phn", SqlDbType.Text).Value = txtbox_phone.Text;
                    cmd1.Parameters.Add("@usertype", SqlDbType.Text).Value = cbo_usetype.Text;
                    if (txtbox_salary.Text != "")
                    {
                        cmd1.Parameters.Add("@salary", SqlDbType.Int).Value = Convert.ToInt32(txtbox_salary.Text);
                    }
                    else
                    {
                        cmd1.Parameters.Add("@salary", SqlDbType.Int).Value = 0;
                    }
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
                        MessageBox.Show("User Updated Successfully");
                        empoyee_load();

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


                if (txtbox_firstname.Text != "" && txtbox_lastname.Text != "" & txtbox_email.Text != "" && cbo_sex.SelectedIndex != 0 && txtbox_phone.Text != "" && txtbox_username.Text != "")
                {

                    {


                        string update = "delete   from User_detail where userid = @id";
                        SqlCommand cmd1 = new SqlCommand(update, con);
                        cmd1.Parameters.Add("@id", SqlDbType.Int).Value = i;
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
                            empoyee_load();

                        }



                    }

                }
                else
                {
                    MessageBox.Show("You must fill all the information to delete from database");
                }
            }
        }
        public string Md5hash(byte[] value)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                var hash = md5.ComputeHash(value);

                return Convert.ToBase64String(hash);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtbox_firstname.Text = (dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            txtbox_lastname.Text = (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            cbo_sex.Text = (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
            txtbox_email.Text = (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            dateTimePicker1.Text = (dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
            txtbox_phone.Text = (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
            txtbox_username.Text = (dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());
            cbo_usetype.Text = (dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString());
            try
            {
                id = Convert.ToInt32((dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
            }
            catch
            {

            }

            txtbox_salary.Text = (dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString());
            cbo_country.SelectedItem = (dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
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
            countryid = Convert.ToInt32(reader["Countryid"]);
            con.Close();
        }

        private void txtbox_firstname_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (char.IsDigit(ch) && ch != 8 && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void txtbox_lastname_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (char.IsDigit(ch) && ch != 8 && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void txtbox_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != 8 && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void txtbox_username_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cbo_sex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
