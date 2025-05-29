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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatosPersonalesNinos));
            btn_Aceptar = new Button();
            txEdad = new TextBox();
            label4 = new Label();
            txFnac = new TextBox();
            label3 = new Label();
            txApellidos = new TextBox();
            label1 = new Label();
            txDNI = new TextBox();
            label2 = new Label();
            txUsuario = new TextBox();
            lblUsuario = new Label();
            dgvNinos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvNinos).BeginInit();
            SuspendLayout();
            // 
            // btn_Aceptar
            // 
            btn_Aceptar.Anchor = AnchorStyles.None;
            btn_Aceptar.BackColor = Color.DodgerBlue;
            btn_Aceptar.FlatAppearance.BorderSize = 0;
            btn_Aceptar.FlatStyle = FlatStyle.Flat;
            btn_Aceptar.ForeColor = Color.White;
            btn_Aceptar.Location = new Point(107, 303);
            btn_Aceptar.Name = "btn_Aceptar";
            btn_Aceptar.Size = new Size(75, 23);
            btn_Aceptar.TabIndex = 37;
            btn_Aceptar.Text = "Aceptar";
            btn_Aceptar.UseVisualStyleBackColor = false;
            btn_Aceptar.Click += button2_Click;
            // 
            // txEdad
            // 
            txEdad.Anchor = AnchorStyles.None;
            txEdad.Location = new Point(11, 258);
            txEdad.Name = "txEdad";
            txEdad.Size = new Size(266, 23);
            txEdad.TabIndex = 36;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Location = new Point(11, 240);
            label4.Name = "label4";
            label4.Size = new Size(33, 15);
            label4.TabIndex = 35;
            label4.Text = "Edad";
            // 
            // txFnac
            // 
            txFnac.Anchor = AnchorStyles.None;
            txFnac.Location = new Point(11, 206);
            txFnac.Name = "txFnac";
            txFnac.Size = new Size(266, 23);
            txFnac.TabIndex = 34;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(11, 188);
            label3.Name = "label3";
            label3.Size = new Size(117, 15);
            label3.TabIndex = 33;
            label3.Text = "Fecha de nacimiento";
            // 
            // txApellidos
            // 
            txApellidos.Anchor = AnchorStyles.None;
            txApellidos.Location = new Point(12, 91);
            txApellidos.Name = "txApellidos";
            txApellidos.Size = new Size(266, 23);
            txApellidos.TabIndex = 32;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(12, 73);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 31;
            label1.Text = "Apellidos";
            // 
            // txDNI
            // 
            txDNI.Anchor = AnchorStyles.None;
            txDNI.Location = new Point(11, 150);
            txDNI.Name = "txDNI";
            txDNI.Size = new Size(266, 23);
            txDNI.TabIndex = 30;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(11, 127);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 29;
            label2.Text = "DNI";
            // 
            // txUsuario
            // 
            txUsuario.Anchor = AnchorStyles.None;
            txUsuario.Location = new Point(12, 47);
            txUsuario.Name = "txUsuario";
            txUsuario.Size = new Size(266, 23);
            txUsuario.TabIndex = 28;
            // 
            // lblUsuario
            // 
            lblUsuario.Anchor = AnchorStyles.None;
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(11, 29);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(51, 15);
            lblUsuario.TabIndex = 27;
            lblUsuario.Text = "Nombre";
            // 
            // dgvNinos
            // 
            dgvNinos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dgvNinos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNinos.Location = new Point(306, 31);
            dgvNinos.Name = "dgvNinos";
            dgvNinos.ReadOnly = true;
            dgvNinos.Size = new Size(482, 96);
            dgvNinos.TabIndex = 38;
            dgvNinos.CellClick += dgvNinos_CellClick;
            // 
            // DatosPersonalesNinos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.LightGray;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvNinos);
            Controls.Add(btn_Aceptar);
            Controls.Add(txEdad);
            Controls.Add(label4);
            Controls.Add(txFnac);
            Controls.Add(label3);
            Controls.Add(txApellidos);
            Controls.Add(label1);
            Controls.Add(txDNI);
            Controls.Add(label2);
            Controls.Add(txUsuario);
            Controls.Add(lblUsuario);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DatosPersonalesNinos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Datos Personales Niños";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvNinos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Button btn_Aceptar;
        private TextBox txEdad;
        private Label label4;
        private TextBox txFnac;
        private Label label3;
        private TextBox txApellidos;
        private Label label1;
        private TextBox txDNI;
        private Label label2;
        private TextBox txUsuario;
        private Label lblUsuario;
        private DataGridView dgvNinos;
    }
}