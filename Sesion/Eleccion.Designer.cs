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
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btn_izq
            // 
            btn_izq.Anchor = AnchorStyles.None;
            btn_izq.BackColor = Color.DodgerBlue;
            btn_izq.FlatAppearance.BorderSize = 0;
            btn_izq.FlatStyle = FlatStyle.Flat;
            btn_izq.ForeColor = Color.White;
            btn_izq.Location = new Point(85, 448);
            btn_izq.Name = "btn_izq";
            btn_izq.Size = new Size(75, 23);
            btn_izq.TabIndex = 1;
            btn_izq.Text = "<----";
            btn_izq.UseVisualStyleBackColor = false;
            btn_izq.Click += btn_izq_Click;
            // 
            // btn_drch
            // 
            btn_drch.Anchor = AnchorStyles.None;
            btn_drch.BackColor = Color.DodgerBlue;
            btn_drch.FlatAppearance.BorderSize = 0;
            btn_drch.FlatStyle = FlatStyle.Flat;
            btn_drch.ForeColor = Color.White;
            btn_drch.Location = new Point(429, 448);
            btn_drch.Name = "btn_drch";
            btn_drch.Size = new Size(75, 23);
            btn_drch.TabIndex = 2;
            btn_drch.Text = "---->";
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
            // PanelBotones
            // 
            PanelBotones.Anchor = AnchorStyles.None;
            PanelBotones.Location = new Point(20, 37);
            PanelBotones.Name = "PanelBotones";
            PanelBotones.Size = new Size(731, 371);
            PanelBotones.TabIndex = 4;
            // 
            // Eleccion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(779, 505);
            Controls.Add(PanelBotones);
            Controls.Add(menuStrip1);
            Controls.Add(btn_drch);
            Controls.Add(btn_izq);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Eleccion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Actividades del Mes";
            WindowState = FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
    }
}