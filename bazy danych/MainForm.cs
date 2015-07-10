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
    public partial class MainForm : Form
    {
        Base DataBase;
        public MainForm()
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

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime czas;

            MySql.Data.MySqlClient.MySqlConnection MySqlConnector;
            //string result = "";
            string Server = "localhost";
            string DataBase = "projekt";
            string UID = "root";
            string Charset = "utf8";
            string Password = "";
            string Connection = "SERVER=" + Server + ";" + "DATABASE=" + DataBase + ";" + "UID=" + UID + ";" + "PASSWORD=" + Password + ";" + "CHARSET=" + Charset;
            MySqlConnector = new MySql.Data.MySqlClient.MySqlConnection(Connection);
            MySql.Data.MySqlClient.MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "SELECT * FROM shipping";
            MySqlConnector.Open();
            MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.IsDBNull(5))
                {
                    czas = reader.GetDateTime(5);
                
                    //DateTime test = (DateTime)czas;
                    System.Console.WriteLine("miesiac " + czas + "dzien tygodnia "+ (int)czas.DayOfWeek);
                }
                else
                {
                    System.Console.WriteLine("Czas jest nieokreslony");
                }
               // DriversList.Add(new Drivers(reader.GetUInt32("Id"), name /*reader.GetString("Name")*/, reader.GetString("Surname"), reader.GetUInt32("Wage"), reader.GetBoolean("ADR_License"), reader.GetBoolean("Employed"), reader.GetBoolean("Busy"), reader.GetString("Comment")));
                //result += "\n" + reader.GetString("Name") + "	" +reader.GetString("Surname") + "	" +reader.GetUInt32("Wage") + "	" +
                //    reader.GetBoolean("ADR_License") + "	" +reader.GetBoolean("Employed") + "	" +reader.GetString("Comment");
            }
            MySqlConnector.Close();
            //return result;

        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime czas;
            czas = new DateTime();
           // czas = DateTime.Now;
            czas = dateTimePicker1.Value;
            MySql.Data.MySqlClient.MySqlConnection MySqlConnector;
            //string result = "";
            string Server = "localhost";
            string DataBase = "projekt";
            string UID = "root";
            string Charset = "utf8";
            string Password = "";
            string Connection = "SERVER=" + Server + ";" + "DATABASE=" + DataBase + ";" + "UID=" + UID + ";" + "PASSWORD=" + Password + ";" + "CHARSET=" + Charset;
            MySqlConnector = new MySql.Data.MySqlClient.MySqlConnection(Connection);
            MySql.Data.MySqlClient.MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "UPDATE shipping SET arriveTime=@Model WHERE id = @Id";
            cmd.Parameters.AddWithValue("@Model", czas);
            cmd.Parameters.AddWithValue("@Id", 11);

            MySqlConnector.Open();
            cmd.ExecuteNonQuery();
            MySqlConnector.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Functions.TextFormat(textBox1.Text));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CompaniesForm CF = new CompaniesForm(DataBase);
            CF.Show();
        }
    }
}
