namespace ClientApp.LogExplorer.ProcessMapEditor
{
    partial class CreateProcessMapBasicForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbMapName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxLabelField = new System.Windows.Forms.ComboBox();
            this.checkedListBoxValues = new System.Windows.Forms.CheckedListBox();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.btOpenMap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Process map name";
            // 
            // tbMapName
            // 
            this.tbMapName.Location = new System.Drawing.Point(116, 10);
            this.tbMapName.Name = "tbMapName";
            this.tbMapName.Size = new System.Drawing.Size(233, 20);
            this.tbMapName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Label  field";
            // 
            // comboBoxLabelField
            // 
            this.comboBoxLabelField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLabelField.FormattingEnabled = true;
            this.comboBoxLabelField.Location = new System.Drawing.Point(116, 46);
            this.comboBoxLabelField.Name = "comboBoxLabelField";
            this.comboBoxLabelField.Size = new System.Drawing.Size(233, 21);
            this.comboBoxLabelField.TabIndex = 3;
            this.comboBoxLabelField.SelectedIndexChanged += new System.EventHandler(this.comboBoxLabelField_SelectedIndexChanged);
            // 
            // checkedListBoxValues
            // 
            this.checkedListBoxValues.FormattingEnabled = true;
            this.checkedListBoxValues.Location = new System.Drawing.Point(16, 85);
            this.checkedListBoxValues.Name = "checkedListBoxValues";
            this.checkedListBoxValues.Size = new System.Drawing.Size(333, 199);
            this.checkedListBoxValues.TabIndex = 4;
            // 
            // buttonCreate
            // 
            this.buttonCreate.Enabled = false;
            this.buttonCreate.Location = new System.Drawing.Point(228, 290);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(121, 57);
            this.buttonCreate.TabIndex = 5;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // btOpenMap
            // 
            this.btOpenMap.Enabled = false;
            this.btOpenMap.Location = new System.Drawing.Point(147, 323);
            this.btOpenMap.Name = "btOpenMap";
            this.btOpenMap.Size = new System.Drawing.Size(75, 23);
            this.btOpenMap.TabIndex = 6;
            this.btOpenMap.Text = "Open map";
            this.btOpenMap.UseVisualStyleBackColor = true;
            this.btOpenMap.Click += new System.EventHandler(this.btOpenMap_Click);
            // 
            // CreateProcessMapBasicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 358);
            this.Controls.Add(this.btOpenMap);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.checkedListBoxValues);
            this.Controls.Add(this.comboBoxLabelField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMapName);
            this.Controls.Add(this.label1);
            this.Name = "CreateProcessMapBasicForm";
            this.Text = "CreateProcessMapBasicForm";
            this.Load += new System.EventHandler(this.CreateProcessMapBasicForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMapName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxLabelField;
        private System.Windows.Forms.CheckedListBox checkedListBoxValues;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button btOpenMap;
    }
}