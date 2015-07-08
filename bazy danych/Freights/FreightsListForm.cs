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
    public partial class FreightsListForm : Form
    {
        string[] ColumnNames = { "Id", "Nazwa towaru", "Typ", "Wymagany ADR", "Klasa ADR", "Komentarz" };
        public static string[] TypesPL = { "Kontener", "Wywrotka", "Platforma", "Laweta", "Chłodnia", "Cysterna" };
        public static string[] Types = { "Container", "Dump", "Flatbed", "Lowboy", "Refrigerated", "Tank" };
        string[] ADRClass = { "Materiały wybuchowe", "Gazy", "Materiały ciekłe zapalne", "Materiały stałe zapalne", "Materiały samozapalne", 
                                "Materiały wytwarzające \nw zetknięciu z wodą gazy palne", "Materiały utleniające", "Materiały organiczne", 
                                "Materiały trujące", "Materiały zakaźne", "Materiały \npromieniotwórcze", "Materiały żrące", 
                                "Różne materiały \ni przedmioty \nniebezpieczne" };
        Base DataBase;
        List<CheckBox> CheckBoxList;
        public FreightsListForm(Base dataBase, int? id = null)
        {
            CheckBoxList = new List<CheckBox>();
            DataBase = dataBase;
            InitializeComponent();
            DrawADRCheckBoxes();

            LoadData(DataBase);

            //Console.WriteLine("laweta ma numer " + Functions.FindStringIndex(TypesPL,"a"));
            type.Items.Clear();
            type.Items.AddRange(TypesPL);
            type.Text = "Kontener";
                if (id >= 0 && Functions.FindFreightsList((int)id, DataBase.FreightsListList) >= 0)
                {
                    this.id.Text = id + "";
                    ShowFreightsList(Functions.FindFreightsList((int)id, DataBase.FreightsListList));
                }
        }

        void DrawADRCheckBoxes()
        {
            for (int i = 0; i < 13; i++)
            {
                CheckBox cb = new CheckBox();
                cb.AutoSize = true;
                cb.Location = new System.Drawing.Point(260 + ((i + 1) / 8) * 60, 13 + 20 * ((i % 7)));
                cb.Enabled = false;
                cb.Size = new System.Drawing.Size(58, 17);
                cb.TabIndex = 19;
                cb.Text = Functions.classes[i];
                cb.Tag = i;
                cb.MouseEnter += ((object sender, EventArgs e) => cb.Text = Functions.classes[(int)cb.Tag] + " " + ADRClass[(int)cb.Tag]);
                cb.MouseLeave += ((object sender, EventArgs e) => cb.Text = Functions.classes[(int)cb.Tag]);
                cb.UseVisualStyleBackColor = true;
                CheckBoxList.Add(cb);
                this.Controls.Add(cb);
            }
        }
        void LoadData(Base DataBase)
        {
            try
            {
                DataSet DS = new DataSet();
                BindingSource BS = new BindingSource();
                MySqlDataAdapter MSDA = new MySqlDataAdapter("select * from freights_list", DataBase.MySqlConnector);
                MSDA.Fill(DS);
                for (int i = 0; i < DS.Tables[0].Columns.Count; i++)
                    DS.Tables[0].Columns[i].ColumnName = ColumnNames[i];
                BS.DataSource = DS.Tables[0];

                dataGridView1.DataSource = BS;

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count - 8;
                }
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width+= 4;

                Functions.TranslateTrueFalse(dataGridView1);


                        
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int r = 0; r < dataGridView1.RowCount; r++)
                {
                    if (Types.Contains(dataGridView1.Rows[r].Cells[i].Value + ""))
                        dataGridView1.Rows[r].Cells[i].Value = TypesPL[(int)(FreightsList.Types)Enum.Parse(typeof(FreightsList.Types),dataGridView1.Rows[r].Cells[i].Value + "")];
                }
            }
       
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd w połączeniu z bazą.");
                //this.Close();
            }
        }
        void ClearForm()
        {
            name.Text = "";
            type.Text = "Kontener";
            adr.Checked = false;
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
            type.Text = DataBase.FreightsListList[Id].Type;
            comment.Text = DataBase.FreightsListList[Id].Comment;
            if (Functions.FindStringIndex(Types, DataBase.FreightsListList[Id].Type) < 0)
                type.Text = DataBase.FreightsListList[Id].Type;
            else
                type.Text = TypesPL[Functions.FindStringIndex(Types, DataBase.FreightsListList[Id].Type)];
            adr.Checked = DataBase.FreightsListList[Id].Adr;
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
                DataBase.FreightsListList[Id].Adr = adr.Checked;
                DataBase.FreightsListList[Id].Type = type.Text;
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

            string adrClass = "";
            foreach (var item in CheckBoxList)
            {
                if (item.Checked)
                {
                    adrClass += Functions.classes[(int)item.Tag] + ",";
                }
            }
            if (adrClass.Length > 0)
                adrClass.Remove(adrClass.Length - 1, 1);
            else
                adr.Checked = false;
            DataBase.AddFreightsList(name.Text, Types[Functions.FindStringIndex(TypesPL, type.Text)], adrClass, adr.Checked, comment.Text);
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
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count - 8;
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 4;
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
