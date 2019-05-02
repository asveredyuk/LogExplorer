namespace ClientApp.LogExplorer.View
{
    partial class JobWaiterForm
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
            this.btClose = new System.Windows.Forms.Button();
            this.jobWaiter = new ClientApp.LogExplorer.View.JobWaiter();
            this.checkBoxAutoClose = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(719, 416);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 1;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // jobWaiter
            // 
            this.jobWaiter.Location = new System.Drawing.Point(13, 13);
            this.jobWaiter.Name = "jobWaiter";
            this.jobWaiter.Size = new System.Drawing.Size(781, 397);
            this.jobWaiter.TabIndex = 0;
            // 
            // checkBoxAutoClose
            // 
            this.checkBoxAutoClose.AutoSize = true;
            this.checkBoxAutoClose.Checked = true;
            this.checkBoxAutoClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoClose.Location = new System.Drawing.Point(13, 421);
            this.checkBoxAutoClose.Name = "checkBoxAutoClose";
            this.checkBoxAutoClose.Size = new System.Drawing.Size(156, 17);
            this.checkBoxAutoClose.TabIndex = 2;
            this.checkBoxAutoClose.Text = "Close form when completed";
            this.checkBoxAutoClose.UseVisualStyleBackColor = true;
            // 
            // JobWaiterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 454);
            this.Controls.Add(this.checkBoxAutoClose);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.jobWaiter);
            this.Name = "JobWaiterForm";
            this.Text = "JobWaiterForm";
            this.Load += new System.EventHandler(this.JobWaiterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btClose;
        public JobWaiter jobWaiter;
        private System.Windows.Forms.CheckBox checkBoxAutoClose;
    }
}