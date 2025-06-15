namespace TFG_DavidGomez
{
    partial class MonitorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorForm));
            menuStrip1 = new MenuStrip();
            añadirMonitorToolStripMenuItem = new ToolStripMenuItem();
            modificarActividadToolStripMenuItem = new ToolStripMenuItem();
            usuarioToolStripMenuItem = new ToolStripMenuItem();
            datosPersonalesToolStripMenuItem = new ToolStripMenuItem();
            cerrarSesionToolStripMenuItem = new ToolStripMenuItem();
            lbFecha = new Label();
            LbFecha2 = new Label();
            LAtividad = new Label();
            LbAtividad2 = new Label();
            LMateriales = new Label();
            label1 = new Label();
            dataGridMateriales = new DataGridView();
            dataGridNinos = new DataGridView();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridMateriales).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridNinos).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { añadirMonitorToolStripMenuItem, modificarActividadToolStripMenuItem, usuarioToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // añadirMonitorToolStripMenuItem
            // 
            añadirMonitorToolStripMenuItem.Name = "añadirMonitorToolStripMenuItem";
            añadirMonitorToolStripMenuItem.Size = new Size(100, 20);
            añadirMonitorToolStripMenuItem.Text = "Añadir Monitor";
            añadirMonitorToolStripMenuItem.Visible = false;
            añadirMonitorToolStripMenuItem.Click += añadirMonitorToolStripMenuItem_Click;
            // 
            // modificarActividadToolStripMenuItem
            // 
            modificarActividadToolStripMenuItem.Name = "modificarActividadToolStripMenuItem";
            modificarActividadToolStripMenuItem.Size = new Size(157, 20);
            modificarActividadToolStripMenuItem.Text = "Agregar o Editar Actividad";
            modificarActividadToolStripMenuItem.Visible = false;
            modificarActividadToolStripMenuItem.Click += modificarActividadToolStripMenuItem_Click;
            // 
            // usuarioToolStripMenuItem
            // 
            usuarioToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            usuarioToolStripMenuItem.BackgroundImageLayout = ImageLayout.Stretch;
            usuarioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { datosPersonalesToolStripMenuItem, cerrarSesionToolStripMenuItem });
            usuarioToolStripMenuItem.Image = (Image)resources.GetObject("usuarioToolStripMenuItem.Image");
            usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            usuarioToolStripMenuItem.RightToLeft = RightToLeft.No;
            usuarioToolStripMenuItem.Size = new Size(75, 20);
            usuarioToolStripMenuItem.Text = "Usuario";
            // 
            // datosPersonalesToolStripMenuItem
            // 
            datosPersonalesToolStripMenuItem.Name = "datosPersonalesToolStripMenuItem";
            datosPersonalesToolStripMenuItem.Size = new Size(196, 22);
            datosPersonalesToolStripMenuItem.Text = "Editar Datos Personales";
            datosPersonalesToolStripMenuItem.Click += datosPersonalesToolStripMenuItem_Click;
            // 
            // cerrarSesionToolStripMenuItem
            // 
            cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            cerrarSesionToolStripMenuItem.Size = new Size(196, 22);
            cerrarSesionToolStripMenuItem.Text = "Cerrar Sesion";
            cerrarSesionToolStripMenuItem.Click += cerrarSesionToolStripMenuItem_Click;
            // 
            // lbFecha
            // 
            lbFecha.Anchor = AnchorStyles.None;
            lbFecha.AutoSize = true;
            lbFecha.Location = new Point(20, 41);
            lbFecha.Name = "lbFecha";
            lbFecha.Size = new Size(41, 15);
            lbFecha.TabIndex = 1;
            lbFecha.Text = "Fecha:";
            // 
            // LbFecha2
            // 
            LbFecha2.Anchor = AnchorStyles.None;
            LbFecha2.AutoSize = true;
            LbFecha2.Location = new Point(67, 41);
            LbFecha2.Name = "LbFecha2";
            LbFecha2.Size = new Size(65, 15);
            LbFecha2.TabIndex = 2;
            LbFecha2.Text = "13/11/2024";
            // 
            // LAtividad
            // 
            LAtividad.Anchor = AnchorStyles.None;
            LAtividad.AutoSize = true;
            LAtividad.Location = new Point(176, 41);
            LAtividad.Name = "LAtividad";
            LAtividad.Size = new Size(60, 15);
            LAtividad.TabIndex = 3;
            LAtividad.Text = "Actividad:";
            // 
            // LbAtividad2
            // 
            LbAtividad2.Anchor = AnchorStyles.None;
            LbAtividad2.AutoSize = true;
            LbAtividad2.Location = new Point(242, 41);
            LbAtividad2.Name = "LbAtividad2";
            LbAtividad2.Size = new Size(69, 15);
            LbAtividad2.TabIndex = 4;
            LbAtividad2.Text = "Papiroflexia";
            // 
            // LMateriales
            // 
            LMateriales.Anchor = AnchorStyles.None;
            LMateriales.AutoSize = true;
            LMateriales.Location = new Point(20, 72);
            LMateriales.Name = "LMateriales";
            LMateriales.Size = new Size(64, 15);
            LMateriales.TabIndex = 5;
            LMateriales.Text = "Materiales:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(424, 72);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 7;
            label1.Text = "Niños:";
            // 
            // dataGridMateriales
            // 
            dataGridMateriales.Anchor = AnchorStyles.None;
            dataGridMateriales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridMateriales.Location = new Point(20, 90);
            dataGridMateriales.Name = "dataGridMateriales";
            dataGridMateriales.ReadOnly = true;
            dataGridMateriales.Size = new Size(289, 327);
            dataGridMateriales.TabIndex = 8;
            // 
            // dataGridNinos
            // 
            dataGridNinos.Anchor = AnchorStyles.None;
            dataGridNinos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridNinos.Location = new Point(424, 90);
            dataGridNinos.Name = "dataGridNinos";
            dataGridNinos.ReadOnly = true;
            dataGridNinos.Size = new Size(289, 327);
            dataGridNinos.TabIndex = 9;
            // 
            // MonitorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.LightGray;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridNinos);
            Controls.Add(dataGridMateriales);
            Controls.Add(label1);
            Controls.Add(LMateriales);
            Controls.Add(LbAtividad2);
            Controls.Add(LAtividad);
            Controls.Add(LbFecha2);
            Controls.Add(lbFecha);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MonitorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MonitorForm";
            WindowState = FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridMateriales).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridNinos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        public ToolStripMenuItem añadirMonitorToolStripMenuItem;
        private Label lbFecha;
        private Label LbFecha2;
        private Label LAtividad;
        private Label LbAtividad2;
        private Label LMateriales;
        private Label label1;
        private ToolStripMenuItem usuarioToolStripMenuItem;
        private ToolStripMenuItem datosPersonalesToolStripMenuItem;
        private ToolStripMenuItem cerrarSesionToolStripMenuItem;
        public ToolStripMenuItem modificarActividadToolStripMenuItem;
        private DataGridView dataGridMateriales;
        private DataGridView dataGridNinos;
    }
}