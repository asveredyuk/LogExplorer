namespace ClientApp
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btImport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbTimeFieldType = new System.Windows.Forms.ComboBox();
            this.cbTimeField = new System.Windows.Forms.ComboBox();
            this.cbGroupingFieldType = new System.Windows.Forms.ComboBox();
            this.cbGroupingField = new System.Windows.Forms.ComboBox();
            this.cbFiles = new System.Windows.Forms.ComboBox();
            this.btJobsRefresh = new System.Windows.Forms.Button();
            this.dgJobs = new System.Windows.Forms.DataGridView();
            this.IdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Progress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgJobs)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btImport);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbTimeFieldType);
            this.groupBox1.Controls.Add(this.cbTimeField);
            this.groupBox1.Controls.Add(this.cbGroupingFieldType);
            this.groupBox1.Controls.Add(this.cbGroupingField);
            this.groupBox1.Controls.Add(this.cbFiles);
            this.groupBox1.Location = new System.Drawing.Point(12, 393);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(537, 289);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New import job";
            // 
            // btImport
            // 
            this.btImport.Location = new System.Drawing.Point(6, 170);
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(163, 63);
            this.btImport.TabIndex = 2;
            this.btImport.Text = "Import";
            this.btImport.UseVisualStyleBackColor = true;
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Time field";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Grouping field";
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
            this.cbTimeFieldType.Location = new System.Drawing.Point(267, 127);
            this.cbTimeFieldType.Name = "cbTimeFieldType";
            this.cbTimeFieldType.Size = new System.Drawing.Size(121, 21);
            this.cbTimeFieldType.TabIndex = 0;
            // 
            // cbTimeField
            // 
            this.cbTimeField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTimeField.FormattingEnabled = true;
            this.cbTimeField.Location = new System.Drawing.Point(93, 127);
            this.cbTimeField.Name = "cbTimeField";
            this.cbTimeField.Size = new System.Drawing.Size(121, 21);
            this.cbTimeField.TabIndex = 0;
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
            this.cbGroupingFieldType.Location = new System.Drawing.Point(267, 87);
            this.cbGroupingFieldType.Name = "cbGroupingFieldType";
            this.cbGroupingFieldType.Size = new System.Drawing.Size(121, 21);
            this.cbGroupingFieldType.TabIndex = 0;
            // 
            // cbGroupingField
            // 
            this.cbGroupingField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroupingField.FormattingEnabled = true;
            this.cbGroupingField.Location = new System.Drawing.Point(93, 87);
            this.cbGroupingField.Name = "cbGroupingField";
            this.cbGroupingField.Size = new System.Drawing.Size(121, 21);
            this.cbGroupingField.TabIndex = 0;
            // 
            // cbFiles
            // 
            this.cbFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiles.FormattingEnabled = true;
            this.cbFiles.Location = new System.Drawing.Point(6, 19);
            this.cbFiles.Name = "cbFiles";
            this.cbFiles.Size = new System.Drawing.Size(121, 21);
            this.cbFiles.TabIndex = 0;
            this.cbFiles.SelectedIndexChanged += new System.EventHandler(this.cbFiles_SelectedIndexChanged);
            // 
            // btJobsRefresh
            // 
            this.btJobsRefresh.Location = new System.Drawing.Point(12, 12);
            this.btJobsRefresh.Name = "btJobsRefresh";
            this.btJobsRefresh.Size = new System.Drawing.Size(75, 23);
            this.btJobsRefresh.TabIndex = 1;
            this.btJobsRefresh.Text = "Refresh";
            this.btJobsRefresh.UseVisualStyleBackColor = true;
            this.btJobsRefresh.Click += new System.EventHandler(this.btJobsRefresh_Click);
            // 
            // dgJobs
            // 
            this.dgJobs.AllowUserToAddRows = false;
            this.dgJobs.AllowUserToDeleteRows = false;
            this.dgJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgJobs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdColumn,
            this.State,
            this.Progress,
            this.Result,
            this.Delete});
            this.dgJobs.Location = new System.Drawing.Point(12, 41);
            this.dgJobs.Name = "dgJobs";
            this.dgJobs.ReadOnly = true;
            this.dgJobs.Size = new System.Drawing.Size(1319, 281);
            this.dgJobs.TabIndex = 0;
            this.dgJobs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgJobs_CellContentClick);
            // 
            // IdColumn
            // 
            this.IdColumn.HeaderText = "Id";
            this.IdColumn.Name = "IdColumn";
            this.IdColumn.ReadOnly = true;
            // 
            // State
            // 
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            // 
            // Progress
            // 
            this.Progress.HeaderText = "Progress";
            this.Progress.Name = "Progress";
            this.Progress.ReadOnly = true;
            this.Progress.Width = 200;
            // 
            // Result
            // 
            this.Result.HeaderText = "Result";
            this.Result.Name = "Result";
            this.Result.ReadOnly = true;
            this.Result.Width = 200;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Text = "Delete";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 694);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btJobsRefresh);
            this.Controls.Add(this.dgJobs);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgJobs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgJobs;
        private System.Windows.Forms.Button btJobsRefresh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbFiles;
        private System.Windows.Forms.ComboBox cbGroupingField;
        private System.Windows.Forms.Button btImport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTimeField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbGroupingFieldType;
        private System.Windows.Forms.ComboBox cbTimeFieldType;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Progress;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
    }
}

