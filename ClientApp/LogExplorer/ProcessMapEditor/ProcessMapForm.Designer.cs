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
            this.components = new System.ComponentModel.Container();
            this.panelForMainView = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonTruncateByNumberOfCon = new System.Windows.Forms.RadioButton();
            this.radioButtonTruncateByValue = new System.Windows.Forms.RadioButton();
            this.checkBoxLiveRefresh = new System.Windows.Forms.CheckBox();
            this.btRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nudTruncate = new System.Windows.Forms.NumericUpDown();
            this.trackBarTruncate = new System.Windows.Forms.TrackBar();
            this.checkBoxShowValues = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvSelectedObject = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTruncate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTruncate)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForMainView
            // 
            this.panelForMainView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForMainView.Location = new System.Drawing.Point(3, 3);
            this.panelForMainView.Name = "panelForMainView";
            this.panelForMainView.Size = new System.Drawing.Size(885, 530);
            this.panelForMainView.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButtonTruncateByNumberOfCon);
            this.groupBox1.Controls.Add(this.radioButtonTruncateByValue);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudTruncate);
            this.groupBox1.Controls.Add(this.trackBarTruncate);
            this.groupBox1.Location = new System.Drawing.Point(15, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 137);
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
            this.checkBoxLiveRefresh.Location = new System.Drawing.Point(15, 505);
            this.checkBoxLiveRefresh.Name = "checkBoxLiveRefresh";
            this.checkBoxLiveRefresh.Size = new System.Drawing.Size(83, 17);
            this.checkBoxLiveRefresh.TabIndex = 5;
            this.checkBoxLiveRefresh.Text = "LiveRefresh";
            this.checkBoxLiveRefresh.UseVisualStyleBackColor = true;
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btRefresh.Location = new System.Drawing.Point(15, 466);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(213, 23);
            this.btRefresh.TabIndex = 4;
            this.btRefresh.Text = "Refresh";
            this.toolTip1.SetToolTip(this.btRefresh, "Graph is refreshed when settings changed");
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btTruncate_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(177, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "%";
            // 
            // nudTruncate
            // 
            this.nudTruncate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTruncate.Location = new System.Drawing.Point(6, 59);
            this.nudTruncate.Name = "nudTruncate";
            this.nudTruncate.Size = new System.Drawing.Size(165, 20);
            this.nudTruncate.TabIndex = 2;
            this.nudTruncate.ValueChanged += new System.EventHandler(this.nudTruncate_ValueChanged);
            // 
            // trackBarTruncate
            // 
            this.trackBarTruncate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarTruncate.Location = new System.Drawing.Point(6, 19);
            this.trackBarTruncate.Maximum = 100;
            this.trackBarTruncate.Name = "trackBarTruncate";
            this.trackBarTruncate.Size = new System.Drawing.Size(211, 45);
            this.trackBarTruncate.TabIndex = 0;
            this.trackBarTruncate.Scroll += new System.EventHandler(this.trackBarTruncate_Scroll);
            // 
            // checkBoxShowValues
            // 
            this.checkBoxShowValues.AutoSize = true;
            this.checkBoxShowValues.Location = new System.Drawing.Point(15, 154);
            this.checkBoxShowValues.Name = "checkBoxShowValues";
            this.checkBoxShowValues.Size = new System.Drawing.Size(163, 17);
            this.checkBoxShowValues.TabIndex = 6;
            this.checkBoxShowValues.Text = "Show values on connections";
            this.checkBoxShowValues.UseVisualStyleBackColor = true;
            this.checkBoxShowValues.CheckedChanged += new System.EventHandler(this.checkBoxShowValues_CheckedChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipTitle = "\\";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvSelectedObject);
            this.groupBox2.Location = new System.Drawing.Point(15, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(223, 245);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected object";
            // 
            // dgvSelectedObject
            // 
            this.dgvSelectedObject.AllowUserToAddRows = false;
            this.dgvSelectedObject.AllowUserToDeleteRows = false;
            this.dgvSelectedObject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelectedObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelectedObject.Location = new System.Drawing.Point(3, 16);
            this.dgvSelectedObject.Name = "dgvSelectedObject";
            this.dgvSelectedObject.ReadOnly = true;
            this.dgvSelectedObject.RowHeadersVisible = false;
            this.dgvSelectedObject.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSelectedObject.Size = new System.Drawing.Size(217, 226);
            this.dgvSelectedObject.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelForMainView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.btRefresh);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxShowValues);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxLiveRefresh);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1143, 536);
            this.splitContainer1.SplitterDistance = 891;
            this.splitContainer1.TabIndex = 8;
            // 
            // ProcessMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 560);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ProcessMapForm";
            this.Text = "ProcessMapForm";
            this.Load += new System.EventHandler(this.ProcessMapForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTruncate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTruncate)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedObject)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelForMainView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudTruncate;
        private System.Windows.Forms.TrackBar trackBarTruncate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.CheckBox checkBoxLiveRefresh;
        private System.Windows.Forms.RadioButton radioButtonTruncateByNumberOfCon;
        private System.Windows.Forms.RadioButton radioButtonTruncateByValue;
        private System.Windows.Forms.CheckBox checkBoxShowValues;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvSelectedObject;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}