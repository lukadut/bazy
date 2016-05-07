using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bazy
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            CarsForm CF = new CarsForm();
            CF.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DriversForm DF = new DriversForm();
            DF.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CargoForm CF = new CargoForm();
            CF.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CompaniesForm CF = new CompaniesForm();
            CF.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FreightsForm FF = new FreightsForm();
            FF.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShippingForm SF = new ShippingForm();
            SF.Show();
        }
    }
}
