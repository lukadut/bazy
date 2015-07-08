using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace bazy_danych
{
    public partial class DriversForm : Form
    {
        string[] ColumnNames = { "Id", "Imię", "Nazwisko", "Stawka", "Licencja ADR", "Zatrudniony", "W trasie", "Komentarz" };
        Base DataBase;

        public DriversForm(Base dataBase, int? id = null )
        {
            DataBase = dataBase;
            InitializeComponent();
            LoadData(DataBase);
            if (id >= 0 && Functions.FindDriver((int)id, DataBase.DriversList) >= 0)
            {
                this.id.Text = id + "";
                ShowDriver(Functions.FindDriver((int)id,DataBase.DriversList));
            }
                //MessageBox.Show("ustalony?");
        }
        void LoadData(Base DataBase)
        {
            try
            {
                DataSet DS = new DataSet();
                BindingSource BS = new BindingSource();
                MySqlDataAdapter MSDA = new MySqlDataAdapter("select * from drivers", DataBase.MySqlConnector);
                MSDA.Fill(DS);
                for (int i = 0; i < DS.Tables[0].Columns.Count; i++)
                    DS.Tables[0].Columns[i].ColumnName = ColumnNames[i];
                BS.DataSource = DS.Tables[0];

                dataGridView1.DataSource = BS;

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count -8;
                }
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 4;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd w połączeniu z bazą.");
                //this.Close();
            }
            Functions.TranslateTrueFalse(dataGridView1);
        }
        void ClearForm()
        {
            name.Text = "";
            surname.Text = "";
            id.Text = "";
            comment.Text = "";
            wage.Value = 0;
            adr.Checked = false;
            employed.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            int Id = Functions.FindDriver(int.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString()), DataBase.DriversList);
            //int Id = -1 + int.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            ShowDriver(Id);
            //name.Text = DataBase.DriversList[Id].Name;
            //surname.Text = DataBase.DriversList[Id].Surname;
            //comment.Text = DataBase.DriversList[Id].Comment;
            //wage.Value = DataBase.DriversList[Id].Wage;
            //employed.Checked = DataBase.DriversList[Id].Employed;
            //adr.Checked = DataBase.DriversList[Id].Adr;
            //if (DataBase.DriversList[Id].Busy) 
            //{
            //    this.employed.Hide();
            //}
            //else
            //{
            //    this.employed.Show();
            //}
        }
        private void ShowDriver(int Id)
        {
            name.Text = DataBase.DriversList[Id].Name;
            surname.Text = DataBase.DriversList[Id].Surname;
            comment.Text = DataBase.DriversList[Id].Comment;
            wage.Value = DataBase.DriversList[Id].Wage;
            employed.Checked = DataBase.DriversList[Id].Employed;
            adr.Checked = DataBase.DriversList[Id].Adr;
            if (DataBase.DriversList[Id].Busy)
            {
                this.employed.Hide();
            }
            else
            {
                this.employed.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int Id = -1 + int.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            if (id.Text.Length == 0)
                return;
            //int Id = -1 + int.Parse(id.Text);
            int Id = Functions.FindDriver(int.Parse(id.Text), DataBase.DriversList);
            if (Id >= 0)
            {
                DataBase.DriversList[Id].Name = name.Text;
                DataBase.DriversList[Id].Surname = surname.Text;
                DataBase.DriversList[Id].Comment = comment.Text;
                DataBase.DriversList[Id].Wage = (uint)wage.Value;
                DataBase.DriversList[Id].Employed = employed.Checked;
                DataBase.DriversList[Id].Adr = adr.Checked;
                DataBase.UpdateDrivers(Id, int.Parse(id.Text));
            }
            LoadData(DataBase);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataBase.AddDriver(name.Text, surname.Text, (uint)wage.Value, adr.Checked, true, comment.Text);
            //DataBase.DriversList.Add(new Drivers());
            LoadData(DataBase);
        }

        private void DriversForm_Resize(object sender, EventArgs e)
        {
            
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count -8;
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 4;
            
        }
    }
}
