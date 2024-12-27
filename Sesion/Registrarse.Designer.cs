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
            SuspendLayout();
            // 
            // TxContrasena
            // 
            TxContrasena.Location = new Point(66, 142);
            TxContrasena.Name = "TxContrasena";
            TxContrasena.Size = new Size(304, 23);
            TxContrasena.TabIndex = 8;
            // 
            // txUsuario
            // 
            txUsuario.Location = new Point(66, 47);
            txUsuario.Name = "txUsuario";
            txUsuario.Size = new Size(304, 23);
            txUsuario.TabIndex = 7;
            // 
            // lblContraseña
            // 
            lblContraseña.AutoSize = true;
            lblContraseña.Location = new Point(66, 124);
            lblContraseña.Name = "lblContraseña";
            lblContraseña.Size = new Size(67, 15);
            lblContraseña.TabIndex = 6;
            lblContraseña.Text = "Contraseña";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(66, 29);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(51, 15);
            lblUsuario.TabIndex = 5;
            lblUsuario.Text = "Nombre";
            // 
            // txDNI
            // 
            txDNI.Location = new Point(66, 190);
            txDNI.Name = "txDNI";
            txDNI.Size = new Size(304, 23);
            txDNI.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(66, 172);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 11;
            label2.Text = "DNI";
            // 
            // txTelf
            // 
            txTelf.Location = new Point(66, 243);
            txTelf.Name = "txTelf";
            txTelf.Size = new Size(304, 23);
            txTelf.TabIndex = 14;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(66, 225);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 13;
            label3.Text = "Teléfono";
            // 
            // txCorreo
            // 
            txCorreo.Location = new Point(66, 294);
            txCorreo.Name = "txCorreo";
            txCorreo.Size = new Size(304, 23);
            txCorreo.TabIndex = 16;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(66, 276);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 15;
            label4.Text = "Correo";
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.Red;
            btnGuardar.Location = new Point(142, 333);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(125, 55);
            btnGuardar.TabIndex = 18;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txApellidos
            // 
            txApellidos.Location = new Point(66, 91);
            txApellidos.Name = "txApellidos";
            txApellidos.Size = new Size(304, 23);
            txApellidos.TabIndex = 20;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(66, 73);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 19;
            label1.Text = "Apellidos";
            // 
            // Registrarse
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(466, 450);
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
            Name = "Registrarse";
            Text = "Registrarse";
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
        private Button btnGuardar;
        private TextBox txApellidos;
        private Label label1;
    }
}