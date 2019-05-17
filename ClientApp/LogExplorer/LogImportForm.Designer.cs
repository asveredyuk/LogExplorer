namespace ClientApp.LogExplorer
{
    partial class LogImportForm
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
            this.cbFiles = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDelimiter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbTimeFieldType = new System.Windows.Forms.ComboBox();
            this.cbTimeField = new System.Windows.Forms.ComboBox();
            this.cbGroupingFieldType = new System.Windows.Forms.ComboBox();
            this.cbGroupingField = new System.Windows.Forms.ComboBox();
            this.btImport = new System.Windows.Forms.Button();
            this.tbLogName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbFiles
            // 
            this.cbFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiles.FormattingEnabled = true;
            this.cbFiles.Location = new System.Drawing.Point(99, 51);
            this.cbFiles.Name = "cbFiles";
            this.cbFiles.Size = new System.Drawing.Size(295, 21);
            this.cbFiles.TabIndex = 0;
            this.cbFiles.SelectedIndexChanged += new System.EventHandler(this.cbFiles_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "File on server";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(255, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Delimiter";
            // 
            // tbDelimiter
            // 
            this.tbDelimiter.Location = new System.Drawing.Point(308, 181);
            this.tbDelimiter.MaxLength = 1;
            this.tbDelimiter.Name = "tbDelimiter";
            this.tbDelimiter.Size = new System.Drawing.Size(21, 20);
            this.tbDelimiter.TabIndex = 13;
            this.tbDelimiter.Text = ";";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Time field";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(236, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(236, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Grouping field";
            // 
            // cbTimeFieldType
            // 
            this.cbTimeFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTimeFieldType.FormattingEnabled = true;
            this.cbTimeFieldType.Items.AddRange(new object[] {
            "string",
            "int",
            "long",
            "double",
            "decimal",
            "datetime",
            "bool"});
            this.cbTimeFieldType.Location = new System.Drawing.Point(273, 138);
            this.cbTimeFieldType.Name = "cbTimeFieldType";
            this.cbTimeFieldType.Size = new System.Drawing.Size(121, 21);
            this.cbTimeFieldType.TabIndex = 5;
            // 
            // cbTimeField
            // 
            this.cbTimeField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTimeField.FormattingEnabled = true;
            this.cbTimeField.Location = new System.Drawing.Point(99, 138);
            this.cbTimeField.Name = "cbTimeField";
            this.cbTimeField.Size = new System.Drawing.Size(121, 21);
            this.cbTimeField.TabIndex = 6;
            // 
            // cbGroupingFieldType
            // 
            this.cbGroupingFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroupingFieldType.FormattingEnabled = true;
            this.cbGroupingFieldType.Items.AddRange(new object[] {
            "string",
            "int",
            "long",
            "double",
            "decimal",
            "datetime",
            "bool"});
            this.cbGroupingFieldType.Location = new System.Drawing.Point(273, 98);
            this.cbGroupingFieldType.Name = "cbGroupingFieldType";
            this.cbGroupingFieldType.Size = new System.Drawing.Size(121, 21);
            this.cbGroupingFieldType.TabIndex = 7;
            // 
            // cbGroupingField
            // 
            this.cbGroupingField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroupingField.FormattingEnabled = true;
            this.cbGroupingField.Location = new System.Drawing.Point(99, 98);
            this.cbGroupingField.Name = "cbGroupingField";
            this.cbGroupingField.Size = new System.Drawing.Size(121, 21);
            this.cbGroupingField.TabIndex = 8;
            // 
            // btImport
            // 
            this.btImport.Location = new System.Drawing.Point(273, 207);
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(121, 58);
            this.btImport.TabIndex = 15;
            this.btImport.Text = "Import";
            this.btImport.UseVisualStyleBackColor = true;
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // tbLogName
            // 
            this.tbLogName.Location = new System.Drawing.Point(99, 13);
            this.tbLogName.Name = "tbLogName";
            this.tbLogName.Size = new System.Drawing.Size(295, 20);
            this.tbLogName.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Log name";
            // 
            // LogImportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 454);
            this.Controls.Add(this.tbLogName);
            this.Controls.Add(this.btImport);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbDelimiter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbTimeFieldType);
            this.Controls.Add(this.cbTimeField);
            this.Controls.Add(this.cbGroupingFieldType);
            this.Controls.Add(this.cbGroupingField);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFiles);
            this.Name = "LogImportForm";
            this.Text = "LogImportDialog";
            this.Load += new System.EventHandler(this.LogImportDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDelimiter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbTimeFieldType;
        private System.Windows.Forms.ComboBox cbTimeField;
        private System.Windows.Forms.ComboBox cbGroupingFieldType;
        private System.Windows.Forms.ComboBox cbGroupingField;
        private System.Windows.Forms.Button btImport;
        private System.Windows.Forms.TextBox tbLogName;
        private System.Windows.Forms.Label label7;
    }
}