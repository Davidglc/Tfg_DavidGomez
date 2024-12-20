namespace TFG_DavidGomez.Sesion
{
    partial class DatosPersonales
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
            btnGuardar = new Button();
            txCorreo = new TextBox();
            label4 = new Label();
            txTelf = new TextBox();
            label3 = new Label();
            txDNI = new TextBox();
            label2 = new Label();
            txApellidos = new TextBox();
            label1 = new Label();
            TxContrasena = new TextBox();
            txUsuario = new TextBox();
            lblContraseña = new Label();
            lblUsuario = new Label();
            menuStrip1 = new MenuStrip();
            datosNiñosToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.Red;
            btnGuardar.Location = new Point(92, 345);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(125, 55);
            btnGuardar.TabIndex = 31;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // txCorreo
            // 
            txCorreo.Location = new Point(16, 306);
            txCorreo.Name = "txCorreo";
            txCorreo.Size = new Size(304, 23);
            txCorreo.TabIndex = 30;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 288);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 29;
            label4.Text = "Correo";
            // 
            // txTelf
            // 
            txTelf.Location = new Point(16, 255);
            txTelf.Name = "txTelf";
            txTelf.Size = new Size(304, 23);
            txTelf.TabIndex = 28;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 237);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 27;
            label3.Text = "Teléfono";
            // 
            // txDNI
            // 
            txDNI.Location = new Point(16, 110);
            txDNI.Name = "txDNI";
            txDNI.Size = new Size(304, 23);
            txDNI.TabIndex = 26;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 92);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 25;
            label2.Text = "DNI";
            // 
            // txApellidos
            // 
            txApellidos.Location = new Point(16, 204);
            txApellidos.Name = "txApellidos";
            txApellidos.Size = new Size(304, 23);
            txApellidos.TabIndex = 24;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 186);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 23;
            label1.Text = "Apellidos";
            // 
            // TxContrasena
            // 
            TxContrasena.Location = new Point(16, 154);
            TxContrasena.Name = "TxContrasena";
            TxContrasena.Size = new Size(304, 23);
            TxContrasena.TabIndex = 22;
            // 
            // txUsuario
            // 
            txUsuario.Location = new Point(16, 59);
            txUsuario.Name = "txUsuario";
            txUsuario.Size = new Size(304, 23);
            txUsuario.TabIndex = 21;
            // 
            // lblContraseña
            // 
            lblContraseña.AutoSize = true;
            lblContraseña.Location = new Point(16, 136);
            lblContraseña.Name = "lblContraseña";
            lblContraseña.Size = new Size(67, 15);
            lblContraseña.TabIndex = 20;
            lblContraseña.Text = "Contraseña";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(16, 41);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(51, 15);
            lblUsuario.TabIndex = 19;
            lblUsuario.Text = "Nombre";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { datosNiñosToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(372, 24);
            menuStrip1.TabIndex = 32;
            menuStrip1.Text = "menuStrip1";
            // 
            // datosNiñosToolStripMenuItem
            // 
            datosNiñosToolStripMenuItem.Name = "datosNiñosToolStripMenuItem";
            datosNiñosToolStripMenuItem.Size = new Size(83, 20);
            datosNiñosToolStripMenuItem.Text = "Datos Niños";
            datosNiñosToolStripMenuItem.Click += datosNiñosToolStripMenuItem_Click;
            // 
            // DatosPersonales
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(372, 450);
            Controls.Add(btnGuardar);
            Controls.Add(txCorreo);
            Controls.Add(label4);
            Controls.Add(txTelf);
            Controls.Add(label3);
            Controls.Add(txDNI);
            Controls.Add(label2);
            Controls.Add(txApellidos);
            Controls.Add(label1);
            Controls.Add(TxContrasena);
            Controls.Add(txUsuario);
            Controls.Add(lblContraseña);
            Controls.Add(lblUsuario);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "DatosPersonales";
            Text = "DatosPersonales";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGuardar;
        private TextBox txCorreo;
        private Label label4;
        private TextBox txTelf;
        private Label label3;
        private TextBox txDNI;
        private Label label2;
        private TextBox txApellidos;
        private Label label1;
        private TextBox TxContrasena;
        private TextBox txUsuario;
        private Label lblContraseña;
        private Label lblUsuario;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem datosNiñosToolStripMenuItem;
    }
}