namespace TFG_DavidGomez.Sesion
{
    partial class Eleccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Eleccion));
            btn_izq = new Button();
            btn_drch = new Button();
            menuStrip1 = new MenuStrip();
            añadirNiñoToolStripMenuItem = new ToolStripMenuItem();
            actividadesApuntadasToolStripMenuItem = new ToolStripMenuItem();
            usuarioToolStripMenuItem = new ToolStripMenuItem();
            editarDatosPersonalesToolStripMenuItem = new ToolStripMenuItem();
            cerrarSesiónToolStripMenuItem = new ToolStripMenuItem();
            PanelBotones = new FlowLayoutPanel();
            PanelParent = new Panel();
            PanelFill = new Panel();
            PanelMenu = new Panel();
            panel4 = new Panel();
            menuStrip1.SuspendLayout();
            PanelParent.SuspendLayout();
            PanelFill.SuspendLayout();
            PanelMenu.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // btn_izq
            // 
            btn_izq.BackColor = Color.DodgerBlue;
            btn_izq.Dock = DockStyle.Left;
            btn_izq.FlatAppearance.BorderSize = 0;
            btn_izq.FlatStyle = FlatStyle.Flat;
            btn_izq.ForeColor = Color.White;
            btn_izq.Location = new Point(0, 0);
            btn_izq.Name = "btn_izq";
            btn_izq.Size = new Size(93, 51);
            btn_izq.TabIndex = 1;
            btn_izq.Text = "Mes anterior";
            btn_izq.UseVisualStyleBackColor = false;
            btn_izq.Click += btn_izq_Click;
            // 
            // btn_drch
            // 
            btn_drch.BackColor = Color.DodgerBlue;
            btn_drch.Dock = DockStyle.Right;
            btn_drch.FlatAppearance.BorderSize = 0;
            btn_drch.FlatStyle = FlatStyle.Flat;
            btn_drch.ForeColor = Color.White;
            btn_drch.Location = new Point(676, 0);
            btn_drch.Name = "btn_drch";
            btn_drch.Size = new Size(103, 51);
            btn_drch.TabIndex = 2;
            btn_drch.Text = "Siguiente mes";
            btn_drch.UseVisualStyleBackColor = false;
            btn_drch.Click += btn_drch_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { añadirNiñoToolStripMenuItem, actividadesApuntadasToolStripMenuItem, usuarioToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(779, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // añadirNiñoToolStripMenuItem
            // 
            añadirNiñoToolStripMenuItem.Name = "añadirNiñoToolStripMenuItem";
            añadirNiñoToolStripMenuItem.Size = new Size(83, 20);
            añadirNiñoToolStripMenuItem.Text = "Añadir Niño";
            añadirNiñoToolStripMenuItem.Click += añadirNiñoToolStripMenuItem_Click;
            // 
            // actividadesApuntadasToolStripMenuItem
            // 
            actividadesApuntadasToolStripMenuItem.Name = "actividadesApuntadasToolStripMenuItem";
            actividadesApuntadasToolStripMenuItem.Size = new Size(140, 20);
            actividadesApuntadasToolStripMenuItem.Text = "Actividades Apuntadas";
            actividadesApuntadasToolStripMenuItem.Click += actividadesApuntadasToolStripMenuItem_Click;
            // 
            // usuarioToolStripMenuItem
            // 
            usuarioToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            usuarioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { editarDatosPersonalesToolStripMenuItem, cerrarSesiónToolStripMenuItem });
            usuarioToolStripMenuItem.Image = (Image)resources.GetObject("usuarioToolStripMenuItem.Image");
            usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            usuarioToolStripMenuItem.Size = new Size(75, 20);
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
            // PanelBotones
            // 
            PanelBotones.Dock = DockStyle.Fill;
            PanelBotones.Location = new Point(0, 0);
            PanelBotones.Name = "PanelBotones";
            PanelBotones.Size = new Size(779, 432);
            PanelBotones.TabIndex = 4;
            // 
            // PanelParent
            // 
            PanelParent.Controls.Add(PanelFill);
            PanelParent.Controls.Add(PanelMenu);
            PanelParent.Dock = DockStyle.Fill;
            PanelParent.Location = new Point(0, 0);
            PanelParent.Name = "PanelParent";
            PanelParent.Size = new Size(779, 454);
            PanelParent.TabIndex = 0;
            // 
            // PanelFill
            // 
            PanelFill.Controls.Add(PanelBotones);
            PanelFill.Dock = DockStyle.Fill;
            PanelFill.Location = new Point(0, 22);
            PanelFill.Name = "PanelFill";
            PanelFill.Size = new Size(779, 432);
            PanelFill.TabIndex = 1;
            // 
            // PanelMenu
            // 
            PanelMenu.Controls.Add(menuStrip1);
            PanelMenu.Dock = DockStyle.Top;
            PanelMenu.Location = new Point(0, 0);
            PanelMenu.Name = "PanelMenu";
            PanelMenu.Size = new Size(779, 22);
            PanelMenu.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.Controls.Add(btn_drch);
            panel4.Controls.Add(btn_izq);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 454);
            panel4.Name = "panel4";
            panel4.Size = new Size(779, 51);
            panel4.TabIndex = 3;
            // 
            // Eleccion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(779, 505);
            Controls.Add(PanelParent);
            Controls.Add(panel4);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Eleccion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Actividades del Mes";
            WindowState = FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            PanelParent.ResumeLayout(false);
            PanelFill.ResumeLayout(false);
            PanelMenu.ResumeLayout(false);
            PanelMenu.PerformLayout();
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button btn_izq;
        private Button btn_drch;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem añadirNiñoToolStripMenuItem;
        private ToolStripMenuItem actividadesApuntadasToolStripMenuItem;
        private ToolStripMenuItem usuarioToolStripMenuItem;
        private ToolStripMenuItem editarDatosPersonalesToolStripMenuItem;
        private ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private FlowLayoutPanel PanelBotones;
        private Panel PanelParent;
        private Panel PanelFill;
        private Panel panel4;
        private Panel PanelMenu;
    }
}