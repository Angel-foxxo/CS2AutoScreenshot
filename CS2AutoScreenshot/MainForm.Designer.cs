using RadGenGUI.Controls;

namespace CS2AutoScreenshot
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            groupBox1 = new BetterGroupBox();
            codeTextBox1 = new CodeTextBox();
            betterGroupBox1 = new BetterGroupBox();
            tableLayoutPanel1 = new SettingsTableLayoutPanel();
            tableLayoutPanel2 = new SettingsTableLayoutPanel();
            betterCheckBox1 = new BetterCheckBox();
            betterButton1 = new BetterButton();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            tableLayoutPanel3 = new SettingsTableLayoutPanel();
            TargetnameAsPlacename_Checkbox = new BetterCheckBox();
            label4 = new System.Windows.Forms.Label();
            splitContainer3 = new System.Windows.Forms.SplitContainer();
            button1 = new BetterButton();
            button2 = new BetterButton();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)codeTextBox1).BeginInit();
            betterGroupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new System.Drawing.Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer3);
            splitContainer1.Size = new System.Drawing.Size(883, 606);
            splitContainer1.SplitterDistance = 563;
            splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer2.Location = new System.Drawing.Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(betterGroupBox1);
            splitContainer2.Size = new System.Drawing.Size(883, 563);
            splitContainer2.SplitterDistance = 394;
            splitContainer2.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(codeTextBox1);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(883, 394);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Text Output (copy to console)";
            // 
            // codeTextBox1
            // 
            codeTextBox1.AutoCompleteBracketsList = new char[]
    {
    '(',
    ')',
    '{',
    '}',
    '[',
    ']',
    '"',
    '"',
    '\'',
    '\''
    };
            codeTextBox1.AutoScrollMinSize = new System.Drawing.Size(0, 14);
            codeTextBox1.BackBrush = null;
            codeTextBox1.CaretBlinking = false;
            codeTextBox1.CharHeight = 14;
            codeTextBox1.CharWidth = 8;
            codeTextBox1.DisabledColor = System.Drawing.Color.FromArgb(100, 180, 180, 180);
            codeTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            codeTextBox1.Hotkeys = resources.GetString("codeTextBox1.Hotkeys");
            codeTextBox1.IsReplaceMode = false;
            codeTextBox1.Location = new System.Drawing.Point(3, 19);
            codeTextBox1.Name = "codeTextBox1";
            codeTextBox1.Paddings = new System.Windows.Forms.Padding(0);
            codeTextBox1.ReadOnly = true;
            codeTextBox1.SelectionColor = System.Drawing.Color.FromArgb(60, 0, 0, 255);
            codeTextBox1.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("codeTextBox1.ServiceColors");
            codeTextBox1.ShowLineNumbers = false;
            codeTextBox1.Size = new System.Drawing.Size(877, 372);
            codeTextBox1.TabIndex = 0;
            codeTextBox1.WordWrap = true;
            codeTextBox1.Zoom = 100;
            // 
            // betterGroupBox1
            // 
            betterGroupBox1.Controls.Add(tableLayoutPanel1);
            betterGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            betterGroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            betterGroupBox1.Location = new System.Drawing.Point(0, 0);
            betterGroupBox1.Name = "betterGroupBox1";
            betterGroupBox1.Size = new System.Drawing.Size(883, 165);
            betterGroupBox1.TabIndex = 0;
            betterGroupBox1.TabStop = false;
            betterGroupBox1.Text = "Settings";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 4);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(877, 143);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            tableLayoutPanel2.Controls.Add(betterCheckBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(betterButton1, 2, 0);
            tableLayoutPanel2.Controls.Add(label2, 1, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 75);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new System.Drawing.Size(871, 30);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // betterCheckBox1
            // 
            betterCheckBox1.AutoSize = true;
            betterCheckBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            betterCheckBox1.Enabled = false;
            betterCheckBox1.Location = new System.Drawing.Point(0, 0);
            betterCheckBox1.Margin = new System.Windows.Forms.Padding(0);
            betterCheckBox1.Name = "betterCheckBox1";
            betterCheckBox1.Size = new System.Drawing.Size(144, 30);
            betterCheckBox1.TabIndex = 0;
            betterCheckBox1.Text = "Loading screen mode";
            betterCheckBox1.UseVisualStyleBackColor = true;
            betterCheckBox1.CheckedChanged += betterCheckBox1_CheckedChanged;
            // 
            // betterButton1
            // 
            betterButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            betterButton1.Enabled = false;
            betterButton1.Location = new System.Drawing.Point(771, 3);
            betterButton1.Name = "betterButton1";
            betterButton1.Size = new System.Drawing.Size(97, 24);
            betterButton1.TabIndex = 1;
            betterButton1.Text = "Finalise Images";
            betterButton1.UseVisualStyleBackColor = true;
            betterButton1.Click += betterButton1_Click;
            // 
            // label2
            // 
            label2.Dock = System.Windows.Forms.DockStyle.Fill;
            label2.Location = new System.Drawing.Point(147, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(618, 30);
            label2.TabIndex = 2;
            label2.Text = "Special mode for generating loading screen images, needs a few extra steps, read the instructions after enabling!";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label2.UseCompatibleTextRendering = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(871, 36);
            label1.TabIndex = 0;
            label1.Text = "Selected VMAP: ";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label1.UseCompatibleTextRendering = true;
            // 
            // label3
            // 
            label3.Dock = System.Windows.Forms.DockStyle.Fill;
            label3.Location = new System.Drawing.Point(3, 36);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(871, 36);
            label3.TabIndex = 2;
            label3.Text = "Output Path: ";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label3.UseCompatibleTextRendering = true;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(TargetnameAsPlacename_Checkbox, 0, 0);
            tableLayoutPanel3.Controls.Add(label4, 1, 0);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(3, 111);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new System.Drawing.Size(871, 29);
            tableLayoutPanel3.TabIndex = 3;
            // 
            // TargetnameAsPlacename_Checkbox
            // 
            TargetnameAsPlacename_Checkbox.AutoSize = true;
            TargetnameAsPlacename_Checkbox.Checked = true;
            TargetnameAsPlacename_Checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            TargetnameAsPlacename_Checkbox.Dock = System.Windows.Forms.DockStyle.Fill;
            TargetnameAsPlacename_Checkbox.Enabled = false;
            TargetnameAsPlacename_Checkbox.Location = new System.Drawing.Point(0, 0);
            TargetnameAsPlacename_Checkbox.Margin = new System.Windows.Forms.Padding(0);
            TargetnameAsPlacename_Checkbox.Name = "TargetnameAsPlacename_Checkbox";
            TargetnameAsPlacename_Checkbox.Size = new System.Drawing.Size(170, 29);
            TargetnameAsPlacename_Checkbox.TabIndex = 0;
            TargetnameAsPlacename_Checkbox.Text = "Targetname as placename";
            TargetnameAsPlacename_Checkbox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.Dock = System.Windows.Forms.DockStyle.Fill;
            label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label4.Location = new System.Drawing.Point(173, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(695, 29);
            label4.TabIndex = 1;
            label4.Text = "Will treat the name of the point_camera as a place name and bake it into the botton left of the loading screen image.\r\n";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label4.UseCompatibleTextRendering = true;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer3.IsSplitterFixed = true;
            splitContainer3.Location = new System.Drawing.Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(button1);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(button2);
            splitContainer3.Size = new System.Drawing.Size(883, 39);
            splitContainer3.SplitterDistance = 437;
            splitContainer3.TabIndex = 1;
            // 
            // button1
            // 
            button1.Dock = System.Windows.Forms.DockStyle.Fill;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            button1.Location = new System.Drawing.Point(0, 0);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(437, 39);
            button1.TabIndex = 0;
            button1.Text = "Select VMAP";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Dock = System.Windows.Forms.DockStyle.Fill;
            button2.Enabled = false;
            button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            button2.Location = new System.Drawing.Point(0, 0);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(442, 39);
            button2.TabIndex = 0;
            button2.Text = "Regenerate Commands";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(889, 612);
            Controls.Add(splitContainer1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Padding = new System.Windows.Forms.Padding(3);
            Text = "CS2AutoScreenshot";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)codeTextBox1).EndInit();
            betterGroupBox1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private BetterButton button1;
        private BetterGroupBox groupBox1;
        private RadGenGUI.Controls.CodeTextBox codeTextBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private BetterGroupBox betterGroupBox1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private BetterButton button2;
        private SettingsTableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private BetterCheckBox betterCheckBox1;
        private SettingsTableLayoutPanel tableLayoutPanel2;
        private BetterButton betterButton1;
        private System.Windows.Forms.Label label3;
        private SettingsTableLayoutPanel tableLayoutPanel3;
        private BetterCheckBox TargetnameAsPlacename_Checkbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}