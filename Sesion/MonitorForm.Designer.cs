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
            menuStrip1 = new MenuStrip();
            actividadesToolStripMenuItem = new ToolStripMenuItem();
            añadirMonitorToolStripMenuItem = new ToolStripMenuItem();
            usuarioToolStripMenuItem = new ToolStripMenuItem();
            datosPersonalesToolStripMenuItem = new ToolStripMenuItem();
            cerrarSesionToolStripMenuItem = new ToolStripMenuItem();
            lbFecha = new Label();
            LbFecha2 = new Label();
            LAtividad = new Label();
            LbAtividad2 = new Label();
            LMateriales = new Label();
            LbMateriales = new ListBox();
            LbNiños = new ListBox();
            label1 = new Label();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { actividadesToolStripMenuItem, añadirMonitorToolStripMenuItem, usuarioToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // actividadesToolStripMenuItem
            // 
            actividadesToolStripMenuItem.Name = "actividadesToolStripMenuItem";
            actividadesToolStripMenuItem.Size = new Size(80, 20);
            actividadesToolStripMenuItem.Text = "Actividades";
            // 
            // añadirMonitorToolStripMenuItem
            // 
            añadirMonitorToolStripMenuItem.Name = "añadirMonitorToolStripMenuItem";
            añadirMonitorToolStripMenuItem.Size = new Size(100, 20);
            añadirMonitorToolStripMenuItem.Text = "Añadir Monitor";
            añadirMonitorToolStripMenuItem.Click += añadirMonitorToolStripMenuItem_Click;
            // 
            // usuarioToolStripMenuItem
            // 
            usuarioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { datosPersonalesToolStripMenuItem, cerrarSesionToolStripMenuItem });
            usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            usuarioToolStripMenuItem.Size = new Size(59, 20);
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
            lbFecha.AutoSize = true;
            lbFecha.Location = new Point(20, 41);
            lbFecha.Name = "lbFecha";
            lbFecha.Size = new Size(41, 15);
            lbFecha.TabIndex = 1;
            lbFecha.Text = "Fecha:";
            // 
            // LbFecha2
            // 
            LbFecha2.AutoSize = true;
            LbFecha2.Location = new Point(67, 41);
            LbFecha2.Name = "LbFecha2";
            LbFecha2.Size = new Size(65, 15);
            LbFecha2.TabIndex = 2;
            LbFecha2.Text = "13/11/2024";
            // 
            // LAtividad
            // 
            LAtividad.AutoSize = true;
            LAtividad.Location = new Point(249, 41);
            LAtividad.Name = "LAtividad";
            LAtividad.Size = new Size(60, 15);
            LAtividad.TabIndex = 3;
            LAtividad.Text = "Actividad:";
            // 
            // LbAtividad2
            // 
            LbAtividad2.AutoSize = true;
            LbAtividad2.Location = new Point(315, 41);
            LbAtividad2.Name = "LbAtividad2";
            LbAtividad2.Size = new Size(69, 15);
            LbAtividad2.TabIndex = 4;
            LbAtividad2.Text = "Papiroflexia";
            // 
            // LMateriales
            // 
            LMateriales.AutoSize = true;
            LMateriales.Location = new Point(20, 72);
            LMateriales.Name = "LMateriales";
            LMateriales.Size = new Size(64, 15);
            LMateriales.TabIndex = 5;
            LMateriales.Text = "Materiales:";
            // 
            // LbMateriales
            // 
            LbMateriales.FormattingEnabled = true;
            LbMateriales.ItemHeight = 15;
            LbMateriales.Location = new Point(20, 90);
            LbMateriales.Name = "LbMateriales";
            LbMateriales.Size = new Size(364, 214);
            LbMateriales.TabIndex = 6;
            // 
            // LbNiños
            // 
            LbNiños.FormattingEnabled = true;
            LbNiños.ItemHeight = 15;
            LbNiños.Location = new Point(424, 90);
            LbNiños.Name = "LbNiños";
            LbNiños.Size = new Size(364, 214);
            LbNiños.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(424, 72);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 7;
            label1.Text = "Niños:";
            // 
            // MonitorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LbNiños);
            Controls.Add(label1);
            Controls.Add(LbMateriales);
            Controls.Add(LMateriales);
            Controls.Add(LbAtividad2);
            Controls.Add(LAtividad);
            Controls.Add(LbFecha2);
            Controls.Add(lbFecha);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MonitorForm";
            Text = "MonitorForm";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem actividadesToolStripMenuItem;
        private ToolStripMenuItem añadirMonitorToolStripMenuItem;
        private Label lbFecha;
        private Label LbFecha2;
        private Label LAtividad;
        private Label LbAtividad2;
        private Label LMateriales;
        private ListBox LbMateriales;
        private ListBox LbNiños;
        private Label label1;
        private ToolStripMenuItem usuarioToolStripMenuItem;
        private ToolStripMenuItem datosPersonalesToolStripMenuItem;
        private ToolStripMenuItem cerrarSesionToolStripMenuItem;
    }
}