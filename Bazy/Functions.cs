using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazy
{
    class Functions
    {
        public static string[] classes = { "1", "2", "3", "4.1", "4.2", "4.3", "5.1", "5.2", "6.1", "6.2", "7", "8", "9" };
        public enum Types { Container, Dump, Flatbed, Lowboy, Refrigerated, Tank };
        public static drivers FindDriver(int IdInBase)
        {
            projektEntities context = new projektEntities();
            var query = (from driver in context.drivers
                         where driver.Id == IdInBase
                         select new
                         {
                             Driver = driver
                         }).First();
            return query.Driver;
        }

        public static cars FindCar(int IdInBase)
        {
            projektEntities context = new projektEntities();
            var query = (from car in context.cars
                        where car.Id == IdInBase
                        select new
                        {
                            Car = car
                        }).First();
            return query.Car;

        }

        public static cargo FindCargo(int IdInBase)
        {
            projektEntities context = new projektEntities();
            var query = (from c in context.cargo
                         where c.Id == IdInBase
                         select new
                         {
                             Cargo = c
                         }).First();
            return query.Cargo;

        }

        public static companies FindCompanies(int IdInBase)
        {
            projektEntities context = new projektEntities();
            var query = (from c in context.companies
                         join ci in context.cities_list on c.CityId equals ci.Id
                         join co in context.company_name_list on c.CompanyId equals co.Id
                         where c.Id == IdInBase
                         select new
                         {
                             Companies = c
                         }).First();
            return query.Companies;
        }

        public static freights FindFreights(int IdInBase)
        {
            projektEntities context = new projektEntities();
            var query = (from c in context.freights
                         where c.Id == IdInBase
                         select new
                         {
                             Freight = c
                         }).First();
            return query.Freight;
        }

        public static shipping FindShipping(int IdInBase)
        {
            projektEntities context = new projektEntities();
            var query = (from c in context.shipping
                         where c.Id == IdInBase
                         select new
                         {
                             Shipping = c
                         }).First();
            return query.Shipping;
        }

        public static bool AllowedPlate(string newPlate)
        {
            projektEntities context = new projektEntities();
            

            //newPlate.Skip(5);
            newPlate = newPlate.ToLower();
            newPlate = System.Text.RegularExpressions.Regex.Replace(newPlate, @"\s", "");
            var query = from car in context.cars
                        select new
                        {
                            Plate = car.Number_plate
                        };
            foreach (var result in query)
            {
                if (String.Equals(newPlate, System.Text.RegularExpressions.Regex.Replace(result.Plate, @"\s", "").ToLower()))
                    return false;
            }
            return true;
        }

        public static bool[] ExpADR(string adr)
        {


            bool[] result = new bool[13];
            string[] explode = adr.Split(',');
            int i = 0;
            foreach (var item in classes)
            {
                if (explode.Contains(item))
                {
                    result[i] = true;
                }
                else
                {
                    result[i] = false;
                }
                i++;
            }
            return result;
        }
        public static void TranslateTrueFalse(System.Windows.Forms.DataGridView dataGridView1, params int[] columns)
        {
            foreach (int column in columns)
            {
                for (int r = 0; r < dataGridView1.RowCount; r++)
                {
                    if ((dataGridView1.Rows[r].Cells[column].Value + "") == "True")
                        dataGridView1.Rows[r].Cells[column].Value = "Tak";
                    else if ((dataGridView1.Rows[r].Cells[column].Value + "") == "False")
                        dataGridView1.Rows[r].Cells[column].Value = "Nie";
                }
            }
        }
        public static void  TranslateShippingState(System.Windows.Forms.DataGridView dataGridView1, params int[] columns)
        {
            foreach (int column in columns)
            {
                for (int r = 0; r < dataGridView1.RowCount; r++)
                {
                    String state = dataGridView1.Rows[r].Cells[column].Value + "";
                    if (state.Equals("On time", StringComparison.CurrentCultureIgnoreCase))
                        dataGridView1.Rows[r].Cells[column].Value = "Dostarczono o czasie";
                    if (state.Equals("Delayed", StringComparison.CurrentCultureIgnoreCase))
                        dataGridView1.Rows[r].Cells[column].Value = "Dostarczono po czasie";
                    if (state.Equals("Not yet", StringComparison.CurrentCultureIgnoreCase))
                        dataGridView1.Rows[r].Cells[column].Value = "W drodze";
                }
            }
        }
        public static int FindStringIndex(string[] strings, string f)
        {
            return Array.FindIndex(strings, s => s.Equals(f));
        }
        public static string TextFormat(string s)
        {
            string[] array = s.Split(' ');
            string result = "";
            foreach (var item in array)
            {
                item.Trim();
                if (item.Length == 0)
                    continue;
                result += item.Substring(0, 1).ToUpper();
                //result+= item.Substring(-1).ToUpper();
                result += item.Substring(1).ToLower();
                result += " ";
            }
            result = result.Trim();
            return result;
        }

        public static void textBox_Leave(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
            tb.Text = Functions.TextFormat(tb.Text);
        }

        public static void fillDataGridView(System.Windows.Forms.DataGridView dgv, params Object[] values)
        {
            dgv.Rows.Add(values);
        }
        public static void addColumnsToDataGridView(System.Windows.Forms.DataGridView dgv, params String[] values)
        {
            foreach (var item in values)
            {
                dgv.Columns.Add(item, item);
            }
        }
        public static int findIndexItemWithIdInCollection(int id, System.Windows.Forms.ComboBox.ObjectCollection collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                if (((Transport)collection[i]).Id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
