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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PadresForm));
            btn_DA = new Button();
            dataGridInscripciones = new DataGridView();
            PanelParent = new Panel();
            PanelCosas = new Panel();
            bindingSource1 = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dataGridInscripciones).BeginInit();
            PanelParent.SuspendLayout();
            PanelCosas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // btn_DA
            // 
            btn_DA.BackColor = Color.DodgerBlue;
            btn_DA.FlatAppearance.BorderSize = 0;
            btn_DA.FlatStyle = FlatStyle.Flat;
            btn_DA.ForeColor = Color.White;
            btn_DA.Location = new Point(328, 359);
            btn_DA.Name = "btn_DA";
            btn_DA.Size = new Size(82, 52);
            btn_DA.TabIndex = 3;
            btn_DA.Text = "DesApuntar";
            btn_DA.UseVisualStyleBackColor = false;
            btn_DA.Click += btn_DA_Click;
            // 
            // dataGridInscripciones
            // 
            dataGridInscripciones.Anchor = AnchorStyles.Top;
            dataGridInscripciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridInscripciones.Location = new Point(12, 12);
            dataGridInscripciones.Name = "dataGridInscripciones";
            dataGridInscripciones.ReadOnly = true;
            dataGridInscripciones.Size = new Size(776, 317);
            dataGridInscripciones.TabIndex = 4;
            // 
            // PanelParent
            // 
            PanelParent.Controls.Add(PanelCosas);
            PanelParent.Dock = DockStyle.Fill;
            PanelParent.Location = new Point(0, 0);
            PanelParent.Name = "PanelParent";
            PanelParent.Size = new Size(800, 450);
            PanelParent.TabIndex = 5;
            // 
            // PanelCosas
            // 
            PanelCosas.Controls.Add(dataGridInscripciones);
            PanelCosas.Controls.Add(btn_DA);
            PanelCosas.Dock = DockStyle.Fill;
            PanelCosas.Location = new Point(0, 0);
            PanelCosas.Name = "PanelCosas";
            PanelCosas.Size = new Size(800, 450);
            PanelCosas.TabIndex = 6;
            // 
            // PadresForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.LightGray;
            ClientSize = new Size(800, 450);
            Controls.Add(PanelParent);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PadresForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Actividades Apuntadas";
            ((System.ComponentModel.ISupportInitialize)dataGridInscripciones).EndInit();
            PanelParent.ResumeLayout(false);
            PanelCosas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btn_DA;
        private DataGridView dataGridInscripciones;
        private Panel PanelParent;
        private Panel PanelCosas;
        private BindingSource bindingSource1;
    }
}