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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            label2 = new System.Windows.Forms.Label();
            betterCheckBox1 = new BetterCheckBox();
            betterButton1 = new BetterButton();
            label1 = new System.Windows.Forms.Label();
            splitContainer3 = new System.Windows.Forms.SplitContainer();
            button1 = new BetterButton();
            button2 = new BetterButton();
            label3 = new System.Windows.Forms.Label();
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
            splitContainer1.Size = new System.Drawing.Size(649, 546);
            splitContainer1.SplitterDistance = 503;
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
            splitContainer2.Size = new System.Drawing.Size(649, 503);
            splitContainer2.SplitterDistance = 366;
            splitContainer2.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(codeTextBox1);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(649, 366);
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
            codeTextBox1.Size = new System.Drawing.Size(643, 344);
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
            betterGroupBox1.Size = new System.Drawing.Size(649, 133);
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
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.Size = new System.Drawing.Size(643, 111);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            tableLayoutPanel2.Controls.Add(label2, 1, 0);
            tableLayoutPanel2.Controls.Add(betterCheckBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(betterButton1, 2, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 75);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new System.Drawing.Size(637, 33);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // label2
            // 
            label2.Dock = System.Windows.Forms.DockStyle.Fill;
            label2.Location = new System.Drawing.Point(143, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(388, 33);
            label2.TabIndex = 0;
            label2.Text = "Special mode for generating loading screen images, needs a few extra steps, read the instructions after enabling!";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // betterCheckBox1
            // 
            betterCheckBox1.AutoSize = true;
            betterCheckBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            betterCheckBox1.Enabled = false;
            betterCheckBox1.Location = new System.Drawing.Point(0, 0);
            betterCheckBox1.Margin = new System.Windows.Forms.Padding(0);
            betterCheckBox1.Name = "betterCheckBox1";
            betterCheckBox1.Size = new System.Drawing.Size(140, 33);
            betterCheckBox1.TabIndex = 0;
            betterCheckBox1.Text = "Loading screen mode";
            betterCheckBox1.UseVisualStyleBackColor = true;
            betterCheckBox1.CheckedChanged += betterCheckBox1_CheckedChanged;
            // 
            // betterButton1
            // 
            betterButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            betterButton1.Enabled = false;
            betterButton1.Location = new System.Drawing.Point(537, 3);
            betterButton1.Name = "betterButton1";
            betterButton1.Size = new System.Drawing.Size(97, 27);
            betterButton1.TabIndex = 1;
            betterButton1.Text = "Finalise Images";
            betterButton1.UseVisualStyleBackColor = true;
            betterButton1.Click += betterButton1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(637, 36);
            label1.TabIndex = 0;
            label1.Text = "Selected VMAP: ";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            splitContainer3.Size = new System.Drawing.Size(649, 39);
            splitContainer3.SplitterDistance = 324;
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
            button1.Size = new System.Drawing.Size(324, 39);
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
            button2.Size = new System.Drawing.Size(321, 39);
            button2.TabIndex = 0;
            button2.Text = "Regenerate Commands";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.Dock = System.Windows.Forms.DockStyle.Fill;
            label3.Location = new System.Drawing.Point(3, 36);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(637, 36);
            label3.TabIndex = 2;
            label3.Text = "Output Path: ";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(655, 552);
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private BetterCheckBox betterCheckBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private BetterButton betterButton1;
        private System.Windows.Forms.Label label3;
    }
}