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
            dbgMateriales = new DataGridView();
            lblMonitor = new Label();
            PanelParent = new Panel();
            panel2 = new Panel();
            lbMateriales = new Label();
            panel3 = new Panel();
            panel1 = new Panel();
            btn_LimpiarCampos = new Button();
            btn_Eliminar = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)dgvActividades).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dbgMateriales).BeginInit();
            PanelParent.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lb_nombre
            // 
            lb_nombre.Anchor = AnchorStyles.None;
            lb_nombre.AutoSize = true;
            lb_nombre.Location = new Point(25, -10);
            lb_nombre.Name = "lb_nombre";
            lb_nombre.Size = new Size(51, 15);
            lb_nombre.TabIndex = 0;
            lb_nombre.Text = "Nombre";
            // 
            // lb_des
            // 
            lb_des.Anchor = AnchorStyles.None;
            lb_des.AutoSize = true;
            lb_des.Location = new Point(25, 70);
            lb_des.MaximumSize = new Size(600, 0);
            lb_des.Name = "lb_des";
            lb_des.Size = new Size(69, 15);
            lb_des.TabIndex = 1;
            lb_des.Text = "Descripción";
            // 
            // lb_Fecha
            // 
            lb_Fecha.Anchor = AnchorStyles.None;
            lb_Fecha.AutoSize = true;
            lb_Fecha.Location = new Point(25, 29);
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
            btnApuntar.Location = new Point(418, 18);
            btnApuntar.Name = "btnApuntar";
            btnApuntar.Size = new Size(82, 52);
            btnApuntar.TabIndex = 3;
            btnApuntar.Text = "Apuntar";
            btnApuntar.UseVisualStyleBackColor = false;
            btnApuntar.Click += btnApuntar_Click;
            // 
            // pn_Img
            // 
            pn_Img.BackgroundImage = (Image)resources.GetObject("pn_Img.BackgroundImage");
            pn_Img.BackgroundImageLayout = ImageLayout.Stretch;
            pn_Img.Location = new Point(348, 26);
            pn_Img.Name = "pn_Img";
            pn_Img.Size = new Size(212, 184);
            pn_Img.TabIndex = 4;
            // 
            // txtNombre
            // 
            txtNombre.Anchor = AnchorStyles.None;
            txtNombre.Location = new Point(145, -13);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(230, 23);
            txtNombre.TabIndex = 5;
            txtNombre.Visible = false;
            // 
            // txtFecha
            // 
            txtFecha.Anchor = AnchorStyles.None;
            txtFecha.Location = new Point(145, 26);
            txtFecha.Name = "txtFecha";
            txtFecha.Size = new Size(100, 23);
            txtFecha.TabIndex = 6;
            txtFecha.Visible = false;
            // 
            // cbNinos
            // 
            cbNinos.Anchor = AnchorStyles.None;
            cbNinos.FormattingEnabled = true;
            cbNinos.Location = new Point(254, 18);
            cbNinos.Name = "cbNinos";
            cbNinos.Size = new Size(121, 23);
            cbNinos.TabIndex = 8;
            // 
            // dgvActividades
            // 
            dgvActividades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvActividades.Location = new Point(348, 219);
            dgvActividades.Name = "dgvActividades";
            dgvActividades.ReadOnly = true;
            dgvActividades.Size = new Size(430, 166);
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
            btnSeleccionarImagen.Location = new Point(418, 18);
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
            btnGuardar.Location = new Point(109, 18);
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
            lbcbNino.Location = new Point(254, 0);
            lbcbNino.Name = "lbcbNino";
            lbcbNino.Size = new Size(93, 15);
            lbcbNino.TabIndex = 13;
            lbcbNino.Text = "Seleccione niño:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Anchor = AnchorStyles.None;
            txtDescripcion.Location = new Point(143, 67);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(338, 143);
            txtDescripcion.TabIndex = 14;
            txtDescripcion.Visible = false;
            // 
            // dbgMateriales
            // 
            dbgMateriales.Anchor = AnchorStyles.None;
            dbgMateriales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dbgMateriales.Location = new Point(18, 234);
            dbgMateriales.Name = "dbgMateriales";
            dbgMateriales.Size = new Size(469, 110);
            dbgMateriales.TabIndex = 15;
            // 
            // lblMonitor
            // 
            lblMonitor.Anchor = AnchorStyles.None;
            lblMonitor.AutoSize = true;
            lblMonitor.Location = new Point(254, 0);
            lblMonitor.Name = "lblMonitor";
            lblMonitor.Size = new Size(112, 15);
            lblMonitor.TabIndex = 16;
            lblMonitor.Text = "Seleccione monitor:";
            lblMonitor.Visible = false;
            // 
            // PanelParent
            // 
            PanelParent.Controls.Add(panel2);
            PanelParent.Controls.Add(panel3);
            PanelParent.Controls.Add(panel1);
            PanelParent.Dock = DockStyle.Fill;
            PanelParent.Location = new Point(0, 0);
            PanelParent.Name = "PanelParent";
            PanelParent.Size = new Size(800, 450);
            PanelParent.TabIndex = 17;
            // 
            // panel2
            // 
            panel2.Controls.Add(lb_nombre);
            panel2.Controls.Add(lb_des);
            panel2.Controls.Add(lb_Fecha);
            panel2.Controls.Add(txtNombre);
            panel2.Controls.Add(txtDescripcion);
            panel2.Controls.Add(txtFecha);
            panel2.Controls.Add(lbMateriales);
            panel2.Controls.Add(dbgMateriales);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(0, 350);
            panel2.TabIndex = 18;
            // 
            // lbMateriales
            // 
            lbMateriales.Anchor = AnchorStyles.None;
            lbMateriales.AutoSize = true;
            lbMateriales.Location = new Point(12, 215);
            lbMateriales.Name = "lbMateriales";
            lbMateriales.Size = new Size(61, 15);
            lbMateriales.TabIndex = 17;
            lbMateriales.Text = "Materiales";
            // 
            // panel3
            // 
            panel3.Controls.Add(pn_Img);
            panel3.Controls.Add(dgvActividades);
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(800, 350);
            panel3.TabIndex = 18;
            // 
            // panel1
            // 
            panel1.Controls.Add(btn_LimpiarCampos);
            panel1.Controls.Add(btn_Eliminar);
            panel1.Controls.Add(lblMonitor);
            panel1.Controls.Add(lbcbNino);
            panel1.Controls.Add(btnGuardar);
            panel1.Controls.Add(btnSeleccionarImagen);
            panel1.Controls.Add(cbNinos);
            panel1.Controls.Add(btnApuntar);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 350);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 100);
            panel1.TabIndex = 11;
            // 
            // btn_LimpiarCampos
            // 
            btn_LimpiarCampos.Anchor = AnchorStyles.None;
            btn_LimpiarCampos.BackColor = Color.DodgerBlue;
            btn_LimpiarCampos.FlatAppearance.BorderSize = 0;
            btn_LimpiarCampos.FlatStyle = FlatStyle.Flat;
            btn_LimpiarCampos.ForeColor = Color.White;
            btn_LimpiarCampos.Location = new Point(506, 18);
            btn_LimpiarCampos.Name = "btn_LimpiarCampos";
            btn_LimpiarCampos.Size = new Size(82, 52);
            btn_LimpiarCampos.TabIndex = 18;
            btn_LimpiarCampos.Text = "Limpiar Campos";
            btn_LimpiarCampos.UseVisualStyleBackColor = false;
            btn_LimpiarCampos.Visible = false;
            btn_LimpiarCampos.Click += btn_LimpiarCampos_Click;
            // 
            // btn_Eliminar
            // 
            btn_Eliminar.Anchor = AnchorStyles.None;
            btn_Eliminar.BackColor = Color.DodgerBlue;
            btn_Eliminar.FlatAppearance.BorderSize = 0;
            btn_Eliminar.FlatStyle = FlatStyle.Flat;
            btn_Eliminar.ForeColor = Color.White;
            btn_Eliminar.Location = new Point(12, 18);
            btn_Eliminar.Name = "btn_Eliminar";
            btn_Eliminar.Size = new Size(82, 52);
            btn_Eliminar.TabIndex = 17;
            btn_Eliminar.Text = "Eliminar";
            btn_Eliminar.UseVisualStyleBackColor = false;
            btn_Eliminar.Visible = false;
            btn_Eliminar.Click += btn_Eliminar_Click;
            // 
            // Actividad
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PanelParent);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Actividad";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Actvidad";
            WindowState = FormWindowState.Maximized;
            Load += Actividad_Load;
            ((System.ComponentModel.ISupportInitialize)dgvActividades).EndInit();
            ((System.ComponentModel.ISupportInitialize)dbgMateriales).EndInit();
            PanelParent.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
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
        public DataGridView dbgMateriales;
        public Label lblMonitor;
        private Panel PanelParent;
        private Panel panel2;
        private Panel panel3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label lbMateriales;
        private Panel panel1;
        public Button btn_Eliminar;
        public Button btn_LimpiarCampos;
    }
}