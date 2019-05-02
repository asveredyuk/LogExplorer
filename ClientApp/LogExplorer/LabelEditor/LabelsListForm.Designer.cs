namespace ClientApp.LogExplorer.LabelEditor
{
    partial class LabelsListForm
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
            this.panelForListView = new System.Windows.Forms.Panel();
            this.addButton = new ClientApp.LogExplorer.View.WinformsComponents.IconButton();
            this.cbProfile = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btDeleteAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelForListView
            // 
            this.panelForListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForListView.Location = new System.Drawing.Point(12, 51);
            this.panelForListView.Name = "panelForListView";
            this.panelForListView.Size = new System.Drawing.Size(457, 542);
            this.panelForListView.TabIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.BackColor = System.Drawing.Color.Transparent;
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Image = global::ClientApp.Properties.Resources.baseline_add_black_18dp;
            this.addButton.Location = new System.Drawing.Point(429, 599);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(40, 40);
            this.addButton.TabIndex = 1;
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cbProfile
            // 
            this.cbProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbProfile.FormattingEnabled = true;
            this.cbProfile.Location = new System.Drawing.Point(137, 12);
            this.cbProfile.Name = "cbProfile";
            this.cbProfile.Size = new System.Drawing.Size(205, 28);
            this.cbProfile.TabIndex = 2;
            this.cbProfile.SelectedIndexChanged += new System.EventHandler(this.cbProfile_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Profile";
            // 
            // btDeleteAll
            // 
            this.btDeleteAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDeleteAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btDeleteAll.Location = new System.Drawing.Point(313, 598);
            this.btDeleteAll.Name = "btDeleteAll";
            this.btDeleteAll.Size = new System.Drawing.Size(110, 40);
            this.btDeleteAll.TabIndex = 4;
            this.btDeleteAll.Text = "Delete all";
            this.btDeleteAll.UseVisualStyleBackColor = true;
            this.btDeleteAll.Click += new System.EventHandler(this.btDeleteAll_Click);
            // 
            // LabelsListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 651);
            this.Controls.Add(this.btDeleteAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbProfile);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.panelForListView);
            this.DoubleBuffered = true;
            this.Name = "LabelsListForm";
            this.Text = "LabelsList";
            this.Load += new System.EventHandler(this.LabelsList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelForListView;
        private View.WinformsComponents.IconButton addButton;
        private System.Windows.Forms.ComboBox cbProfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btDeleteAll;
    }
}