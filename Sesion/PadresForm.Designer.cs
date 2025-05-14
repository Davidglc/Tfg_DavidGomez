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
            actividadesApuntadasToolStripMenuItem = new ToolStripMenuItem();
            usuarioToolStripMenuItem = new ToolStripMenuItem();
            editarDatosPersonalesToolStripMenuItem = new ToolStripMenuItem();
            cerrarSesiónToolStripMenuItem = new ToolStripMenuItem();
            btn_DA = new Button();
            dataGridInscripciones = new DataGridView();
            btnApuntar = new Button();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridInscripciones).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { añadirNiñoToolStripMenuItem, actividadesApuntadasToolStripMenuItem, usuarioToolStripMenuItem });
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
            // 
            // actividadesApuntadasToolStripMenuItem
            // 
            actividadesApuntadasToolStripMenuItem.Name = "actividadesApuntadasToolStripMenuItem";
            actividadesApuntadasToolStripMenuItem.Size = new Size(140, 20);
            actividadesApuntadasToolStripMenuItem.Text = "Actividades Apuntadas";
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
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            cerrarSesiónToolStripMenuItem.Size = new Size(196, 22);
            cerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión";
            cerrarSesiónToolStripMenuItem.Click += cerrarSesiónToolStripMenuItem_Click;
            // 
            // btn_DA
            // 
            btn_DA.Anchor = AnchorStyles.None;
            btn_DA.BackColor = Color.DodgerBlue;
            btn_DA.FlatAppearance.BorderSize = 0;
            btn_DA.FlatStyle = FlatStyle.Flat;
            btn_DA.ForeColor = Color.White;
            btn_DA.Location = new Point(333, 386);
            btn_DA.Name = "btn_DA";
            btn_DA.Size = new Size(82, 52);
            btn_DA.TabIndex = 3;
            btn_DA.Text = "DesApuntar";
            btn_DA.UseVisualStyleBackColor = false;
            btn_DA.Click += btn_DA_Click;
            // 
            // dataGridInscripciones
            // 
            dataGridInscripciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridInscripciones.Location = new Point(12, 43);
            dataGridInscripciones.Name = "dataGridInscripciones";
            dataGridInscripciones.Size = new Size(776, 331);
            dataGridInscripciones.TabIndex = 4;
            // 
            // btnApuntar
            // 
            btnApuntar.Anchor = AnchorStyles.None;
            btnApuntar.BackColor = Color.DodgerBlue;
            btnApuntar.FlatAppearance.BorderSize = 0;
            btnApuntar.FlatStyle = FlatStyle.Flat;
            btnApuntar.ForeColor = Color.White;
            btnApuntar.Location = new Point(165, 386);
            btnApuntar.Name = "btnApuntar";
            btnApuntar.Size = new Size(82, 52);
            btnApuntar.TabIndex = 2;
            btnApuntar.Text = "Apuntar";
            btnApuntar.UseVisualStyleBackColor = false;
            // 
            // PadresForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.LightGray;
            ClientSize = new Size(800, 450);
            Controls.Add(btnApuntar);
            Controls.Add(dataGridInscripciones);
            Controls.Add(btn_DA);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "PadresForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PadresForm";
            WindowState = FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridInscripciones).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem añadirNiñoToolStripMenuItem;
        private Button btn_DA;
        private ToolStripMenuItem usuarioToolStripMenuItem;
        private ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private ToolStripMenuItem editarDatosPersonalesToolStripMenuItem;
        private ToolStripMenuItem actividadesApuntadasToolStripMenuItem;
        private DataGridView dataGridInscripciones;
        private Button btnApuntar;
    }
}