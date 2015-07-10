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
    public partial class CompaniesForm : Form
    {
        string[] ColumnNames = { "Id", "Miasto", "Firma", "Adres", "Komentarz" };

        Base DataBase;
        List<CheckBox> CheckBoxList;
        public CompaniesForm(Base dataBase, int? id = null)
        {
            CheckBoxList = new List<CheckBox>();
            DataBase = dataBase;
            InitializeComponent();

            LoadData(DataBase);

            //Console.WriteLine("laweta ma numer " + Functions.FindStringIndex(TypesPL,"a"));
            cities.Items.Clear();
            foreach (var item in dataBase.CitiesListList)
            {
                cities.Items.Add(item);
            }

            companies.Items.Clear();
            foreach (var item in dataBase.CompanyNamesListList)
            {
                companies.Items.Add(item);
            }
            //type.AutoCompleteSource = dataBase.CitiesListList;
                //(AutoCompleteSource)dataBase.CitiesListList;
            //type.Items.AddRange(dataBase.CitiesListList.ToString());
            
            if (id >= 0 && Functions.FindFreightsList((int)id, DataBase.FreightsListList) >= 0)
            {
                this.id.Text = id + "";
                ShowFreightsList(Functions.FindFreightsList((int)id, DataBase.FreightsListList));
            }
        }


        void LoadData(Base DataBase)
        {
            try
            {
                DataSet DS = new DataSet();
                BindingSource BS = new BindingSource();
                MySqlDataAdapter MSDA = new MySqlDataAdapter("select * from companies", DataBase.MySqlConnector);
                //DS.Tables[0].Columns[1].DataType = typeof(string);
                DS.Tables.Add("Table");
                DS.Tables[0].Columns.Add("Id");
                DS.Tables[0].Columns.Add("CityId",typeof(string));
                DS.Tables[0].Columns.Add("CompanyId",typeof(string));
                MSDA.Fill(DS);
                for (int i = 0; i < DS.Tables[0].Columns.Count; i++)
                    DS.Tables[0].Columns[i].ColumnName = ColumnNames[i];
                //DS.Tables[0].Columns[1].DataType = typeof(string);
                BS.DataSource = DS.Tables[0];
                
                dataGridView1.DataSource = BS;

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count - 8;
                }
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 4;

                ////Functions.TranslateTrueFalse(dataGridView1);

                ////dataGridView1.Columns[1].ValueType = typeof(string);
                System.Console.WriteLine(DS.Tables[0].Rows.Count);
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Console.WriteLine(dataGridView1.Rows[i].Cells[1].ValueType);
                    dataGridView1.Rows[i].Cells[1].Value = DataBase.CitiesListList[Functions.FindCitiesList(int.Parse(dataGridView1.Rows[i].Cells[1].Value + ""), DataBase.CitiesListList)].City + "";
                    dataGridView1.Rows[i].Cells[2].Value = DataBase.CompanyNamesListList[Functions.FindCompanyNamesList(int.Parse(dataGridView1.Rows[i].Cells[2].Value + ""), DataBase.CompanyNamesListList)].CompanyName + "";
                }


                //for (int i = 0; i < dataGridView1.Columns.Count; i++)
                //{
                //    for (int r = 0; r < dataGridView1.RowCount; r++)
                //    {
                //        if (Types.Contains(dataGridView1.Rows[r].Cells[i].Value + ""))
                //            dataGridView1.Rows[r].Cells[i].Value = TypesPL[(int)(FreightsList.Types)Enum.Parse(typeof(FreightsList.Types), dataGridView1.Rows[r].Cells[i].Value + "")];
                //    }
                //}


            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd w połączeniu z bazą.\n"+ex.Message);
                //this.Close();
            }
        }
        void ClearForm()
        {
            name.Text = "";
            cities.Text = "Kontener";
            comment.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            int Id = Functions.FindFreightsList(int.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString()), DataBase.FreightsListList);
            ShowFreightsList(Id);


        }
        private void ShowFreightsList(int Id)
        {
            name.Text = DataBase.FreightsListList[Id].Name;
            cities.Text = DataBase.FreightsListList[Id].Type;
            comment.Text = DataBase.FreightsListList[Id].Comment;
            //if (Functions.FindStringIndex(Types, DataBase.FreightsListList[Id].Type) < 0)
            //    type.Text = DataBase.FreightsListList[Id].Type;
            //else
            //    type.Text = TypesPL[Functions.FindStringIndex(Types, DataBase.FreightsListList[Id].Type)];
            //adr.Checked = DataBase.FreightsListList[Id].Adr;
            foreach (var item in CheckBoxList)
            {
                item.Checked = DataBase.FreightsListList[Id].AdrClass[(int)item.Tag];
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id.Text.Length == 0)
                return;
            int Id = Functions.FindFreightsList(int.Parse(id.Text), DataBase.FreightsListList);
            if (Id >= 0)
            {
                DataBase.FreightsListList[Id].Name = name.Text;
                DataBase.FreightsListList[Id].Type = cities.Text;
                DataBase.FreightsListList[Id].Comment = comment.Text;

                foreach (var item in CheckBoxList)
                {
                    DataBase.FreightsListList[Id].AdrClass[(int)item.Tag] = item.Checked;
                }
                DataBase.UpdateFreightsList(Id, int.Parse(id.Text));
            }
            LoadData(DataBase);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //FreightsList.Types a = FreightsList.Types.Container;
            //string b = "Flatbed";
            //a = (FreightsList.Types)2;
            //Console.WriteLine("Types a = " + a + (int)a);
            //Console.WriteLine(b + " == " + a + "  asasd ");
            //Console.WriteLine((string)a.ToString() == b);
            //Console.WriteLine("");
            //MessageBox.Show(dataGridView1.Rows[1].Cells[4].Value.ToString());


            //DataBase.AddFreightsList(name.Text, Types[Functions.FindStringIndex(TypesPL, type.Text)], adrClass, adr.Checked, comment.Text);
            LoadData(DataBase);

            //if (Functions.AllowedPlate(plate.Text, DataBase.CarsList))
            //{
            //    //DataBase.AddCar(plate.Text, make.Text, model.Text, (uint)carry.Value, false, false, comment.Text);
            //    //DataBase.CarsList.Add(new Cars());
            //    //LoadData(DataBase);
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
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count - 9;
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 2;
        }

        private void sold_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            foreach (var item in CheckBoxList)
            {
                item.Enabled = cb.Checked;
                if (!cb.Checked)
                    item.Checked = false;
            }
        }



    }
}
