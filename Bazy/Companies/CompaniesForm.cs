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

    public partial class CompaniesForm : Form
    {
        static DataGridView dataGridView = null;
        string[] ColumnNames = { "Id", "Firma", "Miasto", "Adres", "Komentarz" };
        projektEntities context;
        public CompaniesForm( int? id = null)
        {
            InitializeComponent();
            context = new projektEntities();
            Functions.addColumnsToDataGridView(dataGridView1, ColumnNames);
            dataGridView = dataGridView1;
            LoadData();
            if (id.HasValue && id.Value >= 0)
            {
                try
                {
                    Functions.FindCompanies((int)id);
                    this.id.Text = id + "";
                    ShowCompanies(id.Value);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Nie znaleziono takiego rekordu");
                }

            }
            
            cities.Items.Clear();
            var query1 = (from city in context.cities_list
                        select new
                        {
                            city.City, city.Id
                        });
            foreach (var item in query1)
            {
                cities.Items.Add(new Transport()
                {
                    Name = item.City,
                    Id = item.Id
                });
            }

            companies.Items.Clear();
            var query2 = from company in context.company_name_list
                        select new
                        {
                            company
                        };
            foreach (var item in query2)
            {
                companies.Items.Add(new Transport()
                {
                    Name = item.company.Company,
                    Id = item.company.Id
                });
            }
        }

        void LoadData()
        {
            Queries.getCompanies();

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count - 8;
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 4;
        }
        
        void ClearForm()
        {
            address.Text = "";
            cities.Text = "";
            companies.Text = "";
            id.Text = "";
            comment.Text = "";
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
            ShowCompanies(Id);


        }
        private void ShowCompanies(int Id)
        {
            projektEntities ctx = new projektEntities();
            var query = (from c in ctx.companies
                        join ci in ctx.cities_list on c.CityId equals ci.Id
                        join co in ctx.company_name_list on c.CompanyId equals co.Id
                        where c.Id == Id
                        select new
                        {
                            Company = c,
                            CompanyName = co,
                            CityName = ci
                        }).First();
            comment.Text = query.Company.Comment;
            address.Text = query.Company.Address;
            cities.Text = query.CityName.City;
            companies.Text = query.CompanyName.Company;

            int a = cities.SelectedIndex;
            System.Console.WriteLine(a);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int currentRow = dataGridView1.CurrentRow.Index;
            int currentCol = dataGridView1.CurrentCell.ColumnIndex;
            if (id.Text == "")
                return;
            int Id = int.Parse(id.Text);

            projektEntities context = new projektEntities();

            var query = (from company in context.companies
                         where company.Id == Id
                         select new
                         {
                             company
                         }).First();
            try
            {
                getOrAddCity(cities.SelectedIndex);
                getOrAddCompany(companies.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            query.company.Address = address.Text;
            query.company.Comment = comment.Text;
            query.company.CityId = ((Transport)cities.SelectedItem).Id;
            query.company.CompanyId = ((Transport)companies.SelectedItem).Id;

            context.Entry(query.company).State = EntityState.Modified;
            context.SaveChanges();

            LoadData();
            dataGridView.CurrentCell = dataGridView[currentCol, currentRow];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                getOrAddCity(cities.SelectedIndex);
                getOrAddCompany(companies.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            companies Company = new companies
            {
                Address = address.Text,
                Comment = comment.Text,
                CityId = ((Transport)cities.SelectedItem).Id,
                CompanyId = ((Transport)companies.SelectedItem).Id
            };
            projektEntities context = new projektEntities();
            try
            {
                context.companies.Add(Company);
                context.SaveChanges();

                LoadData();
                int currentRow = dataGridView.RowCount - 2;
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    System.Console.WriteLine(dataGridView[0, i].Value.ToString());
                    if (int.Parse(dataGridView[0, i].Value.ToString()) == Company.Id)
                    {
                        currentRow = i;
                        break;
                    }
                }
                dataGridView.CurrentCell = dataGridView[0, currentRow];
                id.Text = Company.Id.ToString();
            } catch(Exception){
                MessageBox.Show("Taka firma już istnieje");
            }



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
        private void getOrAddCity(int index)
        {
            if (index < 0)
            {
                String name = Functions.TextFormat(cities.Text);
                if (name.Length == 0)
                {
                    throw new Exception("Miasto nie może mieć pustej nazwy");
                }
                cities_list newCity = new cities_list()
                {
                    City = name
                };
                context.cities_list.Add(newCity);
                context.SaveChanges();
                cities.Items.Add(new Transport()
                {
                    Name = newCity.City,
                    Id = newCity.Id
                });
                cities.SelectedIndex = cities.Items.Count - 1;
            }
        }
        private void getOrAddCompany(int index)
        {
            if (index < 0)
            {
                String name = Functions.TextFormat(companies.Text);
                if (name.Length == 0)
                {
                    throw new Exception("Miasto nie może mieć pustej nazwy");
                }
                companyNameList newCompany = new companyNameList()
                {
                    Company = name
                };
                context.company_name_list.Add(newCompany);
                context.SaveChanges();
                companies.Items.Add(new Transport()
                {
                    Name = newCompany.Company,
                    Id = newCompany.Id
                });
                companies.SelectedIndex = companies.Items.Count - 1;
            }
        }


    }
}
