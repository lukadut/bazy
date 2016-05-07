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
        static DataGridView dataGridView = null;
        string[] ColumnNames = { "Id", "Numer rejestracji", "Marka", "Model", "Ładowność", "Jest używany", "Sprzedany", "Komentarz" };
        projektEntities context;
        public CarsForm(int? id = null)
        {
            InitializeComponent();
            context = new projektEntities();
            make.Leave += Functions.textBox_Leave;
            model.Leave += Functions.textBox_Leave;
            Functions.addColumnsToDataGridView(dataGridView1, ColumnNames);
            dataGridView = dataGridView1;
            LoadData();
            if (id.HasValue && id.Value>=0)
            {
                try 
                { 
                    Functions.FindCar((int)id);
                    this.id.Text = id + "";
                    ShowCar(id.Value);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Nie znaleziono takiego rekordu");
                }

            }
        }
        void LoadData()
        {
            Queries.getCars();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count - 6;
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 4;
            Functions.TranslateTrueFalse(dataGridView1,5,6);

        }
        void ClearForm()
        {
            make.Text = "";
            model.Text = "";
            id.Text = "";
            comment.Text = "";
            plate.Text = "";
            carry.Value = 0;
            sold.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView.CurrentRow.Index >= dataGridView.RowCount - 1)
            {
                return;
            }
            id.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            int Id = int.Parse(id.Text);
            //int Id = -1 + int.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
            ShowCar(Id);

        }
        private void ShowCar(int Id)
        {
            cars Car = Functions.FindCar(Id);

            make.Text = Car.Make;
            model.Text = Car.Model;
            comment.Text = Car.Comment;
            plate.Text = Car.Number_plate;
            carry.Value = Car.Carry;
            sold.Checked = Boolean.Parse(Car.Sold);
            if (Boolean.Parse(Car.IsUsed))
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
            int currentRow = dataGridView1.CurrentRow.Index;
            int currentCol = dataGridView1.CurrentCell.ColumnIndex;
            if (id.Text == "")
                return;
            int Id = int.Parse(id.Text);
            
            projektEntities context = new projektEntities();
            var query = (from car in context.cars
                         where car.Id == Id
                         select new
                         {
                             Car = car
                         }).First();
            if (query.Car == null)
            {
                MessageBox.Show("Nie znaleziono auta o takim Id");
                return;
            }
            if (!Functions.AllowedPlate(plate.Text) && plate.Text != query.Car.Number_plate)
            {
                MessageBox.Show("Taki numer rejestracji już występuje");
                return;
            }

            query.Car.Make = make.Text;
            query.Car.Model = model.Text;
            query.Car.Comment = comment.Text;
            query.Car.Number_plate = plate.Text;
            query.Car.Carry = (int)carry.Value;
            query.Car.Sold = sold.Checked.ToString();

            context.Entry(query.Car).State = EntityState.Modified;
            context.SaveChanges();

            LoadData();
            dataGridView.CurrentCell = dataGridView[currentCol, currentRow];

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Functions.AllowedPlate(plate.Text))
            {
                cars Car = new cars
                {
                    Make = make.Text,
                    Model = model.Text,
                    Comment = comment.Text,
                    Number_plate = plate.Text,
                    Carry = (int)carry.Value,
                    IsUsed=false.ToString(),
                    Sold = false.ToString()
                };
                projektEntities context = new projektEntities();
                context.cars.Add(Car);
                context.SaveChanges();

                LoadData();
                dataGridView.CurrentCell = dataGridView[0, dataGridView.RowCount-2];
                id.Text = Car.Id.ToString();
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
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 4;
        }

        public static void fillDataGridView(params Object[] values)
        {
            if (dataGridView != null)
                dataGridView.Rows.Add(values);

        }
        public static void clearDataGridView()
        {
            if (dataGridView != null)
                dataGridView.Rows.Clear();
        }

    }
}
