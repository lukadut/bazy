using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bazy_danych
{
    public partial class Form1 : Form
    {
        Base DataBase;
        public Form1()
        {
            InitializeComponent();
            DataBase = new Base();
            try
            {
                DataBase.ConnectToDataBase();
                //MessageBox.Show(DataBase.LoadDrivers());
                //MessageBox.Show(DataBase.DriversList.Count + "");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DriversForm DF = new DriversForm(DataBase);
            DF.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CarsForm CF = new CarsForm(DataBase);
            CF.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FreightsListForm FLF = new FreightsListForm(DataBase);
            FLF.Show();
        }
    }
}
