namespace TFG_DavidGomez
{
    partial class PadresForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PadresForm));
            menuStrip1 = new MenuStrip();
            añadirNiñoToolStripMenuItem = new ToolStripMenuItem();
            usuarioToolStripMenuItem = new ToolStripMenuItem();
            editarDatosPersonalesToolStripMenuItem = new ToolStripMenuItem();
            cerrarSesiónToolStripMenuItem = new ToolStripMenuItem();
            monthCalendar1 = new MonthCalendar();
            btnApuntar = new Button();
            btn_DA = new Button();
            LbNinos = new ListBox();
            Actividades = new ListBox();
            btn_VP = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { añadirNiñoToolStripMenuItem, usuarioToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // añadirNiñoToolStripMenuItem
            // 
            añadirNiñoToolStripMenuItem.Name = "añadirNiñoToolStripMenuItem";
            añadirNiñoToolStripMenuItem.Size = new Size(83, 20);
            añadirNiñoToolStripMenuItem.Text = "Añadir Niño";
            añadirNiñoToolStripMenuItem.Click += añadirNiñoToolStripMenuItem_Click;
            // 
            // usuarioToolStripMenuItem
            // 
            usuarioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { editarDatosPersonalesToolStripMenuItem, cerrarSesiónToolStripMenuItem });
            usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            usuarioToolStripMenuItem.Size = new Size(59, 20);
            usuarioToolStripMenuItem.Text = "Usuario";
            // 
            // editarDatosPersonalesToolStripMenuItem
            // 
            editarDatosPersonalesToolStripMenuItem.Name = "editarDatosPersonalesToolStripMenuItem";
            editarDatosPersonalesToolStripMenuItem.Size = new Size(196, 22);
            editarDatosPersonalesToolStripMenuItem.Text = "Editar Datos Personales";
            editarDatosPersonalesToolStripMenuItem.Click += editarDatosPersonalesToolStripMenuItem_Click;
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            cerrarSesiónToolStripMenuItem.Size = new Size(196, 22);
            cerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión";
            cerrarSesiónToolStripMenuItem.Click += cerrarSesiónToolStripMenuItem_Click;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Anchor = AnchorStyles.None;
            monthCalendar1.BackColor = Color.LightGray;
            monthCalendar1.CalendarDimensions = new Size(1, 2);
            monthCalendar1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            monthCalendar1.Location = new Point(11, 33);
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.ShowToday = false;
            monthCalendar1.TabIndex = 1;
            monthCalendar1.DateChanged += monthCalendar1_DateChanged;
            // 
            // btnApuntar
            // 
            btnApuntar.Anchor = AnchorStyles.None;
            btnApuntar.BackColor = Color.DodgerBlue;
            btnApuntar.FlatAppearance.BorderSize = 0;
            btnApuntar.FlatStyle = FlatStyle.Flat;
            btnApuntar.ForeColor = Color.White;
            btnApuntar.Location = new Point(128, 380);
            btnApuntar.Name = "btnApuntar";
            btnApuntar.Size = new Size(82, 52);
            btnApuntar.TabIndex = 2;
            btnApuntar.Text = "Apuntar";
            btnApuntar.UseVisualStyleBackColor = false;
            btnApuntar.Click += btnApuntar_Click;
            // 
            // btn_DA
            // 
            btn_DA.Anchor = AnchorStyles.None;
            btn_DA.BackColor = Color.DodgerBlue;
            btn_DA.FlatAppearance.BorderSize = 0;
            btn_DA.FlatStyle = FlatStyle.Flat;
            btn_DA.ForeColor = Color.White;
            btn_DA.Location = new Point(238, 380);
            btn_DA.Name = "btn_DA";
            btn_DA.Size = new Size(82, 52);
            btn_DA.TabIndex = 3;
            btn_DA.Text = "DesApuntar";
            btn_DA.UseVisualStyleBackColor = false;
            btn_DA.Click += button2_Click;
            // 
            // LbNinos
            // 
            LbNinos.Anchor = AnchorStyles.None;
            LbNinos.Font = new Font("Segoe UI", 9.75F);
            LbNinos.FormattingEnabled = true;
            LbNinos.ItemHeight = 17;
            LbNinos.Location = new Point(215, 33);
            LbNinos.Name = "LbNinos";
            LbNinos.Size = new Size(579, 123);
            LbNinos.TabIndex = 4;
            // 
            // Actividades
            // 
            Actividades.Anchor = AnchorStyles.None;
            Actividades.Font = new Font("Segoe UI", 9.75F);
            Actividades.FormattingEnabled = true;
            Actividades.ItemHeight = 17;
            Actividades.Location = new Point(215, 193);
            Actividades.Name = "Actividades";
            Actividades.Size = new Size(579, 123);
            Actividades.TabIndex = 5;
            // 
            // btn_VP
            // 
            btn_VP.Anchor = AnchorStyles.None;
            btn_VP.BackColor = Color.DodgerBlue;
            btn_VP.FlatAppearance.BorderSize = 0;
            btn_VP.FlatStyle = FlatStyle.Flat;
            btn_VP.ForeColor = Color.White;
            btn_VP.Location = new Point(18, 380);
            btn_VP.Name = "btn_VP";
            btn_VP.Size = new Size(82, 52);
            btn_VP.TabIndex = 6;
            btn_VP.Text = "Vista Previa";
            btn_VP.UseVisualStyleBackColor = false;
            btn_VP.Click += btn_VP_Click;
            // 
            // PadresForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.LightGray;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_VP);
            Controls.Add(Actividades);
            Controls.Add(LbNinos);
            Controls.Add(btn_DA);
            Controls.Add(btnApuntar);
            Controls.Add(monthCalendar1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "PadresForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PadresForm";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem añadirNiñoToolStripMenuItem;
        private MonthCalendar monthCalendar1;
        private Button btnApuntar;
        private Button btn_DA;
        public ListBox LbNinos;
        private ListBox Actividades;
        private ToolStripMenuItem usuarioToolStripMenuItem;
        private ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private ToolStripMenuItem editarDatosPersonalesToolStripMenuItem;
        private Button btn_VP;
    }
}