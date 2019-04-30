namespace ClientApp.LogExplorer.LabelEditor
{
    partial class LabelGenerateForm
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
            this.comboBoxFieldName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btGroupSelected = new System.Windows.Forms.Button();
            this.tbProfileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btGenerate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxFieldName
            // 
            this.comboBoxFieldName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFieldName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFieldName.Enabled = false;
            this.comboBoxFieldName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxFieldName.FormattingEnabled = true;
            this.comboBoxFieldName.Location = new System.Drawing.Point(77, 9);
            this.comboBoxFieldName.Name = "comboBoxFieldName";
            this.comboBoxFieldName.Size = new System.Drawing.Size(505, 26);
            this.comboBoxFieldName.TabIndex = 0;
            this.comboBoxFieldName.SelectedIndexChanged += new System.EventHandler(this.comboBoxFieldName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select field";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(588, 9);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(451, 26);
            this.progressBar.TabIndex = 2;
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(206, 55);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(833, 494);
            this.dgv.TabIndex = 3;
            // 
            // btGroupSelected
            // 
            this.btGroupSelected.Location = new System.Drawing.Point(12, 55);
            this.btGroupSelected.Name = "btGroupSelected";
            this.btGroupSelected.Size = new System.Drawing.Size(188, 23);
            this.btGroupSelected.TabIndex = 4;
            this.btGroupSelected.Text = "Group selected";
            this.btGroupSelected.UseVisualStyleBackColor = true;
            this.btGroupSelected.Click += new System.EventHandler(this.btGroupSelected_Click);
            // 
            // tbProfileName
            // 
            this.tbProfileName.Location = new System.Drawing.Point(12, 101);
            this.tbProfileName.Name = "tbProfileName";
            this.tbProfileName.Size = new System.Drawing.Size(188, 20);
            this.tbProfileName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Profile name";
            // 
            // btGenerate
            // 
            this.btGenerate.Location = new System.Drawing.Point(12, 128);
            this.btGenerate.Name = "btGenerate";
            this.btGenerate.Size = new System.Drawing.Size(188, 53);
            this.btGenerate.TabIndex = 7;
            this.btGenerate.Text = "Generate";
            this.btGenerate.UseVisualStyleBackColor = true;
            this.btGenerate.Click += new System.EventHandler(this.btGenerate_Click);
            // 
            // LabelGenerateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 561);
            this.Controls.Add(this.btGenerate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbProfileName);
            this.Controls.Add(this.btGroupSelected);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxFieldName);
            this.Name = "LabelGenerateForm";
            this.Text = "LabelGenerateForm";
            this.Load += new System.EventHandler(this.LabelGenerateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxFieldName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btGroupSelected;
        private System.Windows.Forms.TextBox tbProfileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btGenerate;
    }
}