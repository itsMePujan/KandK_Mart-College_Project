using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KandK.admin
{
    public partial class Administrator_Dasboard : Form
    {
        public Administrator_Dasboard(int id)
        {
            InitializeComponent();
            a = id;
        }

        public Administrator_Dasboard()
        {
        }

        int a;
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Administrator_Dasboard_Load(object sender, EventArgs e)
        {
            ll();
            home_panel.Show();
            home home = new home();
            panel4.Controls.Add(home);

        }
        private void ll()
        {
            panel4.Controls.Clear();
            employee_panel.Hide();
            product_panel.Hide();
            supplier_panel.Hide();
            home_panel.Hide();
            setting_panel.Hide();
            report_panel.Hide();
            costumer_panel.Hide();
            Backup_panel.Hide();
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            ll();
            home_panel.Show();
            home home = new home();
            panel4.Controls.Add(home);
        }

        private void btn_employee_Click(object sender, EventArgs e)
        {
            ll();
            employee_panel.Show();
            employee employee = new employee();
            panel4.Controls.Add(employee);
            
        }

        private void btn_products_Click(object sender, EventArgs e)
        {
            ll();
            product_panel.Show();
            product product = new product();
            panel4.Controls.Add(product);
        }

        private void btn_sales_Click(object sender, EventArgs e)
        {
            ll();
            report report = new report();
            report_panel.Show();
            panel4.Controls.Add(report);
        }

        private void btn_orders_Click(object sender, EventArgs e)
        {
            ll();
            supplier_panel.Show();
            supplier supplier = new supplier(); 
            panel4.Controls.Add(supplier);
        }

        private void btn_customer_Click(object sender, EventArgs e)
        {
            ll();
            costumer_panel.Show();
            Customer customer = new Customer();
            panel4.Controls.Add(customer);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ll();
            int b = a;
            setting setting = new setting(b);
            setting_panel.Show();
            panel4.Controls.Add(setting);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start start = new Start();
               this.Hide();
            start.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ll();
            Backup_panel.Show();
            backup backup = new backup();
            panel4.Controls.Add(backup);
        }
        private bool _dragginh = false;
        private Point _start_point = new Point(0, 0);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragginh = true;
            _start_point = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            if (_dragginh)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);

            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragginh = false;
        }

        private void panel4_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
