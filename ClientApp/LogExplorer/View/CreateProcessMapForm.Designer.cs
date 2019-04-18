namespace ClientApp.LogExplorer.View
{
    partial class CreateProcessMapForm
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
            this.checkedListBoxProfiles = new System.Windows.Forms.CheckedListBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btMakeMap = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.btOpenMap = new System.Windows.Forms.Button();
            this.timerCheckStatus = new System.Windows.Forms.Timer(this.components);
            this.labelStatus = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBoxProfiles
            // 
            this.checkedListBoxProfiles.FormattingEnabled = true;
            this.checkedListBoxProfiles.Location = new System.Drawing.Point(19, 69);
            this.checkedListBoxProfiles.Name = "checkedListBoxProfiles";
            this.checkedListBoxProfiles.Size = new System.Drawing.Size(188, 184);
            this.checkedListBoxProfiles.TabIndex = 0;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(60, 17);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 20);
            this.tbName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Profiles";
            // 
            // tabControl
            // 
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.ItemSize = new System.Drawing.Size(20, 20);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(507, 345);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btMakeMap);
            this.tabPage1.Controls.Add(this.checkedListBoxProfiles);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tbName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(499, 317);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labelStatus);
            this.tabPage2.Controls.Add(this.btOpenMap);
            this.tabPage2.Controls.Add(this.btClose);
            this.tabPage2.Controls.Add(this.progressBar1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(499, 317);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(7, 124);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(486, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // btMakeMap
            // 
            this.btMakeMap.Location = new System.Drawing.Point(418, 288);
            this.btMakeMap.Name = "btMakeMap";
            this.btMakeMap.Size = new System.Drawing.Size(75, 23);
            this.btMakeMap.TabIndex = 4;
            this.btMakeMap.Text = "Make map";
            this.btMakeMap.UseVisualStyleBackColor = true;
            this.btMakeMap.Click += new System.EventHandler(this.btMakeMap_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(418, 288);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 1;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btOpenMap
            // 
            this.btOpenMap.Enabled = false;
            this.btOpenMap.Location = new System.Drawing.Point(337, 288);
            this.btOpenMap.Name = "btOpenMap";
            this.btOpenMap.Size = new System.Drawing.Size(75, 23);
            this.btOpenMap.TabIndex = 2;
            this.btOpenMap.Text = "Open map";
            this.btOpenMap.UseVisualStyleBackColor = true;
            this.btOpenMap.Click += new System.EventHandler(this.btOpenMap_Click);
            // 
            // timerCheckStatus
            // 
            this.timerCheckStatus.Interval = 1000;
            this.timerCheckStatus.Tick += new System.EventHandler(this.timerCheckStatus_Tick);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(7, 105);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(63, 13);
            this.labelStatus.TabIndex = 3;
            this.labelStatus.Text = "Status: new";
            // 
            // CreateProcessMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 369);
            this.Controls.Add(this.tabControl);
            this.Name = "CreateProcessMapForm";
            this.Text = "CreateProcessMapForm";
            this.Load += new System.EventHandler(this.CreateProcessMapForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxProfiles;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btMakeMap;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btOpenMap;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Timer timerCheckStatus;
        private System.Windows.Forms.Label labelStatus;
    }
}