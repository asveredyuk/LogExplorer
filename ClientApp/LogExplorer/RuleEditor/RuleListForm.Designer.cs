namespace ClientApp.LogExplorer.RuleEditor
{
    partial class RuleListForm
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
            this.checkedListBoxRules = new System.Windows.Forms.CheckedListBox();
            this.btEdit = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.btClone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBoxRules
            // 
            this.checkedListBoxRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxRules.FormattingEnabled = true;
            this.checkedListBoxRules.Location = new System.Drawing.Point(12, 12);
            this.checkedListBoxRules.Name = "checkedListBoxRules";
            this.checkedListBoxRules.Size = new System.Drawing.Size(318, 349);
            this.checkedListBoxRules.TabIndex = 1;
            // 
            // btEdit
            // 
            this.btEdit.Location = new System.Drawing.Point(93, 367);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(75, 23);
            this.btEdit.TabIndex = 2;
            this.btEdit.Text = "Edit";
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(174, 367);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(75, 23);
            this.btDelete.TabIndex = 2;
            this.btDelete.Text = "Delete";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(12, 367);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 3;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btClone
            // 
            this.btClone.Location = new System.Drawing.Point(256, 368);
            this.btClone.Name = "btClone";
            this.btClone.Size = new System.Drawing.Size(75, 23);
            this.btClone.TabIndex = 4;
            this.btClone.Text = "Clone";
            this.btClone.UseVisualStyleBackColor = true;
            this.btClone.Click += new System.EventHandler(this.btClone_Click);
            // 
            // RuleListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 407);
            this.Controls.Add(this.btClone);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.checkedListBoxRules);
            this.Name = "RuleListForm";
            this.Text = "RuleListForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RuleListForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxRules;
        private System.Windows.Forms.Button btEdit;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btClone;
    }
}