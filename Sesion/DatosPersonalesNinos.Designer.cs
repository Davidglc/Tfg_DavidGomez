namespace TFG_DavidGomez.Sesion
{
    partial class DatosPersonalesNinos
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatosPersonalesNinos));
            btn_Aceptar = new Button();
            txFnac = new TextBox();
            label3 = new Label();
            txApellidos = new TextBox();
            label1 = new Label();
            txDNI = new TextBox();
            label2 = new Label();
            txUsuario = new TextBox();
            lblUsuario = new Label();
            dgvNinos = new DataGridView();
            PanelParent = new Panel();
            PanelDbg = new Panel();
            PanelResto = new Panel();
            bindingSource1 = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dgvNinos).BeginInit();
            PanelParent.SuspendLayout();
            PanelDbg.SuspendLayout();
            PanelResto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // btn_Aceptar
            // 
            btn_Aceptar.Anchor = AnchorStyles.None;
            btn_Aceptar.BackColor = Color.DodgerBlue;
            btn_Aceptar.FlatAppearance.BorderSize = 0;
            btn_Aceptar.FlatStyle = FlatStyle.Flat;
            btn_Aceptar.ForeColor = Color.White;
            btn_Aceptar.Location = new Point(338, 256);
            btn_Aceptar.Name = "btn_Aceptar";
            btn_Aceptar.Size = new Size(75, 23);
            btn_Aceptar.TabIndex = 37;
            btn_Aceptar.Text = "Aceptar";
            btn_Aceptar.UseVisualStyleBackColor = false;
            btn_Aceptar.Click += button2_Click;
            // 
            // txFnac
            // 
            txFnac.Anchor = AnchorStyles.None;
            txFnac.Location = new Point(261, 206);
            txFnac.Name = "txFnac";
            txFnac.Size = new Size(266, 23);
            txFnac.TabIndex = 34;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(261, 188);
            label3.Name = "label3";
            label3.Size = new Size(117, 15);
            label3.TabIndex = 33;
            label3.Text = "Fecha de nacimiento";
            // 
            // txApellidos
            // 
            txApellidos.Anchor = AnchorStyles.None;
            txApellidos.Location = new Point(262, 96);
            txApellidos.Name = "txApellidos";
            txApellidos.Size = new Size(266, 23);
            txApellidos.TabIndex = 32;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(262, 78);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 31;
            label1.Text = "Apellidos";
            // 
            // txDNI
            // 
            txDNI.Anchor = AnchorStyles.None;
            txDNI.Location = new Point(261, 150);
            txDNI.Name = "txDNI";
            txDNI.Size = new Size(266, 23);
            txDNI.TabIndex = 30;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(261, 127);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 29;
            label2.Text = "DNI";
            // 
            // txUsuario
            // 
            txUsuario.Anchor = AnchorStyles.None;
            txUsuario.Location = new Point(262, 49);
            txUsuario.Name = "txUsuario";
            txUsuario.Size = new Size(266, 23);
            txUsuario.TabIndex = 28;
            // 
            // lblUsuario
            // 
            lblUsuario.Anchor = AnchorStyles.None;
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(261, 31);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(51, 15);
            lblUsuario.TabIndex = 27;
            lblUsuario.Text = "Nombre";
            // 
            // dgvNinos
            // 
            dgvNinos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNinos.Location = new Point(0, 0);
            dgvNinos.Name = "dgvNinos";
            dgvNinos.ReadOnly = true;
            dgvNinos.Size = new Size(0, 450);
            dgvNinos.TabIndex = 38;
            dgvNinos.CellClick += dgvNinos_CellClick;
            // 
            // PanelParent
            // 
            PanelParent.Controls.Add(PanelDbg);
            PanelParent.Controls.Add(PanelResto);
            PanelParent.Dock = DockStyle.Fill;
            PanelParent.Location = new Point(0, 0);
            PanelParent.Name = "PanelParent";
            PanelParent.Size = new Size(800, 450);
            PanelParent.TabIndex = 39;
            // 
            // PanelDbg
            // 
            PanelDbg.Controls.Add(dgvNinos);
            PanelDbg.Dock = DockStyle.Fill;
            PanelDbg.Location = new Point(0, 0);
            PanelDbg.Name = "PanelDbg";
            PanelDbg.Size = new Size(0, 450);
            PanelDbg.TabIndex = 41;
            // 
            // PanelResto
            // 
            PanelResto.Controls.Add(lblUsuario);
            PanelResto.Controls.Add(txUsuario);
            PanelResto.Controls.Add(label3);
            PanelResto.Controls.Add(txApellidos);
            PanelResto.Controls.Add(label1);
            PanelResto.Controls.Add(txDNI);
            PanelResto.Controls.Add(label2);
            PanelResto.Controls.Add(txFnac);
            PanelResto.Controls.Add(btn_Aceptar);
            PanelResto.Dock = DockStyle.Right;
            PanelResto.Location = new Point(0, 0);
            PanelResto.Name = "PanelResto";
            PanelResto.Size = new Size(800, 450);
            PanelResto.TabIndex = 40;
            // 
            // DatosPersonalesNinos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.LightGray;
            ClientSize = new Size(800, 450);
            Controls.Add(PanelParent);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DatosPersonalesNinos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Datos Personales Niños";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvNinos).EndInit();
            PanelParent.ResumeLayout(false);
            PanelDbg.ResumeLayout(false);
            PanelResto.ResumeLayout(false);
            PanelResto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
        }



        #endregion

        private Button btn_Aceptar;
        private TextBox txFnac;
        private Label label3;
        private TextBox txApellidos;
        private Label label1;
        private TextBox txDNI;
        private Label label2;
        private TextBox txUsuario;
        private Label lblUsuario;
        private DataGridView dgvNinos;
        private Panel PanelParent;
        private Panel PanelResto;
        private Panel PanelDbg;
        private BindingSource bindingSource1;
    }
}