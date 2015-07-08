﻿using System;
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
    public partial class CarsForm : Form
    {
        string[] ColumnNames = { "Id", "Numer rejestracji", "Marka", "Model", "Ładowność", "Jest używany", "Sprzedany", "Komentarz" };
        Base DataBase;

        public CarsForm(Base dataBase, int? id = null )
        {
            DataBase = dataBase;
            InitializeComponent();
            LoadData(DataBase);
            if (id >= 0 && Functions.FindCar((int)id, DataBase.CarsList) >= 0)
            {
                this.id.Text = id + "";
                ShowCar(Functions.FindCar((int)id,DataBase.CarsList));
            }
                //MessageBox.Show("ustalony?");
        }
        void LoadData(Base DataBase)
        {
            try
            {
                DataSet DS = new DataSet();
                BindingSource BS = new BindingSource();
                MySqlDataAdapter MSDA = new MySqlDataAdapter("select * from cars", DataBase.MySqlConnector);
                MSDA.Fill(DS);
                for (int i = 0; i < DS.Tables[0].Columns.Count; i++)
                    DS.Tables[0].Columns[i].ColumnName = ColumnNames[i];
                BS.DataSource = DS.Tables[0];

                dataGridView1.DataSource = BS;

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count -6;
                }
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 4;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd w połączeniu z bazą.");
                //this.Close();
            }
        }
        void ClearForm()
        {
            make.Text = "";
            model.Text = "";
            id.Text = "";
            comment.Text = "";
            plate.Text = "";
            carry.Value = 0;
            //isUsed.Checked = false;
            sold.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            int Id = Functions.FindCar(int.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString()), DataBase.CarsList);
            //int Id = -1 + int.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            ShowCar(Id);
            //name.Text = DataBase.CarsList[Id].Name;
            //surname.Text = DataBase.CarsList[Id].Surname;
            //comment.Text = DataBase.CarsList[Id].Comment;
            //wage.Value = DataBase.CarsList[Id].Wage;
            //employed.Checked = DataBase.CarsList[Id].Employed;
            //adr.Checked = DataBase.CarsList[Id].Adr;
            //if (DataBase.CarsList[Id].Busy) 
            //{
            //    this.employed.Hide();
            //}
            //else
            //{
            //    this.employed.Show();
            //}
        }
        private void ShowCar(int Id)
        {
            make.Text = DataBase.CarsList[Id].Make;
            model.Text = DataBase.CarsList[Id].Model;
            comment.Text = DataBase.CarsList[Id].Comment;
            plate.Text = DataBase.CarsList[Id].Plate;
            carry.Value = DataBase.CarsList[Id].Carry;
            sold.Checked = DataBase.CarsList[Id].Sold;
            //isUsed.Checked = DataBase.CarsList[Id].IsUsed;
            if (DataBase.CarsList[Id].IsUsed)
            {
                this.sold.Hide();
            }
            else
            {
                this.sold.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int Id = -1 + int.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            if (id.Text.Length == 0)
                return;
            if (!Functions.AllowedPlate(plate.Text, DataBase.CarsList))
            {
                MessageBox.Show("Taki numer rejestracji już występuje");
                return;
            }
            //int Id = -1 + int.Parse(id.Text);
            int Id = Functions.FindCar(int.Parse(id.Text), DataBase.CarsList);
            if (Id >= 0)
            {
                DataBase.CarsList[Id].Make = make.Text;
                DataBase.CarsList[Id].Model = model.Text;
                DataBase.CarsList[Id].Comment = comment.Text;
                DataBase.CarsList[Id].Plate = plate.Text;
                DataBase.CarsList[Id].Carry = (uint)carry.Value;
                DataBase.CarsList[Id].Sold = sold.Checked;
                //DataBase.CarsList[Id].IsUsed = isUsed.Checked;
                DataBase.UpdateCars(Id, int.Parse(id.Text));
            }
            LoadData(DataBase);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Functions.AllowedPlate(plate.Text, DataBase.CarsList))
            {
                DataBase.AddCar(plate.Text, make.Text, model.Text, (uint)carry.Value, false, false, comment.Text);
                //DataBase.CarsList.Add(new Cars());
                LoadData(DataBase);
            }
            else
            {
                MessageBox.Show("Istnieje już auto o takiej rejestracji");
            }
        }


        private void CarsForm_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count - 6;
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width +=4;
        }
    }
}