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
            countryload();
            cbo_country.SelectedIndex = 0;
            getuserdetail();
            con.Close();
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
    }
}
