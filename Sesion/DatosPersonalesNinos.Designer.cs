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
            button2 = new Button();
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
            LbNinos = new ListBox();
            SuspendLayout();
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.Location = new Point(107, 303);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 37;
            button2.Text = "Aceptar";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // txEdad
            // 
            txEdad.Location = new Point(11, 258);
            txEdad.Name = "txEdad";
            txEdad.Size = new Size(266, 23);
            txEdad.TabIndex = 36;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 240);
            label4.Name = "label4";
            label4.Size = new Size(33, 15);
            label4.TabIndex = 35;
            label4.Text = "Edad";
            // 
            // txFnac
            // 
            txFnac.Location = new Point(11, 206);
            txFnac.Name = "txFnac";
            txFnac.Size = new Size(266, 23);
            txFnac.TabIndex = 34;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 188);
            label3.Name = "label3";
            label3.Size = new Size(117, 15);
            label3.TabIndex = 33;
            label3.Text = "Fecha de nacimiento";
            // 
            // txApellidos
            // 
            txApellidos.Location = new Point(12, 107);
            txApellidos.Name = "txApellidos";
            txApellidos.Size = new Size(266, 23);
            txApellidos.TabIndex = 32;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 133);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 31;
            label1.Text = "Apellidos";
            // 
            // txDNI
            // 
            txDNI.Location = new Point(12, 151);
            txDNI.Name = "txDNI";
            txDNI.Size = new Size(266, 23);
            txDNI.TabIndex = 30;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 80);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 29;
            label2.Text = "DNI";
            // 
            // txUsuario
            // 
            txUsuario.Location = new Point(11, 47);
            txUsuario.Name = "txUsuario";
            txUsuario.Size = new Size(266, 23);
            txUsuario.TabIndex = 28;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(11, 29);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(51, 15);
            lblUsuario.TabIndex = 27;
            lblUsuario.Text = "Nombre";
            // 
            // LbNinos
            // 
            LbNinos.FormattingEnabled = true;
            LbNinos.ItemHeight = 15;
            LbNinos.Location = new Point(295, 29);
            LbNinos.Name = "LbNinos";
            LbNinos.Size = new Size(494, 274);
            LbNinos.TabIndex = 38;
            LbNinos.DoubleClick += LbNinos_DoubleClick;
            // 
            // DatosPersonalesNinos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(800, 450);
            Controls.Add(LbNinos);
            Controls.Add(button2);
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
            Text = "DatosPersonalesNinos";
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Button button2;
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
        public ListBox LbNinos;

    }
}