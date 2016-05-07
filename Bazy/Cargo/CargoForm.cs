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
    public partial class CargoForm : Form
    {
        static DataGridView dataGridView = null;
        string[] ColumnNames = { "Id", "Nazwa towaru", "Typ", "Wymagany ADR", "Klasa ADR", "Komentarz" };
        public static string[] TypesPL = { "Kontener", "Wywrotka", "Platforma", "Laweta", "Chłodnia", "Cysterna" };
        public static string[] Types = { "Container", "Dump", "Flatbed", "Lowboy", "Refrigerated", "Tank" };
        string[] ADRClass = { "Materiały wybuchowe", "Gazy", "Materiały ciekłe zapalne", "Materiały stałe zapalne", "Materiały samozapalne", 
                                "Materiały wytwarzające \nw zetknięciu z wodą gazy palne", "Materiały utleniające", "Materiały organiczne", 
                                "Materiały trujące", "Materiały zakaźne", "Materiały \npromieniotwórcze", "Materiały żrące", 
                                "Różne materiały \ni przedmioty \nniebezpieczne" };
        List<CheckBox> CheckBoxList;
        projektEntities context;
        public CargoForm( int? id = null)
        {
            CheckBoxList = new List<CheckBox>();
            InitializeComponent();
            context = new projektEntities();
            Functions.addColumnsToDataGridView(dataGridView1, ColumnNames);
            dataGridView = dataGridView1;
            DrawADRCheckBoxes();

            LoadData();

            //Console.WriteLine("laweta ma numer " + Functions.FindStringIndex(TypesPL,"a"));
            type.Items.Clear();
            type.Items.AddRange(TypesPL);
            type.Text = "Kontener";
            if (id.HasValue && id.Value >= 0)
            {
                try
                {
                    Functions.FindCargo((int)id);
                    this.id.Text = id + "";
                    ShowCargo(id.Value);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Nie znaleziono takiego rekordu");
                }

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
                cb.TabIndex = 5 + i;
                cb.Text = Functions.classes[i];
                cb.Tag = i;
                cb.MouseEnter += ((object sender, EventArgs e) => cb.Text = Functions.classes[(int)cb.Tag] + " " + ADRClass[(int)cb.Tag]);
                cb.MouseLeave += ((object sender, EventArgs e) => cb.Text = Functions.classes[(int)cb.Tag]);
                cb.UseVisualStyleBackColor = true;
                CheckBoxList.Add(cb);
                this.Controls.Add(cb);
            }
        }
        void LoadData()
        {
            Queries.getCargo();

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width / dataGridView1.Columns.Count - 8;
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width += 4;

            Functions.TranslateTrueFalse(dataGridView1,3);

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int r = 0; r < dataGridView1.RowCount; r++)
                {
                    if (Types.Contains(dataGridView1.Rows[r].Cells[i].Value + ""))
                        dataGridView1.Rows[r].Cells[i].Value = TypesPL[(int)(Functions.Types)Enum.Parse(typeof(Functions.Types), dataGridView1.Rows[r].Cells[i].Value + "")];
                }
            }



        }
        void ClearForm()
        {
            id.Text = "";
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
            if (dataGridView.CurrentRow.Index >= dataGridView.RowCount - 1)
            {
                return;
            }
            id.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            int Id = int.Parse(id.Text); 
            ShowCargo(Id);


        }
        private void ShowCargo(int Id)
        {
            cargo Cargo = Functions.FindCargo(Id);

            name.Text = Cargo.Name;
            type.Text = Cargo.Type;
            comment.Text = Cargo.Comment;
            if (Functions.FindStringIndex(Types, Cargo.Type) < 0)
                type.Text = Cargo.Type;
            else
                type.Text = TypesPL[Functions.FindStringIndex(Types, Cargo.Type)];
            adr.Checked = Boolean.Parse(Cargo.ADR);
            Boolean[] adrClass = Functions.ExpADR(Cargo.ADR_Class);
            foreach (var item in CheckBoxList)
            {
                item.Checked = adrClass[(int)item.Tag];
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
            var query = (from c in context.cargo
                         where c.Id == Id
                         select new
                         {
                             Cargo = c
                         }).First();
            if (query.Cargo == null)
            {
                MessageBox.Show("Nie znaleziono auta o takim Id");
                return;
            }

            query.Cargo.Name = name.Text;
            query.Cargo.ADR = adr.Checked.ToString();
            query.Cargo.Type = Types[Functions.FindStringIndex(TypesPL, type.Text)];
            query.Cargo.Comment = comment.Text;

            string adrClass = "";
            foreach (var item in CheckBoxList)
            {
                if (item.Checked)
                {
                    adrClass += Functions.classes[(int)item.Tag];
                }
                adrClass += ",";
                //Cargo.AdrClass[(int)item.Tag] = item.Checked;
            }
            adrClass = adrClass.Remove(adrClass.LastIndexOf(','));
            query.Cargo.ADR_Class = adrClass;
            context.Entry(query.Cargo).State = EntityState.Modified;
            context.SaveChanges();
            
            LoadData();
            dataGridView.CurrentCell = dataGridView[currentCol, currentRow];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //////FreightsList.Types a = FreightsList.Types.Container;
            //////string b = "Flatbed";
            //////a = (FreightsList.Types)2;
            //////Console.WriteLine("Types a = " + a + (int)a);
            //////Console.WriteLine(b + " == " + a + "  asasd ");
            //////Console.WriteLine((string)a.ToString() == b);
            //////Console.WriteLine("");
            //////MessageBox.Show(dataGridView1.Rows[1].Cells[4].Value.ToString());

            string adrClass = "";
            foreach (var item in CheckBoxList)
            {
                if (item.Checked)
                {
                    adrClass += Functions.classes[(int)item.Tag];
                }
                adrClass += ",";
                //Cargo.AdrClass[(int)item.Tag] = item.Checked;
            }
            adrClass = adrClass.Remove(adrClass.LastIndexOf(','));

            cargo Cargo = new cargo()
            {
                Name = name.Text,
                ADR = adr.Checked.ToString(),
                Type = Types[Functions.FindStringIndex(TypesPL, type.Text)],
                Comment = comment.Text,
                ADR_Class = adrClass
            };

            projektEntities context = new projektEntities();
            context.cargo.Add(Cargo);
            context.SaveChanges();

            LoadData();
            dataGridView.CurrentCell = dataGridView[0, dataGridView.RowCount - 2];
            id.Text = Cargo.Id.ToString();
            ////string adrClass = "";
            ////foreach (var item in CheckBoxList)
            ////{
            ////    if (item.Checked)
            ////    {
            ////        adrClass += Functions.classes[(int)item.Tag] + ",";
            ////    }
            ////}
            ////if (adrClass.Length > 0)
            ////    adrClass.Remove(adrClass.Length - 1, 1);
            ////else
            ////    adr.Checked = false;
            ////DataBase.AddFreightsList(name.Text, Types[Functions.FindStringIndex(TypesPL, type.Text)], adrClass, adr.Checked, comment.Text);
            ////LoadData(DataBase);

            //////if (Functions.AllowedPlate(plate.Text, DataBase.CarsList))
            //////{
            //////    //DataBase.AddCar(plate.Text, make.Text, model.Text, (uint)carry.Value, false, false, comment.Text);
            //////    //DataBase.CarsList.Add(new Cars());
            //////    //LoadData(DataBase);
            //////}
            //////else
            //////{
            //////    MessageBox.Show("Istnieje już auto o takiej rejestracji");
            //////}
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
