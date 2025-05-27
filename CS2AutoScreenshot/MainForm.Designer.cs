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
            groupBox1 = new BetterGroupBox();
            codeTextBox1 = new CodeTextBox();
            button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)codeTextBox1).BeginInit();
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
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(button1);
            splitContainer1.Size = new System.Drawing.Size(587, 372);
            splitContainer1.SplitterDistance = 329;
            splitContainer1.TabIndex = 1;
            splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(codeTextBox1);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(587, 329);
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
            codeTextBox1.CharHeight = 14;
            codeTextBox1.CharWidth = 8;
            codeTextBox1.DisabledColor = System.Drawing.Color.FromArgb(100, 180, 180, 180);
            codeTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            codeTextBox1.Font = new System.Drawing.Font("Courier New", 9.75F);
            codeTextBox1.Hotkeys = resources.GetString("codeTextBox1.Hotkeys");
            codeTextBox1.IsReplaceMode = false;
            codeTextBox1.Location = new System.Drawing.Point(3, 19);
            codeTextBox1.Name = "codeTextBox1";
            codeTextBox1.Paddings = new System.Windows.Forms.Padding(0);
            codeTextBox1.ReadOnly = true;
            codeTextBox1.SelectionColor = System.Drawing.Color.FromArgb(60, 0, 0, 255);
            codeTextBox1.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("codeTextBox1.ServiceColors");
            codeTextBox1.ShowLineNumbers = false;
            codeTextBox1.Size = new System.Drawing.Size(581, 307);
            codeTextBox1.TabIndex = 0;
            codeTextBox1.WordWrap = true;
            codeTextBox1.Zoom = 100;
            // 
            // button1
            // 
            button1.Dock = System.Windows.Forms.DockStyle.Fill;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            button1.Location = new System.Drawing.Point(0, 0);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(587, 39);
            button1.TabIndex = 0;
            button1.Text = "Select VMAP";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(593, 378);
            Controls.Add(splitContainer1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Padding = new System.Windows.Forms.Padding(3);
            Text = "CS2AutoScreenshot";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)codeTextBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private BetterGroupBox groupBox1;
        private RadGenGUI.Controls.CodeTextBox codeTextBox1;
    }
}