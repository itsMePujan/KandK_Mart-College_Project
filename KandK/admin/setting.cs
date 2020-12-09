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
    public partial class setting : UserControl
    {   
        public setting(int b)
        {
            InitializeComponent();
            label3.Text = b.ToString();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=mart;Integrated Security=True");
        private void setting_Load(object sender, EventArgs e)
        {
            con.Open();
            infoload();
            countryload();
            cbo_country.SelectedIndex = 0;
            getuserdetail();
            dataGridView1.Columns[0].Visible = false;
            con.Close();
        }
        private void infoload()
        {
            SqlCommand cmd = new SqlCommand("select * from category order by categoryid", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DA.Fill(ds, "getbyid");
            dataGridView1.DataSource = ds.Tables["getbyid"];
        }
        private void getuserdetail()
        {

            string sql = "select * from User_detail where userid = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            // cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = label3.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            txtbox_firstname.Text = (reader["firstname"].ToString());
            txtbox_lastname.Text = (reader["lastname"].ToString());
            cbo_sex.SelectedItem = (reader["sex"].ToString());
            cbo_usetype.SelectedItem = (reader["usertype"].ToString());
            txtbox_email.Text = (reader["email"].ToString());
            txtbox_phone.Text = (reader["phone"].ToString());
            cbo_country.SelectedIndex = Convert.ToInt32(reader["CountryId"].ToString()) - 1;
            txtbox_username.Text = (reader["username"].ToString());
            dateTimePicker1.Text = (reader["dob"].ToString());
            txtbox_salary.Text = (reader["salary"].ToString());
        }
        private void countryload()
        {
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
            cbo_country.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public string Md5hash(byte[] value)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                var hash = md5.ComputeHash(value);

                return Convert.ToBase64String(hash);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txtBox_category.Text != string.Empty)
            {

                SqlCommand cmd = new SqlCommand("insert into category(categoryName) values (@name)", con);

                cmd.Parameters.AddWithValue("@name", SqlDbType.NVarChar).Value = txtBox_category.Text;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    MessageBox.Show("Category added successfully");
                    infoload();
                    con.Close();


                }
            }
            else
            {
                MessageBox.Show("You should  fillup category name to add :");
            }

        }
        int id;
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtBox_category.Text = (dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            try
            {
                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            }
            catch (Exception ex)
            {

            }
           
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("delete from category where Categoryid =@id", con);
            cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MessageBox.Show("Category Deleted");
                infoload();
                con.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((txtBox_newpassword.Text == txtbox_repass.Text) && (txtBox_newpassword.Text != "" & txtbox_repass.Text != ""))
            {
                con.Open();
                string sql = "select * from User_detail where userid = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = label3.Text;
                SqlDataReader reader1 = cmd.ExecuteReader();
                reader1.Read();
                string currentpassword = (reader1["password"].ToString());
                byte[] pass = System.Text.Encoding.UTF8.GetBytes(txtBox_currentpassword.Text.ToString());
                //MessageBox.Show(currentpassword);
                con.Close();
                if (currentpassword == (Md5hash(pass)))
                {
                    string sql1 = "Update User_detail set [password] = @pass where userid = @id";
                    SqlCommand cmd1 = new SqlCommand(sql1, con);
                    byte[] passtohash = System.Text.Encoding.UTF8.GetBytes(txtBox_newpassword.Text.ToString());
                    cmd1.Parameters.Add("@pass", SqlDbType.Text).Value = Md5hash(passtohash);
                    cmd1.Parameters.Add("@id", SqlDbType.Int).Value = label3.Text;
                    try
                    {
                        con.Open();
                        cmd1.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        MessageBox.Show("Updated successfully");
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Current password doesnt mathched");
                }

            }
            else
            {
                MessageBox.Show("New Password Mismatched! or Password Must be Filled ");
            }
        }

        private void txtBox_category_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (char.IsDigit(ch) && ch != 8 && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}
