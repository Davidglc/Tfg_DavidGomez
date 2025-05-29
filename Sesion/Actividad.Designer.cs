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
            txtNombre = new TextBox();
            txtFecha = new TextBox();
            cbNinos = new ComboBox();
            dgvActividades = new DataGridView();
            btnSeleccionarImagen = new Button();
            btnGuardar = new Button();
            lbcbNino = new Label();
            txtDescripcion = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvActividades).BeginInit();
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
            btnApuntar.Location = new Point(293, 330);
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
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.None;
            txtNombre.Location = new Point(145, 37);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(230, 23);
            txtNombre.TabIndex = 5;
            txtNombre.Visible = false;
            // 
            // txtFecha
            // 
            txtFecha.Anchor = AnchorStyles.None;
            txtFecha.Location = new Point(145, 76);
            txtFecha.Name = "txtFecha";
            txtFecha.Size = new Size(100, 23);
            txtFecha.TabIndex = 6;
            txtFecha.Visible = false;
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
            // dgvActividades
            // 
            dgvActividades.Anchor = AnchorStyles.None;
            dgvActividades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvActividades.Location = new Point(524, 199);
            dgvActividades.Name = "dgvActividades";
            dgvActividades.ReadOnly = true;
            dgvActividades.Size = new Size(596, 232);
            dgvActividades.TabIndex = 10;
            dgvActividades.Visible = false;
            dgvActividades.CellClick += dgvActividades_CellClick;
            // 
            // btnSeleccionarImagen
            // 
            btnSeleccionarImagen.Anchor = AnchorStyles.None;
            btnSeleccionarImagen.BackColor = Color.DodgerBlue;
            btnSeleccionarImagen.FlatAppearance.BorderSize = 0;
            btnSeleccionarImagen.FlatStyle = FlatStyle.Flat;
            btnSeleccionarImagen.ForeColor = Color.White;
            btnSeleccionarImagen.Location = new Point(293, 345);
            btnSeleccionarImagen.Name = "btnSeleccionarImagen";
            btnSeleccionarImagen.Size = new Size(82, 52);
            btnSeleccionarImagen.TabIndex = 11;
            btnSeleccionarImagen.Text = "Imagen";
            btnSeleccionarImagen.UseVisualStyleBackColor = false;
            btnSeleccionarImagen.Visible = false;
            btnSeleccionarImagen.Click += btnSeleccionarImagen_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.None;
            btnGuardar.BackColor = Color.DodgerBlue;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(25, 345);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(82, 52);
            btnGuardar.TabIndex = 12;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Visible = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // lbcbNino
            // 
            lbcbNino.Anchor = AnchorStyles.None;
            lbcbNino.AutoSize = true;
            lbcbNino.Location = new Point(143, 312);
            lbcbNino.Name = "lbcbNino";
            lbcbNino.Size = new Size(93, 15);
            lbcbNino.TabIndex = 13;
            lbcbNino.Text = "Seleccione niño:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Anchor = AnchorStyles.None;
            txtDescripcion.Location = new Point(143, 117);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(338, 143);
            txtDescripcion.TabIndex = 14;
            txtDescripcion.Visible = false;
            // 
            // Actividad
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtDescripcion);
            Controls.Add(lbcbNino);
            Controls.Add(btnGuardar);
            Controls.Add(btnSeleccionarImagen);
            Controls.Add(dgvActividades);
            Controls.Add(cbNinos);
            Controls.Add(txtFecha);
            Controls.Add(txtNombre);
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
            ((System.ComponentModel.ISupportInitialize)dgvActividades).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lb_nombre;
        private Label lb_des;
        private Label lb_Fecha;
        public Button btnApuntar;
        private Panel pn_Img;
        public TextBox txtNombre;
        public TextBox txtFecha;
        public ComboBox cbNinos;
        public DataGridView dgvActividades;
        public Button btnSeleccionarImagen;
        public Button btnGuardar;
        public Label lbcbNino;
        public TextBox txtDescripcion;
    }
}