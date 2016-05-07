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

    public partial class FreightsForm : Form
    {
        static DataGridView dataGridView = null;
        string[] ColumnNames = { "Id", "Towar", "Skąd", "Dokąd","Planowana dostawa","Liczba sztuk","Waga", "Komentarz" };
        projektEntities context;
        public FreightsForm(int? id = null)
        {
            InitializeComponent();
            date.Value = DateTime.Now;
            context = new projektEntities();
            Functions.addColumnsToDataGridView(dataGridView1, ColumnNames);
            dataGridView = dataGridView1;
            LoadData();


            cargo.Items.Clear();
            var query1 = from c in context.cargo
                         select new
                         {
                             Cargo = c
                         };
            foreach (var result in query1)
            {
                cargo.Items.Add(new Transport
                {
                    Id = result.Cargo.Id,
                    Name = result.Cargo.Name + " - " + //CargoForm.TypesPL[
                    CargoForm.TypesPL[(int)Enum.Parse(typeof(Functions.Types), result.Cargo.Type)]
                    
                });
            }
            destination.Items.Clear();
            start.Items.Clear();
            var query2 = from c in context.companies
                         join ci in context.cities_list on c.CityId equals ci.Id
                         join co in context.company_name_list on c.CompanyId equals co.Id
                         select new
                         {
                             Company = c.Id,
                             Address = c.Address,
                             City = ci.City,
                             Name = co.Company
                         };
            foreach (var result in query2)
            {
                Transport t = new Transport
                {
                    Id = result.Company,
                    Name = result.Name + ", " + result.City + ", " + result.Address
                };
                destination.Items.Add(t);
                start.Items.Add(t);
            }
            if (id.HasValue && id.Value >= 0)
            {
                try
                {
                    Functions.FindFreights((int)id);
                    this.id.Text = id + "";
                    ShowFreight(id.Value);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Nie znaleziono takiego rekordu");
                }

            }
        }

        void LoadData()
        {
            Queries.getFreights();

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count - 8;
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 4;

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        void ClearForm()
        {
            cargo.SelectedIndex = -1;
            start.SelectedIndex = -1;
            destination.SelectedIndex = -1;
            date.Value = DateTime.Now;
            id.Text = "";
            comment.Text = "";
            amount.Value = 1;
            weight.Value = weight.Minimum;
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
            ShowFreight(Id);
            freights Freight = Functions.FindFreights(Id);

            if(dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                CargoForm CF = new CargoForm(Freight.CargoId);
                CF.Show();
                return;
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                CompaniesForm CF = new CompaniesForm(Freight.From);
                CF.Show();
                return;
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                CompaniesForm CF = new CompaniesForm(Freight.To);
                CF.Show();
                return;
            }
            


        }
        private void ShowFreight(int Id)
        {
            freights Freight = Functions.FindFreights(Id);
            start.SelectedIndex = Functions.findIndexItemWithIdInCollection(Freight.From, start.Items);
            destination.SelectedIndex = Functions.findIndexItemWithIdInCollection(Freight.To, destination.Items);
            cargo.SelectedIndex = Functions.findIndexItemWithIdInCollection(Freight.CargoId, cargo.Items);
            comment.Text = Freight.Comment;
            amount.Value = Freight.Amount;
            weight.Value = Freight.Weight;
            date.Value = Freight.ScheduledArrive;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int currentRow = dataGridView1.CurrentRow.Index;
            int currentCol = dataGridView1.CurrentCell.ColumnIndex;
            if (id.Text == "")
                return;
            int Id = int.Parse(id.Text);
            if (cargo.SelectedIndex < 0 || start.SelectedIndex < 0 || destination.SelectedIndex < 0)
            {
                MessageBox.Show("Towar, miejsce startowe i miejsce docelowe nie mogą być puste");
                return;
            }
            projektEntities context = new projektEntities();

            var query = (from f in context.freights
                         where f.Id == Id
                         select new
                         {
                             Freight = f
                         }).First();
            query.Freight.Amount = (byte)amount.Value;
            query.Freight.CargoId = ((Transport)cargo.SelectedItem).Id;
            query.Freight.Comment = comment.Text;
            query.Freight.From = ((Transport)start.SelectedItem).Id;
            query.Freight.ScheduledArrive = date.Value;
            query.Freight.To = ((Transport)destination.SelectedItem).Id;
            query.Freight.Weight = (int)weight.Value;

            context.Entry(query.Freight).State = EntityState.Modified;
            context.SaveChanges();

            LoadData();
            dataGridView.CurrentCell = dataGridView[currentCol, currentRow];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cargo.SelectedIndex < 0 || start.SelectedIndex < 0 || destination.SelectedIndex < 0)
            {
                MessageBox.Show("Towar, miejsce startowe i miejsce docelowe nie mogą być puste");
                return;
            }
            freights Freight = new freights
            {
                Amount = (byte)amount.Value,
                CargoId = ((Transport)cargo.SelectedItem).Id,
                Comment = comment.Text,
                From = ((Transport)start.SelectedItem).Id,
                ScheduledArrive = date.Value,
                To = ((Transport)destination.SelectedItem).Id,
                Weight = (int)weight.Value
            };

            projektEntities context = new projektEntities();
            context.freights.Add(Freight);
            context.SaveChanges();

            LoadData();
            dataGridView.CurrentCell = dataGridView[0, dataGridView.RowCount - 2];
            id.Text = Freight.Id.ToString();
            

        }


        private void CarsForm_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count - 9;
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 2;
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
