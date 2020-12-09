using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KandK.emp
{
    public partial class Employee_dash : Form
    {
        int i;
        public Employee_dash(int id)
        {
            InitializeComponent();
            i = id;
            panel();
            home_panel.Show();
            admin.home home = new admin.home();
            panel4.Controls.Add(home);

        }

        public Employee_dash()
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start start = new Start();
            this.Hide();
            start.Show();
        }
        private void panel()
        {
            panel4.Controls.Clear();
            product_panel.Hide();
            sales_panel.Hide();
            billing_panel.Hide();
            setting_panel.Hide();
            panel_costumer.Hide();
            home_panel.Hide();
        }
        private void btn_products_Click(object sender, EventArgs e)
        {
            panel();
            product_panel.Show();
            admin.product product = new admin.product();
            panel4.Controls.Add(product);
        }

        private void btn_addproduct_Click(object sender, EventArgs e)
        {
            panel();
            report rep = new report();
            panel4.Controls.Add(rep);
            sales_panel.Show();
        }

        private void btn_customer_Click(object sender, EventArgs e)
        {
            panel();
            panel_costumer.Show();
            Customer customer = new Customer();
            panel4.Controls.Add(customer);
        }

        private void btn_Billing_Click(object sender, EventArgs e)
        {
            panel();
            billing_panel.Show();
            billing bill = new billing();
            panel4.Controls.Add(bill);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel();
            setting_panel.Show();
            int b = i;
            emp.setting setting = new setting(b);
            panel4.Controls.Add(setting);
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            panel();
            home_panel.Show();
            admin.home home = new admin.home();
            panel4.Controls.Add(home);

        }
    }
}
