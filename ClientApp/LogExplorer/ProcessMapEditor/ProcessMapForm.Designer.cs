namespace ClientApp.LogExplorer.ProcessMapEditor
{
    partial class ProcessMapForm
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
            this.panelForMainView = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonTruncateByNumberOfCon = new System.Windows.Forms.RadioButton();
            this.radioButtonTruncateByValue = new System.Windows.Forms.RadioButton();
            this.checkBoxLiveRefresh = new System.Windows.Forms.CheckBox();
            this.btTruncate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nudTruncate = new System.Windows.Forms.NumericUpDown();
            this.trackBarTruncate = new System.Windows.Forms.TrackBar();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTruncate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTruncate)).BeginInit();
            this.SuspendLayout();
            // 
            // panelForMainView
            // 
            this.panelForMainView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForMainView.Location = new System.Drawing.Point(13, 13);
            this.panelForMainView.Name = "panelForMainView";
            this.panelForMainView.Size = new System.Drawing.Size(902, 400);
            this.panelForMainView.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButtonTruncateByNumberOfCon);
            this.groupBox1.Controls.Add(this.radioButtonTruncateByValue);
            this.groupBox1.Controls.Add(this.checkBoxLiveRefresh);
            this.groupBox1.Controls.Add(this.btTruncate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudTruncate);
            this.groupBox1.Controls.Add(this.trackBarTruncate);
            this.groupBox1.Location = new System.Drawing.Point(970, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 252);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Truncate";
            // 
            // radioButtonTruncateByNumberOfCon
            // 
            this.radioButtonTruncateByNumberOfCon.AutoSize = true;
            this.radioButtonTruncateByNumberOfCon.Location = new System.Drawing.Point(7, 109);
            this.radioButtonTruncateByNumberOfCon.Name = "radioButtonTruncateByNumberOfCon";
            this.radioButtonTruncateByNumberOfCon.Size = new System.Drawing.Size(153, 17);
            this.radioButtonTruncateByNumberOfCon.TabIndex = 6;
            this.radioButtonTruncateByNumberOfCon.Text = "Percentage of connections";
            this.radioButtonTruncateByNumberOfCon.UseVisualStyleBackColor = true;
            this.radioButtonTruncateByNumberOfCon.CheckedChanged += new System.EventHandler(this.radioButtonTruncate_CheckedChanged);
            // 
            // radioButtonTruncateByValue
            // 
            this.radioButtonTruncateByValue.AutoSize = true;
            this.radioButtonTruncateByValue.Checked = true;
            this.radioButtonTruncateByValue.Location = new System.Drawing.Point(7, 86);
            this.radioButtonTruncateByValue.Name = "radioButtonTruncateByValue";
            this.radioButtonTruncateByValue.Size = new System.Drawing.Size(121, 17);
            this.radioButtonTruncateByValue.TabIndex = 6;
            this.radioButtonTruncateByValue.TabStop = true;
            this.radioButtonTruncateByValue.Text = "Percentage of value";
            this.radioButtonTruncateByValue.UseVisualStyleBackColor = true;
            this.radioButtonTruncateByValue.CheckedChanged += new System.EventHandler(this.radioButtonTruncate_CheckedChanged);
            // 
            // checkBoxLiveRefresh
            // 
            this.checkBoxLiveRefresh.AutoSize = true;
            this.checkBoxLiveRefresh.Location = new System.Drawing.Point(6, 184);
            this.checkBoxLiveRefresh.Name = "checkBoxLiveRefresh";
            this.checkBoxLiveRefresh.Size = new System.Drawing.Size(83, 17);
            this.checkBoxLiveRefresh.TabIndex = 5;
            this.checkBoxLiveRefresh.Text = "LiveRefresh";
            this.checkBoxLiveRefresh.UseVisualStyleBackColor = true;
            // 
            // btTruncate
            // 
            this.btTruncate.Location = new System.Drawing.Point(6, 155);
            this.btTruncate.Name = "btTruncate";
            this.btTruncate.Size = new System.Drawing.Size(168, 23);
            this.btTruncate.TabIndex = 4;
            this.btTruncate.Text = "Truncate";
            this.btTruncate.UseVisualStyleBackColor = true;
            this.btTruncate.Click += new System.EventHandler(this.btTruncate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "%";
            // 
            // nudTruncate
            // 
            this.nudTruncate.Location = new System.Drawing.Point(6, 59);
            this.nudTruncate.Name = "nudTruncate";
            this.nudTruncate.Size = new System.Drawing.Size(120, 20);
            this.nudTruncate.TabIndex = 2;
            this.nudTruncate.ValueChanged += new System.EventHandler(this.nudTruncate_ValueChanged);
            // 
            // trackBarTruncate
            // 
            this.trackBarTruncate.Location = new System.Drawing.Point(6, 19);
            this.trackBarTruncate.Maximum = 100;
            this.trackBarTruncate.Name = "trackBarTruncate";
            this.trackBarTruncate.Size = new System.Drawing.Size(168, 45);
            this.trackBarTruncate.TabIndex = 0;
            this.trackBarTruncate.Scroll += new System.EventHandler(this.trackBarTruncate_Scroll);
            // 
            // ProcessMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 425);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelForMainView);
            this.Name = "ProcessMapForm";
            this.Text = "ProcessMapForm";
            this.Load += new System.EventHandler(this.ProcessMapForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTruncate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTruncate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelForMainView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudTruncate;
        private System.Windows.Forms.TrackBar trackBarTruncate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btTruncate;
        private System.Windows.Forms.CheckBox checkBoxLiveRefresh;
        private System.Windows.Forms.RadioButton radioButtonTruncateByNumberOfCon;
        private System.Windows.Forms.RadioButton radioButtonTruncateByValue;
    }
}