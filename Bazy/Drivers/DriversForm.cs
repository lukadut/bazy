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
    public partial class DriversForm : Form
    {
        static DataGridView dataGridView = null;
        string[] ColumnNames = { "Id", "Imię", "Nazwisko", "Stawka", "Licencja ADR", "Zatrudniony", "W trasie", "Komentarz" };
        projektEntities context;
        public DriversForm( int? id = null)
        {
            InitializeComponent();
            context = new projektEntities();
            name.Leave += Functions.textBox_Leave;
            surname.Leave += Functions.textBox_Leave;
            Functions.addColumnsToDataGridView(dataGridView1, ColumnNames);
            dataGridView = dataGridView1;
            LoadData();
            if (id.HasValue && id.Value >= 0)
            {
                try
                {
                    Functions.FindDriver((int)id);
                    this.id.Text = id + "";
                    ShowDriver(id.Value);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Nie znaleziono takiego rekordu");
                }

            }
        }

        void LoadData()
        {
            Queries.getDrivers();

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count - 8;
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 4;

            Functions.TranslateTrueFalse(dataGridView1,4,5,6);
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
            if (dataGridView.CurrentRow.Index >= dataGridView.RowCount - 1)
            {
                return;
            }
            id.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            int Id = int.Parse(id.Text);
            ShowDriver(Id);

        }
        private void ShowDriver(int Id)
        {
            drivers Driver = Functions.FindDriver(Id);
            name.Text = Driver.Name;
            surname.Text = Driver.Surname;
            comment.Text = Driver.Comment;
            wage.Value = Driver.Wage;
            employed.Checked = Boolean.Parse(Driver.Employed);
            adr.Checked = Boolean.Parse(Driver.ADR_License);
            if (Boolean.Parse(Driver.Busy))
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
            int currentRow = dataGridView1.CurrentRow.Index;
            int currentCol = dataGridView1.CurrentCell.ColumnIndex;
            if (id.Text == "")
                return;
            int Id = int.Parse(id.Text);

            projektEntities context = new projektEntities();
            var query = (from driver in context.drivers
                         where driver.Id == Id
                         select new
                         {
                             Driver = driver
                         }).First();
            if (query.Driver == null)
            {
                MessageBox.Show("Nie znaleziono kierowcy o takim Id");
                return;
            }


            query.Driver.Name = name.Text;
            query.Driver.Surname = surname.Text;
            query.Driver.Comment = comment.Text;
            query.Driver.Wage = (int)wage.Value;
            query.Driver.Employed = employed.Checked.ToString();
            query.Driver.ADR_License = adr.Checked.ToString();

            context.Entry(query.Driver).State = EntityState.Modified;
            context.SaveChanges();

            LoadData();
            dataGridView.CurrentCell = dataGridView[currentCol, currentRow];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            drivers Driver = new drivers
            {
                Name = name.Text,
                Surname = surname.Text,
                Wage = (int)wage.Value,
                ADR_License = adr.Checked.ToString(),
                Employed = true.ToString(),
                Busy = false.ToString(),
                Comment = comment.Text
            };
            projektEntities context = new projektEntities();
            context.drivers.Add(Driver);
            context.SaveChanges();

            LoadData();
            dataGridView.CurrentCell = dataGridView[0, dataGridView.RowCount - 2];
            id.Text = Driver.Id.ToString();
        }

        private void DriversForm_Resize(object sender, EventArgs e)
        {

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count - 8;
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
