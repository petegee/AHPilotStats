namespace My2Cents.HTC.AHPilotStats
{
    partial class StartupTips
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartupTips));
            this.nextTipButton = new System.Windows.Forms.Button();
            this.prevTipButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.showTipsChkBx = new System.Windows.Forms.CheckBox();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // nextTipButton
            // 
            this.nextTipButton.Location = new System.Drawing.Point(68, 209);
            this.nextTipButton.Name = "nextTipButton";
            this.nextTipButton.Size = new System.Drawing.Size(50, 23);
            this.nextTipButton.TabIndex = 1;
            this.nextTipButton.Text = ">>";
            this.nextTipButton.UseVisualStyleBackColor = true;
            this.nextTipButton.Click += new System.EventHandler(this.nextTipButton_Click);
            // 
            // prevTipButton
            // 
            this.prevTipButton.Location = new System.Drawing.Point(12, 209);
            this.prevTipButton.Name = "prevTipButton";
            this.prevTipButton.Size = new System.Drawing.Size(50, 23);
            this.prevTipButton.TabIndex = 2;
            this.prevTipButton.Text = "<<";
            this.prevTipButton.UseVisualStyleBackColor = true;
            this.prevTipButton.Click += new System.EventHandler(this.prevTipButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(271, 211);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // showTipsChkBx
            // 
            this.showTipsChkBx.AutoSize = true;
            this.showTipsChkBx.Checked = true;
            this.showTipsChkBx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showTipsChkBx.Location = new System.Drawing.Point(124, 213);
            this.showTipsChkBx.Name = "showTipsChkBx";
            this.showTipsChkBx.Size = new System.Drawing.Size(119, 17);
            this.showTipsChkBx.TabIndex = 4;
            this.showTipsChkBx.Text = "Show tips at startup";
            this.showTipsChkBx.UseVisualStyleBackColor = true;
            this.showTipsChkBx.CheckedChanged += new System.EventHandler(this.showTipsChkBx_CheckedChanged);
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(13, 13);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(333, 190);
            this.richTextBox.TabIndex = 5;
            this.richTextBox.Text = "";
            // 
            // StartupTips
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 246);
            this.ControlBox = false;
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.showTipsChkBx);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.prevTipButton);
            this.Controls.Add(this.nextTipButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartupTips";
            this.Text = "Did You Know?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button nextTipButton;
        private System.Windows.Forms.Button prevTipButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.CheckBox showTipsChkBx;
        private System.Windows.Forms.RichTextBox richTextBox;
    }
}