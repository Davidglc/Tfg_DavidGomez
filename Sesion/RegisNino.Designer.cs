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
            btn_Aceptar = new Button();
            label5 = new Label();
            PanelParent = new Panel();
            panel2 = new Panel();
            PanelParent.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // txDNI
            // 
            txDNI.Anchor = AnchorStyles.None;
            txDNI.Location = new Point(22, 96);
            txDNI.MaxLength = 9;
            txDNI.Name = "txDNI";
            txDNI.Size = new Size(304, 23);
            txDNI.TabIndex = 18;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(22, 78);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 17;
            label2.Text = "DNI";
            // 
            // txUsuario
            // 
            txUsuario.Anchor = AnchorStyles.None;
            txUsuario.Location = new Point(22, 45);
            txUsuario.Name = "txUsuario";
            txUsuario.Size = new Size(304, 23);
            txUsuario.TabIndex = 15;
            // 
            // lblUsuario
            // 
            lblUsuario.Anchor = AnchorStyles.None;
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(22, 27);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(51, 15);
            lblUsuario.TabIndex = 13;
            lblUsuario.Text = "Nombre";
            // 
            // txApellidos
            // 
            txApellidos.Anchor = AnchorStyles.None;
            txApellidos.Location = new Point(22, 149);
            txApellidos.Name = "txApellidos";
            txApellidos.Size = new Size(304, 23);
            txApellidos.TabIndex = 20;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(22, 131);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 19;
            label1.Text = "Apellidos";
            // 
            // txFnac
            // 
            txFnac.Anchor = AnchorStyles.None;
            txFnac.Location = new Point(22, 204);
            txFnac.Name = "txFnac";
            txFnac.Size = new Size(304, 23);
            txFnac.TabIndex = 22;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(22, 186);
            label3.Name = "label3";
            label3.Size = new Size(117, 15);
            label3.TabIndex = 21;
            label3.Text = "Fecha de nacimiento";
            // 
            // btn_Aceptar
            // 
            btn_Aceptar.Anchor = AnchorStyles.None;
            btn_Aceptar.BackColor = Color.DodgerBlue;
            btn_Aceptar.FlatAppearance.BorderSize = 0;
            btn_Aceptar.FlatStyle = FlatStyle.Flat;
            btn_Aceptar.ForeColor = Color.White;
            btn_Aceptar.Location = new Point(124, 253);
            btn_Aceptar.Name = "btn_Aceptar";
            btn_Aceptar.Size = new Size(75, 23);
            btn_Aceptar.TabIndex = 25;
            btn_Aceptar.Text = "Aceptar";
            btn_Aceptar.UseVisualStyleBackColor = false;
            btn_Aceptar.Click += button1_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Location = new Point(51, 78);
            label5.Name = "label5";
            label5.Size = new Size(264, 15);
            label5.TabIndex = 27;
            label5.Text = "(Si el niño no tiene DNI deja el campo en blanco)";
            // 
            // PanelParent
            // 
            PanelParent.Controls.Add(panel2);
            PanelParent.Dock = DockStyle.Fill;
            PanelParent.Location = new Point(0, 0);
            PanelParent.Name = "PanelParent";
            PanelParent.Size = new Size(364, 418);
            PanelParent.TabIndex = 28;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblUsuario);
            panel2.Controls.Add(txUsuario);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(txDNI);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(txApellidos);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(txFnac);
            panel2.Controls.Add(btn_Aceptar);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(364, 418);
            panel2.TabIndex = 29;
            // 
            // RegisNino
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.LightGray;
            ClientSize = new Size(364, 418);
            Controls.Add(PanelParent);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "RegisNino";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registrar Niño";
            WindowState = FormWindowState.Maximized;
            PanelParent.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
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
        private Button btn_Aceptar;
        private Label label5;
        private Panel PanelParent;
        private Panel panel2;
    }
}