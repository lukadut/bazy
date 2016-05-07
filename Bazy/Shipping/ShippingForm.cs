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
    public partial class ShippingForm : Form
    {
        static DataGridView dataGridView = null;
        string[] ColumnNames = { "Id", "Kierowca", "Pojazd", "Zlecenie","Czas wyruszenia","Czas dotarcia","Status", "Komentarz" };
        projektEntities context;
        public ShippingForm()
        {
            InitializeComponent();
            context = new projektEntities();
            Functions.addColumnsToDataGridView(dataGridView1, ColumnNames);
            dataGridView = dataGridView1;
            LoadData();

            updateResources();
        }

        void LoadData()
        {
            Queries.getShipping();

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count - 8;
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 4;

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            Functions.TranslateShippingState(dataGridView1, 6);
        }

        void ClearForm()
        {
            driver.SelectedIndex = -1;
            driver.DropDownStyle = ComboBoxStyle.DropDownList;
            driver.Enabled = true;

            car.SelectedIndex = -1;
            car.DropDownStyle = ComboBoxStyle.DropDownList;
            car.Enabled = true;

            freight.SelectedIndex = -1;
            freight.DropDownStyle = ComboBoxStyle.DropDownList;
            freight.Enabled = true;

            id.Text = "";
            comment.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateResources();
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
            ShowShipping(Id);
            shipping Shipping = Functions.FindShipping(Id);

            if (dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                DriversForm DF = new DriversForm(Shipping.DriverId);
                DF.Show();
                return;
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                CarsForm CF = new CarsForm(Shipping.CarId);
                CF.Show();
                return;
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                FreightsForm FF = new FreightsForm(Shipping.FreightId);
                FF.Show();
                return;
            }
            


        }
        private void ShowShipping(int Id)
        {
            shipping Shipping = Functions.FindShipping(Id);

            projektEntities ctx = new projektEntities();
            var result = (from fr in ctx.freights
                        join ca in ctx.cargo on fr.CargoId equals ca.Id
                        join st in ctx.companies on fr.From equals st.Id
                        join stci in ctx.cities_list on st.CityId equals stci.Id
                        join stco in ctx.company_name_list on st.CompanyId equals stco.Id
                        join de in ctx.companies on fr.To equals de.Id
                        join deci in ctx.cities_list on de.CityId equals deci.Id
                        join deco in ctx.company_name_list on de.CompanyId equals deco.Id
                        where fr.Id == Shipping.FreightId
                        select new
                        {
                            Freight = fr,
                            Cargo = ca,
                            Start = st,
                            StartCity = stci,
                            StartCompany = stco,
                            Destination = de,
                            DestinationCity = deci,
                            DestinationCompany = deco
                        }).First();
            drivers Driver = Functions.FindDriver(Shipping.DriverId);
            cars Car = Functions.FindCar(Shipping.CarId);

            driver.Text = Driver.Surname + " " + Driver.Name;
            if (Boolean.Parse(Driver.Busy) || !Boolean.Parse(Driver.Employed))
            {
                driver.SelectedIndex = -1;
                driver.DropDownStyle = ComboBoxStyle.DropDown;
                driver.Enabled = false;
                driver.Text = Driver.Surname + " " + Driver.Name;
            }
            else
            {
                driver.DropDownStyle = ComboBoxStyle.DropDownList;
                driver.Enabled = true;
            }

            car.Text = Car.Make + " " + Car.Model + ", " + Car.Number_plate;
            if (Boolean.Parse(Car.IsUsed) || Boolean.Parse(Car.Sold))
            {
                car.SelectedIndex = -1;
                car.DropDownStyle = ComboBoxStyle.DropDown;
                car.Enabled = false;
                car.Text = Car.Make + " " + Car.Model + ", " + Car.Number_plate;
            }
            else
            {
                car.DropDownStyle = ComboBoxStyle.DropDownList;
                car.Enabled = true;
            }


            freight.Text = result.Cargo.Name + ": " + result.StartCompany.Company + ", " + result.StartCity.City + '\u279C' + result.DestinationCompany.Company + ", " + result.DestinationCity.City;
            if (result.Freight.Amount<1)
            {
                freight.SelectedIndex = -1;
                freight.DropDownStyle = ComboBoxStyle.DropDown;
                freight.Enabled = false;
                freight.Text = result.Cargo.Name + ": " + result.StartCompany.Company + ", " + result.StartCity.City + '\u279C' + result.DestinationCompany.Company + ", " + result.DestinationCity.City;
            }
            else
            {
                freight.DropDownStyle = ComboBoxStyle.DropDownList;
                freight.Enabled = true;
            }
            context.Dispose();
            comment.Text = Shipping.Comment;
            context.SaveChanges();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int currentRow = dataGridView1.CurrentRow.Index;
            int currentCol = dataGridView1.CurrentCell.ColumnIndex;
            if (id.Text == "")
                return;
            int Id = int.Parse(id.Text);

            projektEntities context = new projektEntities();
            
            var query = (from f in context.shipping
                         where f.Id == Id
                         select new
                         {
                             Shipping = f
                         }).First();
            query.Shipping.Comment = comment.Text;

            context.Entry(query.Shipping).State = EntityState.Modified;
            context.SaveChanges();

            LoadData();
            dataGridView.CurrentCell = dataGridView[currentCol, currentRow];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (driver.SelectedIndex < 0 || car.SelectedIndex < 0 || freight.SelectedIndex < 0)
            {
                MessageBox.Show("Musi być określony kierowca, pojazd i zlecenie");
                return;
            }
            shipping Shipping = new shipping
            {
                CarId = ((Transport)car.SelectedItem).Id,
                DriverId = ((Transport)driver.SelectedItem).Id,
                FreightId = ((Transport)freight.SelectedItem).Id,
                //DepartTime = DateTime.Now,
                Delivered = "Not yet",
               // ArriveTime = null,
                Comment = comment.Text
            };
            freights Freight = Functions.FindFreights(Shipping.FreightId);
            cars Car = Functions.FindCar(Shipping.CarId);
            drivers Driver = Functions.FindDriver(Shipping.DriverId);
            cargo Cargo = Functions.FindCargo(Freight.CargoId);

            if (Freight.Weight > Car.Carry)
            {
                MessageBox.Show("Ten pojazd ma za małą ładowność");
                return;
            }
            if(Boolean.Parse(Cargo.ADR) && !Boolean.Parse(Driver.ADR_License))
            {
                MessageBox.Show("Kierowca nie może wieźć ładunku niebezpiecznego");
                return;
            }
            //Freight.Weight

            projektEntities context = new projektEntities();
            context.shipping.Add(Shipping);
            context.SaveChanges();

            LoadData();
            dataGridView.CurrentCell = dataGridView[0, dataGridView.RowCount - 2];
            id.Text = Shipping.Id.ToString();
            updateResources();
            

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

        private void updateResources()
        {
            driver.Items.Clear();
            car.Items.Clear();
            freight.Items.Clear();

            projektEntities ctx = new projektEntities();
            var queryDrivers = from d in ctx.drivers
                         where ((d.Employed == "True") && (d.Busy == "False"))

                         select new{
                             Driver = d
                         };
            var queryCars = from c in ctx.cars
                            where ((c.Sold == "False") && (c.IsUsed == "False"))
                            select new
                            {
                                Car = c
                            };

            var queryFreights = from fr in ctx.freights
                                join ca in ctx.cargo on fr.CargoId equals ca.Id
                                join st in ctx.companies on fr.From equals st.Id
                                join stci in ctx.cities_list on st.CityId equals stci.Id
                                join stco in ctx.company_name_list on st.CompanyId equals stco.Id
                                join de in ctx.companies on fr.To equals de.Id
                                join deci in ctx.cities_list on de.CityId equals deci.Id
                                join deco in ctx.company_name_list on de.CompanyId equals deco.Id
                                where fr.Amount > 0
                                select new
                                {
                                    Freight = fr,
                                    Cargo = ca,
                                    Start = st,
                                    StartCity = stci,
                                    StartCompany = stco,
                                    Destination = de,
                                    DestinationCity = deci,
                                    DestinationCompany = deco
                                };
            foreach (var result in queryCars)
            {
                car.Items.Add(new Transport
                {
                    Id = result.Car.Id,
                    Name = result.Car.Make + " " + result.Car.Model + ", " + result.Car.Number_plate
                });
            }
            foreach (var result in queryDrivers)
            {
                driver.Items.Add(new Transport
                {
                    Id = result.Driver.Id,
                    Name = result.Driver.Surname + " " + result.Driver.Name
                });
            }
            foreach (var result in queryFreights)
            {
                freight.Items.Add(new Transport
                {
                    Id = result.Freight.Id,
                    Name = result.Cargo.Name + ": " + result.StartCompany.Company + ", " + result.StartCity.City + '\u279C' + result.DestinationCompany.Company + ", " + result.DestinationCity.City
                });
            }

                        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int currentRow = dataGridView1.CurrentRow.Index;
            int currentCol = dataGridView1.CurrentCell.ColumnIndex;
            if (id.Text == "")
                return;
            int Id = int.Parse(id.Text);

            projektEntities context = new projektEntities();

            var query = (from f in context.shipping
                         where f.Id == Id
                         select new
                         {
                             Shipping = f
                         }).First();
            if (query.Shipping.ArriveTime.HasValue)
            {
                MessageBox.Show("To zlecenie zostało już ukończone");
                return;
            }
            query.Shipping.Comment = comment.Text;
            query.Shipping.ArriveTime = DateTime.Now;

            context.Entry(query.Shipping).State = EntityState.Modified;
            context.SaveChanges();
            updateResources();
            ClearForm();
            LoadData();
            dataGridView.CurrentCell = dataGridView[currentCol, currentRow];
        }



    }
}
