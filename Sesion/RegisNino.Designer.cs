namespace TFG_DavidGomez.Sesion
{
    partial class RegisNino
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisNino));
            txDNI = new TextBox();
            label2 = new Label();
            txUsuario = new TextBox();
            lblUsuario = new Label();
            txApellidos = new TextBox();
            label1 = new Label();
            txFnac = new TextBox();
            label3 = new Label();
            txEdad = new TextBox();
            label4 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // txDNI
            // 
            txDNI.Location = new Point(22, 96);
            txDNI.Name = "txDNI";
            txDNI.Size = new Size(304, 23);
            txDNI.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 78);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 17;
            label2.Text = "DNI";
            // 
            // txUsuario
            // 
            txUsuario.Location = new Point(22, 45);
            txUsuario.Name = "txUsuario";
            txUsuario.Size = new Size(304, 23);
            txUsuario.TabIndex = 15;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(22, 27);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(51, 15);
            lblUsuario.TabIndex = 13;
            lblUsuario.Text = "Nombre";
            // 
            // txApellidos
            // 
            txApellidos.Location = new Point(22, 149);
            txApellidos.Name = "txApellidos";
            txApellidos.Size = new Size(304, 23);
            txApellidos.TabIndex = 20;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 131);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 19;
            label1.Text = "Apellidos";
            // 
            // txFnac
            // 
            txFnac.Location = new Point(22, 204);
            txFnac.Name = "txFnac";
            txFnac.Size = new Size(304, 23);
            txFnac.TabIndex = 22;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 186);
            label3.Name = "label3";
            label3.Size = new Size(117, 15);
            label3.TabIndex = 21;
            label3.Text = "Fecha de nacimiento";
            // 
            // txEdad
            // 
            txEdad.Location = new Point(22, 256);
            txEdad.Name = "txEdad";
            txEdad.Size = new Size(304, 23);
            txEdad.TabIndex = 24;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 238);
            label4.Name = "label4";
            label4.Size = new Size(33, 15);
            label4.TabIndex = 23;
            label4.Text = "Edad";
            // 
            // button1
            // 
            button1.BackColor = Color.Red;
            button1.Location = new Point(118, 313);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 25;
            button1.Text = "Aceptar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.Location = new Point(118, 342);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 26;
            button2.Text = "Aceptar";
            button2.UseVisualStyleBackColor = false;
            button2.Visible = false;
            // 
            // RegisNino
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(364, 418);
            Controls.Add(button2);
            Controls.Add(button1);
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
            Name = "RegisNino";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RegisNino";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txDNI;
        private Label label2;
        private TextBox txUsuario;
        private Label lblUsuario;
        private TextBox txApellidos;
        private Label label1;
        private TextBox txFnac;
        private Label label3;
        private TextBox txEdad;
        private Label label4;
        private Button button1;
        private Button button2;
    }
}