namespace ClientApp.LogExplorer.LabelEditor
{
    partial class LabelListItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelName = new System.Windows.Forms.Label();
            this.boxLabel = new System.Windows.Forms.Label();
            this.labelGuid = new System.Windows.Forms.Label();
            this.removeButton = new ClientApp.LogExplorer.View.WinformsComponents.IconButton();
            this.btEdit = new ClientApp.LogExplorer.View.WinformsComponents.IconButton();
            this.cloneButton = new ClientApp.LogExplorer.View.WinformsComponents.IconButton();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.Location = new System.Drawing.Point(59, 3);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(124, 21);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "HelloWorldRule";
            // 
            // boxLabel
            // 
            this.boxLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(108)))), ((int)(((byte)(0)))));
            this.boxLabel.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.boxLabel.ForeColor = System.Drawing.Color.White;
            this.boxLabel.Location = new System.Drawing.Point(3, 3);
            this.boxLabel.Name = "boxLabel";
            this.boxLabel.Size = new System.Drawing.Size(50, 50);
            this.boxLabel.TabIndex = 1;
            this.boxLabel.Text = "hwd";
            this.boxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelGuid
            // 
            this.labelGuid.AutoSize = true;
            this.labelGuid.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGuid.Location = new System.Drawing.Point(60, 26);
            this.labelGuid.Name = "labelGuid";
            this.labelGuid.Size = new System.Drawing.Size(251, 17);
            this.labelGuid.TabIndex = 0;
            this.labelGuid.Text = "d149bd99-f978-4b50-811c-b497eb771cf0";
            // 
            // removeButton
            // 
            this.removeButton.BackColor = System.Drawing.Color.Transparent;
            this.removeButton.FlatAppearance.BorderSize = 0;
            this.removeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeButton.Image = global::ClientApp.Properties.Resources.baseline_delete_black_18dp;
            this.removeButton.Location = new System.Drawing.Point(390, 49);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(40, 40);
            this.removeButton.TabIndex = 3;
            this.removeButton.UseVisualStyleBackColor = false;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // btEdit
            // 
            this.btEdit.BackColor = System.Drawing.Color.Transparent;
            this.btEdit.FlatAppearance.BorderSize = 0;
            this.btEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btEdit.Image = global::ClientApp.Properties.Resources.baseline_edit_black_18dp;
            this.btEdit.Location = new System.Drawing.Point(344, 49);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(40, 40);
            this.btEdit.TabIndex = 2;
            this.btEdit.UseVisualStyleBackColor = false;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // cloneButton
            // 
            this.cloneButton.BackColor = System.Drawing.Color.Transparent;
            this.cloneButton.FlatAppearance.BorderSize = 0;
            this.cloneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cloneButton.Image = global::ClientApp.Properties.Resources.baseline_library_add_black_18dp;
            this.cloneButton.Location = new System.Drawing.Point(298, 49);
            this.cloneButton.Name = "cloneButton";
            this.cloneButton.Size = new System.Drawing.Size(40, 40);
            this.cloneButton.TabIndex = 4;
            this.cloneButton.UseVisualStyleBackColor = false;
            this.cloneButton.Click += new System.EventHandler(this.cloneButton_Click);
            // 
            // LabelListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cloneButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.boxLabel);
            this.Controls.Add(this.labelGuid);
            this.Controls.Add(this.labelName);
            this.Name = "LabelListItem";
            this.Size = new System.Drawing.Size(433, 101);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label boxLabel;
        private View.WinformsComponents.IconButton btEdit;
        private System.Windows.Forms.Label labelGuid;
        private View.WinformsComponents.IconButton removeButton;
        private View.WinformsComponents.IconButton cloneButton;
    }
}
