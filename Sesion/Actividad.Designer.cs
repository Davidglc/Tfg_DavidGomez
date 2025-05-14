namespace TFG_DavidGomez.Sesion
{
    partial class Actividad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Actividad));
            lb_nombre = new Label();
            lb_des = new Label();
            lb_Fecha = new Label();
            btnApuntar = new Button();
            pn_Img = new Panel();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            cbNinos = new ComboBox();
            SuspendLayout();
            // 
            // lb_nombre
            // 
            lb_nombre.Anchor = AnchorStyles.None;
            lb_nombre.AutoSize = true;
            lb_nombre.Location = new Point(25, 40);
            lb_nombre.Name = "lb_nombre";
            lb_nombre.Size = new Size(51, 15);
            lb_nombre.TabIndex = 0;
            lb_nombre.Text = "Nombre";
            // 
            // lb_des
            // 
            lb_des.Anchor = AnchorStyles.None;
            lb_des.AutoSize = true;
            lb_des.Location = new Point(25, 117);
            lb_des.Name = "lb_des";
            lb_des.Size = new Size(69, 15);
            lb_des.TabIndex = 1;
            lb_des.Text = "Descripción";
            // 
            // lb_Fecha
            // 
            lb_Fecha.Anchor = AnchorStyles.None;
            lb_Fecha.AutoSize = true;
            lb_Fecha.Location = new Point(25, 79);
            lb_Fecha.Name = "lb_Fecha";
            lb_Fecha.Size = new Size(38, 15);
            lb_Fecha.TabIndex = 2;
            lb_Fecha.Text = "Fecha";
            // 
            // btnApuntar
            // 
            btnApuntar.Anchor = AnchorStyles.None;
            btnApuntar.BackColor = Color.DodgerBlue;
            btnApuntar.FlatAppearance.BorderSize = 0;
            btnApuntar.FlatStyle = FlatStyle.Flat;
            btnApuntar.ForeColor = Color.White;
            btnApuntar.Location = new Point(25, 330);
            btnApuntar.Name = "btnApuntar";
            btnApuntar.Size = new Size(82, 52);
            btnApuntar.TabIndex = 3;
            btnApuntar.Text = "Apuntar";
            btnApuntar.UseVisualStyleBackColor = false;
            btnApuntar.Click += btnApuntar_Click;
            // 
            // pn_Img
            // 
            pn_Img.Anchor = AnchorStyles.None;
            pn_Img.BackgroundImage = (Image)resources.GetObject("pn_Img.BackgroundImage");
            pn_Img.BackgroundImageLayout = ImageLayout.Stretch;
            pn_Img.Location = new Point(526, 40);
            pn_Img.Name = "pn_Img";
            pn_Img.Size = new Size(164, 140);
            pn_Img.TabIndex = 4;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Location = new Point(145, 37);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 5;
            textBox1.Visible = false;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.Location = new Point(145, 71);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 6;
            textBox2.Visible = false;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.None;
            textBox3.Location = new Point(145, 109);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 7;
            textBox3.Visible = false;
            // 
            // cbNinos
            // 
            cbNinos.Anchor = AnchorStyles.None;
            cbNinos.FormattingEnabled = true;
            cbNinos.Location = new Point(145, 330);
            cbNinos.Name = "cbNinos";
            cbNinos.Size = new Size(121, 23);
            cbNinos.TabIndex = 8;
            // 
            // Actividad
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cbNinos);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(pn_Img);
            Controls.Add(btnApuntar);
            Controls.Add(lb_Fecha);
            Controls.Add(lb_des);
            Controls.Add(lb_nombre);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Actividad";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Actvidad";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lb_nombre;
        private Label lb_des;
        private Label lb_Fecha;
        private Button btnApuntar;
        private Panel pn_Img;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private ComboBox cbNinos;
    }
}