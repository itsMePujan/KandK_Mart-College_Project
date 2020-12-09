using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace KandK
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        string constring = "Data Source=.;Initial Catalog=mart;Integrated Security=True";
        int id;
        private void button1_Click(object sender, EventArgs e)
        {

            {


                if (txtbox_username.Text != "" && txtbox_password.Text != "")
                {

                    SqlConnection con = new SqlConnection(constring);
                    con.Open();
                    string sql = "select * from User_detail where username =@user and password =@pass";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    byte[] pass = System.Text.Encoding.UTF8.GetBytes(txtbox_password.Text.ToString());
                    cmd.Parameters.AddWithValue("@user", txtbox_username.Text);
                    cmd.Parameters.AddWithValue("@pass", Md5hash(pass));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    con.Close();

                    da.Fill(ds, "checkuser");
                    if (ds.Tables["checkuser"].Rows.Count == 1)
                    {
                        usrid(); /// to get userid 
                        con.Open();
                        SqlDataReader dr1 = cmd.ExecuteReader();

                        if (dr1.Read())
                        {
                            string usrtyp = (dr1["usertype"].ToString());
                            if (usrtyp == "Admin")
                            {
                                MessageBox.Show("WELCOME TO ADMINISTRATOR PANEL");
                                this.Hide();
                                admin.Administrator_Dasboard adm = new admin.Administrator_Dasboard(id);
                                adm.Show();
                            }
                            else
                            {
                                MessageBox.Show("WELCOME TO EMPLOYEE PANEL");
                                this.Hide();
                                emp.Employee_dash employeedash = new emp.Employee_dash(id);
                                employeedash.Show();

                            }
                        }
                        con.Close();

                    }
                    else
                    {
                        MessageBox.Show("User Not Found");
                    }
                }
                else
                {


                    MessageBox.Show("You must fill credential to login!");
                }
            }
        }
        private void usrid()
        {

            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string sql = "select * from User_detail where username =@user and password =@pass";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@user", txtbox_username.Text);
            byte[] pass = System.Text.Encoding.UTF8.GetBytes(txtbox_password.Text.ToString());
            cmd.Parameters.AddWithValue("@pass", Md5hash(pass));
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            id = Convert.ToInt32(reader["userid"]);
            con.Close();
        }
        public string Md5hash(byte[] value)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                var hash = md5.ComputeHash(value);

                return Convert.ToBase64String(hash);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private bool _dragginh = false;
        private Point _start_point = new Point(0, 0);
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            _dragginh = true;
            _start_point = new Point(e.X, e.Y);
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragginh)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);

            }
        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            _dragginh = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        
        }

        private void Start_Load(object sender, EventArgs e)
        {
            admin.Administrator_Dasboard bla = new admin.Administrator_Dasboard();
            bla.Show();
        }
    }
}
