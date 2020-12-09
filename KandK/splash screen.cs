using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KandK
{
    public partial class splash_screen : Form
    {
        public splash_screen()
        {
            InitializeComponent();
        }

        private void splash_screen_Load(object sender, EventArgs e)
        {
            timer1.Start();
   
        
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(6);
            percent.Text = progressBar1.Value.ToString()+"%";
            if (progressBar1.Value == 100)
            {
                timer1.Stop();
                Start start = new Start();
                start.Show();
                this.Hide();
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void percent_Click(object sender, EventArgs e)
        {

        }
    }
}
