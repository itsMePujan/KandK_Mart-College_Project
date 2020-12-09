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
    public partial class backup : UserControl
    {
        public backup()
        {
            InitializeComponent();

        }
        string location;
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=mart;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLocation.Text = folderBrowserDialog1.SelectedPath;
                btnBackup.Enabled = true;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            timer1.Start();
            con.Open();
            String database = con.Database.ToString();
            try
            {
                if (txtLocation.Text == string.Empty)
                {
                    MessageBox.Show("please enter the backup file location");
                }
                else
                {

                    string q = "BACKUP DATABASE [" +database+ "] TO DISK='" + txtLocation.Text + "\\" + "Database" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";

                    SqlCommand scmd = new SqlCommand(q, con);
                    scmd.ExecuteNonQuery();
                    MessageBox.Show("Backup taken successfully", "Backup successs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBackup.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(""+ex);
            }
            con.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pb1.Increment(20);
        }
    }
}

