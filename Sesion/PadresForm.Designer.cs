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
            cerrarSesiónToolStripMenuItem = new ToolStripMenuItem();
            editarDatosPersonalesToolStripMenuItem = new ToolStripMenuItem();
            monthCalendar1 = new MonthCalendar();
            btnApuntar = new Button();
            button2 = new Button();
            LbNinos = new ListBox();
            Actividades = new ListBox();
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
            usuarioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cerrarSesiónToolStripMenuItem, editarDatosPersonalesToolStripMenuItem });
            usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            usuarioToolStripMenuItem.Size = new Size(59, 20);
            usuarioToolStripMenuItem.Text = "Usuario";
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            cerrarSesiónToolStripMenuItem.Size = new Size(196, 22);
            cerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión";
            cerrarSesiónToolStripMenuItem.Click += cerrarSesiónToolStripMenuItem_Click;
            // 
            // editarDatosPersonalesToolStripMenuItem
            // 
            editarDatosPersonalesToolStripMenuItem.Name = "editarDatosPersonalesToolStripMenuItem";
            editarDatosPersonalesToolStripMenuItem.Size = new Size(196, 22);
            editarDatosPersonalesToolStripMenuItem.Text = "Editar Datos Personales";
            editarDatosPersonalesToolStripMenuItem.Click += editarDatosPersonalesToolStripMenuItem_Click;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Anchor = AnchorStyles.None;
            monthCalendar1.CalendarDimensions = new Size(1, 2);
            monthCalendar1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            monthCalendar1.Location = new Point(18, 33);
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.ShowToday = false;
            monthCalendar1.TabIndex = 1;
            monthCalendar1.DateChanged += monthCalendar1_DateChanged;
            // 
            // btnApuntar
            // 
            btnApuntar.Anchor = AnchorStyles.None;
            btnApuntar.BackColor = Color.Red;
            btnApuntar.Location = new Point(18, 369);
            btnApuntar.Name = "btnApuntar";
            btnApuntar.Size = new Size(82, 52);
            btnApuntar.TabIndex = 2;
            btnApuntar.Text = "Apuntar";
            btnApuntar.UseVisualStyleBackColor = false;
            btnApuntar.Click += btnApuntar_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.None;
            button2.BackColor = Color.Red;
            button2.Location = new Point(128, 369);
            button2.Name = "button2";
            button2.Size = new Size(82, 52);
            button2.TabIndex = 3;
            button2.Text = "DesApuntar";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // LbNinos
            // 
            LbNinos.Anchor = AnchorStyles.None;
            LbNinos.Font = new Font("Segoe UI", 9.75F);
            LbNinos.FormattingEnabled = true;
            LbNinos.ItemHeight = 17;
            LbNinos.Location = new Point(222, 33);
            LbNinos.Name = "LbNinos";
            LbNinos.Size = new Size(566, 123);
            LbNinos.TabIndex = 4;
            // 
            // Actividades
            // 
            Actividades.Anchor = AnchorStyles.None;
            Actividades.Font = new Font("Segoe UI", 9.75F);
            Actividades.FormattingEnabled = true;
            Actividades.ItemHeight = 17;
            Actividades.Location = new Point(222, 193);
            Actividades.Name = "Actividades";
            Actividades.Size = new Size(566, 123);
            Actividades.TabIndex = 5;
            // 
            // PadresForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(800, 450);
            Controls.Add(Actividades);
            Controls.Add(LbNinos);
            Controls.Add(button2);
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
        private Button button2;
        public ListBox LbNinos;
        private ListBox Actividades;
        private ToolStripMenuItem usuarioToolStripMenuItem;
        private ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private ToolStripMenuItem editarDatosPersonalesToolStripMenuItem;
    }
}