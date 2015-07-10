using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy_danych
{
    class Functions
    {
        public static string[] classes = { "1", "2", "3", "4.1", "4.2", "4.3", "5.1", "5.2", "6.1", "6.2", "7", "8", "9" };
        public static int FindDriver(int IdInBase, List<Drivers> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == IdInBase)
                    return i;
            }
            return -1;
        }

        public static int FindCar(int IdInBase, List<Cars> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == IdInBase)
                    return i;
            }
            return -1;
        }

        public static int FindFreightsList(int IdInBase, List<FreightsList> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == IdInBase)
                    return i;
            }
            return -1;
        }
        public static int FindCitiesList(int IdInBase, List<CitiesList> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == IdInBase)
                    return i;
            }
            return -1;
        }

        public static int FindCompanyNamesList(int IdInBase, List<CompanyNamesList> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == IdInBase)
                    return i;
            }
            return -1;
        }

        public static int FindCompanies(int IdInBase, List<Companies> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == IdInBase)
                    return i;
            }
            return -1;
        }

        public static bool AllowedPlate(string newPlate, List<Cars>list)
        {
            //newPlate.Skip(5);
            newPlate = newPlate.ToLower();
            newPlate = System.Text.RegularExpressions.Regex.Replace(newPlate,@"\s","");
            for (int i = 0; i < list.Count; i++)
            {
                
                //if (list[i].Plate == newPlate)
                if(String.Equals(newPlate, System.Text.RegularExpressions.Regex.Replace(list[i].Plate,@"\s","").ToLower()))
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
        public static void TranslateTrueFalse(System.Windows.Forms.DataGridView dataGridView1)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int r = 0; r < dataGridView1.RowCount; r++)
                {
                    if ((dataGridView1.Rows[r].Cells[i].Value + "") == "True")
                        dataGridView1.Rows[r].Cells[i].Value = "Tak";
                    else if ((dataGridView1.Rows[r].Cells[i].Value + "") == "False")
                        dataGridView1.Rows[r].Cells[i].Value = "Nie";
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
    }
}
