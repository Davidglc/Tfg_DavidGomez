namespace TFG_DavidGomez
{
    partial class InicioSesion
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblUsuario = new Label();
            lblContraseña = new Label();
            btnInicioSesion = new Button();
            txUsuario = new TextBox();
            TxContrasena = new TextBox();
            btnRegistrarse = new Button();
            lblBienvenia = new Label();
            SuspendLayout();
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(235, 91);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(47, 15);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuario";
            // 
            // lblContraseña
            // 
            lblContraseña.AutoSize = true;
            lblContraseña.Location = new Point(235, 197);
            lblContraseña.Name = "lblContraseña";
            lblContraseña.Size = new Size(67, 15);
            lblContraseña.TabIndex = 1;
            lblContraseña.Text = "Contraseña";
            // 
            // btnInicioSesion
            // 
            btnInicioSesion.BackColor = Color.Red;
            btnInicioSesion.ForeColor = Color.White;
            btnInicioSesion.Location = new Point(174, 306);
            btnInicioSesion.Name = "btnInicioSesion";
            btnInicioSesion.Size = new Size(128, 23);
            btnInicioSesion.TabIndex = 2;
            btnInicioSesion.Text = "Iniciar Sesion";
            btnInicioSesion.UseVisualStyleBackColor = false;
            btnInicioSesion.Click += btnInicioSesion_Click;
            // 
            // txUsuario
            // 
            txUsuario.Location = new Point(235, 136);
            txUsuario.Name = "txUsuario";
            txUsuario.Size = new Size(304, 23);
            txUsuario.TabIndex = 3;
            // 
            // TxContrasena
            // 
            TxContrasena.Location = new Point(235, 235);
            TxContrasena.Name = "TxContrasena";
            TxContrasena.Size = new Size(304, 23);
            TxContrasena.TabIndex = 4;
            // 
            // btnRegistrarse
            // 
            btnRegistrarse.BackColor = Color.Red;
            btnRegistrarse.ForeColor = Color.White;
            btnRegistrarse.Location = new Point(484, 306);
            btnRegistrarse.Name = "btnRegistrarse";
            btnRegistrarse.Size = new Size(128, 23);
            btnRegistrarse.TabIndex = 5;
            btnRegistrarse.Text = "Registrarse";
            btnRegistrarse.UseVisualStyleBackColor = false;
            btnRegistrarse.Click += btnRegistrarse_Click;
            // 
            // lblBienvenia
            // 
            lblBienvenia.AutoSize = true;
            lblBienvenia.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBienvenia.Location = new Point(319, 31);
            lblBienvenia.Name = "lblBienvenia";
            lblBienvenia.Size = new Size(127, 25);
            lblBienvenia.TabIndex = 6;
            lblBienvenia.Text = "BIENVENIDO";
            // 
            // InicioSesion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(800, 450);
            Controls.Add(lblBienvenia);
            Controls.Add(btnRegistrarse);
            Controls.Add(TxContrasena);
            Controls.Add(txUsuario);
            Controls.Add(btnInicioSesion);
            Controls.Add(lblContraseña);
            Controls.Add(lblUsuario);
            Name = "InicioSesion";
            Text = "Inicio Sesion";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUsuario;
        private Label lblContraseña;
        private Button btnInicioSesion;
        private TextBox txUsuario;
        private TextBox TxContrasena;
        private Button btnRegistrarse;
        private Label lblBienvenia;
    }
}
