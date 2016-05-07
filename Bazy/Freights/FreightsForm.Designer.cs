namespace Bazy
{
    partial class FreightsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comment = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.ComboBox();
            this.destination = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cargo = new System.Windows.Forms.ComboBox();
            this.date = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.amount = new System.Windows.Forms.NumericUpDown();
            this.weight = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weight)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 223);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(572, 227);
            this.dataGridView1.TabIndex = 22;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Id";
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(100, 10);
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Size = new System.Drawing.Size(239, 20);
            this.id.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Skąd";
            // 
            // comment
            // 
            this.comment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comment.Location = new System.Drawing.Point(362, 31);
            this.comment.MaxLength = 200;
            this.comment.Multiline = true;
            this.comment.Name = "comment";
            this.comment.Size = new System.Drawing.Size(222, 157);
            this.comment.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(360, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Komentarz";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Zapisz zmiany";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(129, 194);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Wyczyść";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(233, 194);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "Dodaj zlecenie";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Towar";
            // 
            // start
            // 
            this.start.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.start.Location = new System.Drawing.Point(100, 62);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(238, 21);
            this.start.TabIndex = 3;
            // 
            // destination
            // 
            this.destination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.destination.Location = new System.Drawing.Point(100, 89);
            this.destination.Name = "destination";
            this.destination.Size = new System.Drawing.Size(238, 21);
            this.destination.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Dokąd";
            // 
            // cargo
            // 
            this.cargo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cargo.Location = new System.Drawing.Point(101, 36);
            this.cargo.Name = "cargo";
            this.cargo.Size = new System.Drawing.Size(238, 21);
            this.cargo.TabIndex = 25;
            // 
            // date
            // 
            this.date.CustomFormat = "yyyy-MM-dd, HH:mm:ss";
            this.date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date.Location = new System.Drawing.Point(101, 116);
            this.date.Name = "date";
            this.date.ShowUpDown = true;
            this.date.Size = new System.Drawing.Size(238, 20);
            this.date.TabIndex = 26;
            this.date.Value = new System.DateTime(2015, 9, 14, 14, 50, 46, 501);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Czas dostawy";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Sztuk";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 170);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Waga";
            // 
            // amount
            // 
            this.amount.Location = new System.Drawing.Point(100, 142);
            this.amount.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(239, 20);
            this.amount.TabIndex = 30;
            this.amount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // weight
            // 
            this.weight.Location = new System.Drawing.Point(100, 168);
            this.weight.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.weight.Name = "weight";
            this.weight.Size = new System.Drawing.Size(239, 20);
            this.weight.TabIndex = 31;
            // 
            // FreightsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 462);
            this.Controls.Add(this.weight);
            this.Controls.Add(this.amount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.date);
            this.Controls.Add(this.cargo);
            this.Controls.Add(this.destination);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.start);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comment);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "FreightsForm";
            this.Text = "Freights";
            this.Resize += new System.EventHandler(this.CarsForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox comment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox start;
        private System.Windows.Forms.ComboBox destination;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cargo;
        private System.Windows.Forms.DateTimePicker date;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown amount;
        private System.Windows.Forms.NumericUpDown weight;
    }
}