namespace ClientApp.LogExplorer.RuleEditor
{
    partial class RuleEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleEditorForm));
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbColorHex = new System.Windows.Forms.TextBox();
            this.btColorPick = new System.Windows.Forms.Button();
            this.pbColorExample = new System.Windows.Forms.PictureBox();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.tbJs = new FastColoredTextBoxNS.FastColoredTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorExample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbJs)).BeginInit();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(12, 29);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(461, 20);
            this.tbName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Js selector";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pbColorExample);
            this.groupBox1.Controls.Add(this.btColorPick);
            this.groupBox1.Controls.Add(this.tbColorHex);
            this.groupBox1.Controls.Add(this.tbText);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 453);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 145);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Display options";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Text (optional)";
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(9, 96);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(71, 20);
            this.tbText.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Color";
            // 
            // tbColorHex
            // 
            this.tbColorHex.Location = new System.Drawing.Point(9, 32);
            this.tbColorHex.Name = "tbColorHex";
            this.tbColorHex.Size = new System.Drawing.Size(177, 20);
            this.tbColorHex.TabIndex = 2;
            this.tbColorHex.TextChanged += new System.EventHandler(this.tbColorHex_TextChanged);
            // 
            // btColorPick
            // 
            this.btColorPick.Location = new System.Drawing.Point(223, 32);
            this.btColorPick.Name = "btColorPick";
            this.btColorPick.Size = new System.Drawing.Size(75, 23);
            this.btColorPick.TabIndex = 3;
            this.btColorPick.Text = "Picker";
            this.btColorPick.UseVisualStyleBackColor = true;
            this.btColorPick.Click += new System.EventHandler(this.btColorPick_Click);
            // 
            // pbColorExample
            // 
            this.pbColorExample.BackColor = System.Drawing.SystemColors.Control;
            this.pbColorExample.Location = new System.Drawing.Point(192, 31);
            this.pbColorExample.Name = "pbColorExample";
            this.pbColorExample.Size = new System.Drawing.Size(25, 25);
            this.pbColorExample.TabIndex = 4;
            this.pbColorExample.TabStop = false;
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.Location = new System.Drawing.Point(398, 600);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 4;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(317, 600);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // tbJs
            // 
            this.tbJs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbJs.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.tbJs.AutoIndentCharsPatterns = "\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\n";
            this.tbJs.AutoScrollMinSize = new System.Drawing.Size(173, 54);
            this.tbJs.BackBrush = null;
            this.tbJs.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.tbJs.CharHeight = 18;
            this.tbJs.CharWidth = 9;
            this.tbJs.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbJs.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbJs.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbJs.IsReplaceMode = false;
            this.tbJs.Language = FastColoredTextBoxNS.Language.JS;
            this.tbJs.LeftBracket = '(';
            this.tbJs.LeftBracket2 = '{';
            this.tbJs.Location = new System.Drawing.Point(12, 68);
            this.tbJs.Name = "tbJs";
            this.tbJs.Paddings = new System.Windows.Forms.Padding(0);
            this.tbJs.RightBracket = ')';
            this.tbJs.RightBracket2 = '}';
            this.tbJs.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tbJs.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("tbJs.ServiceColors")));
            this.tbJs.Size = new System.Drawing.Size(461, 378);
            this.tbJs.TabIndex = 6;
            this.tbJs.Text = "function(o){\r\n    return true;\r\n};";
            this.tbJs.Zoom = 100;
            // 
            // RuleEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 631);
            this.Controls.Add(this.tbJs);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbName);
            this.Name = "RuleEditorForm";
            this.Text = "RuleEditorForm";
            this.Load += new System.EventHandler(this.RuleEditorForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColorExample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbJs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.PictureBox pbColorExample;
        private System.Windows.Forms.Button btColorPick;
        private System.Windows.Forms.TextBox tbColorHex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private FastColoredTextBoxNS.FastColoredTextBox tbJs;
    }
}