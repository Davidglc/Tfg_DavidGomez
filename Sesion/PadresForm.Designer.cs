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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PadresForm));
            btn_DA = new Button();
            dataGridInscripciones = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridInscripciones).BeginInit();
            SuspendLayout();
            // 
            // btn_DA
            // 
            btn_DA.Anchor = AnchorStyles.None;
            btn_DA.BackColor = Color.DodgerBlue;
            btn_DA.FlatAppearance.BorderSize = 0;
            btn_DA.FlatStyle = FlatStyle.Flat;
            btn_DA.ForeColor = Color.White;
            btn_DA.Location = new Point(333, 386);
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
            dataGridInscripciones.Location = new Point(12, 43);
            dataGridInscripciones.Name = "dataGridInscripciones";
            dataGridInscripciones.Size = new Size(776, 331);
            dataGridInscripciones.TabIndex = 4;
            // 
            // PadresForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.LightGray;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridInscripciones);
            Controls.Add(btn_DA);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PadresForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Actividades Apuntadas";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dataGridInscripciones).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btn_DA;
        private DataGridView dataGridInscripciones;
    }
}