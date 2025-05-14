namespace TFG_DavidGomez.Sesion
{
    partial class Registrarse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Registrarse));
            TxContrasena = new TextBox();
            txUsuario = new TextBox();
            lblContraseña = new Label();
            lblUsuario = new Label();
            txDNI = new TextBox();
            label2 = new Label();
            txTelf = new TextBox();
            label3 = new Label();
            txCorreo = new TextBox();
            label4 = new Label();
            btnGuardar = new Button();
            txApellidos = new TextBox();
            label1 = new Label();
            BtnGuardarMoni = new Button();
            TxDirec = new TextBox();
            TxDireccion = new Label();
            SuspendLayout();
            // 
            // TxContrasena
            // 
            TxContrasena.Anchor = AnchorStyles.None;
            TxContrasena.Font = new Font("Segoe UI", 9.75F);
            TxContrasena.Location = new Point(66, 142);
            TxContrasena.Name = "TxContrasena";
            TxContrasena.PasswordChar = '*';
            TxContrasena.Size = new Size(304, 25);
            TxContrasena.TabIndex = 9;
            // 
            // txUsuario
            // 
            txUsuario.Anchor = AnchorStyles.None;
            txUsuario.Font = new Font("Segoe UI", 9.75F);
            txUsuario.Location = new Point(66, 47);
            txUsuario.Name = "txUsuario";
            txUsuario.Size = new Size(304, 25);
            txUsuario.TabIndex = 7;
            // 
            // lblContraseña
            // 
            lblContraseña.Anchor = AnchorStyles.None;
            lblContraseña.AutoSize = true;
            lblContraseña.Location = new Point(66, 124);
            lblContraseña.Name = "lblContraseña";
            lblContraseña.Size = new Size(67, 15);
            lblContraseña.TabIndex = 6;
            lblContraseña.Text = "Contraseña";
            // 
            // lblUsuario
            // 
            lblUsuario.Anchor = AnchorStyles.None;
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(66, 29);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(51, 15);
            lblUsuario.TabIndex = 5;
            lblUsuario.Text = "Nombre";
            // 
            // txDNI
            // 
            txDNI.Anchor = AnchorStyles.None;
            txDNI.Font = new Font("Segoe UI", 9.75F);
            txDNI.Location = new Point(66, 190);
            txDNI.MaxLength = 9;
            txDNI.Name = "txDNI";
            txDNI.Size = new Size(304, 25);
            txDNI.TabIndex = 12;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(66, 172);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 11;
            label2.Text = "DNI";
            // 
            // txTelf
            // 
            txTelf.Anchor = AnchorStyles.None;
            txTelf.Font = new Font("Segoe UI", 9.75F);
            txTelf.Location = new Point(66, 243);
            txTelf.MaxLength = 9;
            txTelf.Name = "txTelf";
            txTelf.Size = new Size(304, 25);
            txTelf.TabIndex = 14;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(66, 225);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 13;
            label3.Text = "Teléfono";
            // 
            // txCorreo
            // 
            txCorreo.Anchor = AnchorStyles.None;
            txCorreo.Font = new Font("Segoe UI", 9.75F);
            txCorreo.Location = new Point(66, 294);
            txCorreo.Name = "txCorreo";
            txCorreo.Size = new Size(304, 25);
            txCorreo.TabIndex = 16;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Location = new Point(66, 276);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 15;
            label4.Text = "Correo";
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.None;
            btnGuardar.BackColor = Color.DodgerBlue;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(143, 374);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(125, 55);
            btnGuardar.TabIndex = 18;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txApellidos
            // 
            txApellidos.Anchor = AnchorStyles.None;
            txApellidos.Font = new Font("Segoe UI", 9.75F);
            txApellidos.Location = new Point(66, 91);
            txApellidos.Name = "txApellidos";
            txApellidos.Size = new Size(304, 25);
            txApellidos.TabIndex = 8;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(66, 73);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 19;
            label1.Text = "Apellidos";
            // 
            // BtnGuardarMoni
            // 
            BtnGuardarMoni.Anchor = AnchorStyles.None;
            BtnGuardarMoni.BackColor = Color.DodgerBlue;
            BtnGuardarMoni.FlatAppearance.BorderSize = 0;
            BtnGuardarMoni.FlatStyle = FlatStyle.Flat;
            BtnGuardarMoni.ForeColor = Color.White;
            BtnGuardarMoni.Location = new Point(143, 389);
            BtnGuardarMoni.Name = "BtnGuardarMoni";
            BtnGuardarMoni.Size = new Size(125, 55);
            BtnGuardarMoni.TabIndex = 21;
            BtnGuardarMoni.Text = "Guardar";
            BtnGuardarMoni.UseVisualStyleBackColor = false;
            BtnGuardarMoni.Click += BtnGuardarMoni_Click;
            // 
            // TxDirec
            // 
            TxDirec.Anchor = AnchorStyles.None;
            TxDirec.Font = new Font("Segoe UI", 9.75F);
            TxDirec.Location = new Point(66, 339);
            TxDirec.Name = "TxDirec";
            TxDirec.Size = new Size(304, 25);
            TxDirec.TabIndex = 17;
            // 
            // TxDireccion
            // 
            TxDireccion.Anchor = AnchorStyles.None;
            TxDireccion.AutoSize = true;
            TxDireccion.Location = new Point(66, 321);
            TxDireccion.Name = "TxDireccion";
            TxDireccion.Size = new Size(57, 15);
            TxDireccion.TabIndex = 22;
            TxDireccion.Text = "Dirección";
            // 
            // Registrarse
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.LightGray;
            ClientSize = new Size(466, 450);
            Controls.Add(TxDirec);
            Controls.Add(TxDireccion);
            Controls.Add(BtnGuardarMoni);
            Controls.Add(txApellidos);
            Controls.Add(label1);
            Controls.Add(btnGuardar);
            Controls.Add(txCorreo);
            Controls.Add(label4);
            Controls.Add(txTelf);
            Controls.Add(label3);
            Controls.Add(txDNI);
            Controls.Add(label2);
            Controls.Add(TxContrasena);
            Controls.Add(txUsuario);
            Controls.Add(lblContraseña);
            Controls.Add(lblUsuario);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Registrarse";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registrarse";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TxContrasena;
        private TextBox txUsuario;
        private Label lblContraseña;
        private Label lblUsuario;
        private TextBox txDNI;
        private Label label2;
        private TextBox txTelf;
        private Label label3;
        private TextBox txCorreo;
        private Label label4;
        public Button btnGuardar;
        private TextBox txApellidos;
        private Label label1;
        public Button BtnGuardarMoni;
        private TextBox TxDirec;
        private Label TxDireccion;
    }
}