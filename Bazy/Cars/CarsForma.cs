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
    public partial class CarsForm : Form
    {
        string[] ColumnNames = { "Id", "Numer rejestracji", "Marka", "Model", "Ładowność", "Jest używany", "Sprzedany", "Komentarz" };
        projektEntities context;
        public CarsForm(int? id = null )
        {
            //DataBase = dataBase;
            InitializeComponent();
            context = new projektEntities();
            make.Leave += Functions.textBox_Leave;
            model.Leave += Functions.textBox_Leave;
            LoadData();
            //if (id >= 0 && Functions.FindCar((int)id, DataBase.CarsList) >= 0)
            //{
            //    this.id.Text = id + "";
            //    ShowCar(Functions.FindCar((int)id,DataBase.CarsList));
            //}
                //MessageBox.Show("ustalony?");
        }
        void LoadData()
        {
            try
            {
                var query = from car in context.cars
                            select new
                            {
                                Car = car
                            };
                Functions.addColumnsToDataGridView(dataGridView1, ColumnNames);
                dataGridView1.Rows.Clear();
                foreach (var result in query)
                {
                    Functions.fillDataGridView(dataGridView1, result.Car.Id, result.Car.Number_plate, result.Car.Make, result.Car.Model, result.Car.Carry, result.Car.IsUsed, result.Car.Sold, result.Car.Comment);
                }


                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count -6;
                }
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 4;
                Functions.TranslateTrueFalse(dataGridView1);
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
            int Id = int.Parse(id.Text);
            //int Id = -1 + int.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            ShowCar(Id);

        }
        private void ShowCar(int Id)
        {
            var query = (from car in context.cars
                        where car.Id == Id
                        select new
                        {
                            Car = car
                        }).First();
            make.Text = query.Car.Make;
            model.Text = query.Car.Model;
            comment.Text = query.Car.Comment;
            plate.Text = query.Car.Number_plate;
            carry.Value = query.Car.Carry;
            sold.Checked = Boolean.Parse( query.Car.Sold);
            if (Boolean.Parse(query.Car.IsUsed))
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
            ////int Id = -1 + int.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            //int Id = Functions.FindCar(int.Parse(id.Text), DataBase.CarsList);
            //if (id.Text.Length == 0)
            //    return;
            //if (!Functions.AllowedPlate(plate.Text, DataBase.CarsList) && plate.Text != DataBase.CarsList[Id].Plate)
            //{
            //    MessageBox.Show("Taki numer rejestracji już występuje");
            //    return;
            //}
            ////int Id = -1 + int.Parse(id.Text);
            
            //if (Id >= 0)
            //{
            //    DataBase.CarsList[Id].Make = make.Text;
            //    DataBase.CarsList[Id].Model = model.Text;
            //    DataBase.CarsList[Id].Comment = comment.Text;
            //    DataBase.CarsList[Id].Plate = plate.Text;
            //    DataBase.CarsList[Id].Carry = (uint)carry.Value;
            //    DataBase.CarsList[Id].Sold = sold.Checked;
            //    //DataBase.CarsList[Id].IsUsed = isUsed.Checked;
            //    DataBase.UpdateCars(Id, int.Parse(id.Text));
            //}
            //LoadData(DataBase);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (Functions.AllowedPlate(plate.Text, DataBase.CarsList))
            //{
            //    DataBase.AddCar(plate.Text, make.Text, model.Text, (uint)carry.Value, false, false, comment.Text);
            //    //DataBase.CarsList.Add(new Cars());
            //    LoadData(DataBase);
            //}
            //else
            //{
            //    MessageBox.Show("Istnieje już auto o takiej rejestracji");
            //}
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
