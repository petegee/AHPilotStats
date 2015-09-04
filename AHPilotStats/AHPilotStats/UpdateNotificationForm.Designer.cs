namespace My2Cents.HTC.AHPilotStats
{
    partial class UpdateNotificationForm
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
            this.mainHeadingLabel = new System.Windows.Forms.Label();
            this.newVersionNumber = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.updateDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ignoreButton = new System.Windows.Forms.Button();
            this.getItNowButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainHeadingLabel
            // 
            this.mainHeadingLabel.AutoSize = true;
            this.mainHeadingLabel.Location = new System.Drawing.Point(12, 12);
            this.mainHeadingLabel.Name = "mainHeadingLabel";
            this.mainHeadingLabel.Size = new System.Drawing.Size(342, 13);
            this.mainHeadingLabel.TabIndex = 0;
            this.mainHeadingLabel.Text = "A new version of the Aces High Pilot Stats Application is now available.";
            // 
            // newVersionNumber
            // 
            this.newVersionNumber.AutoSize = true;
            this.newVersionNumber.Location = new System.Drawing.Point(98, 14);
            this.newVersionNumber.Name = "newVersionNumber";
            this.newVersionNumber.Size = new System.Drawing.Size(105, 13);
            this.newVersionNumber.TabIndex = 2;
            this.newVersionNumber.Text = "[newVersionNumber]";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.updateDate);
            this.panel1.Controls.Add(this.newVersionNumber);
            this.panel1.Location = new System.Drawing.Point(15, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(337, 65);
            this.panel1.TabIndex = 3;
            // 
            // updateDate
            // 
            this.updateDate.AutoSize = true;
            this.updateDate.Location = new System.Drawing.Point(98, 34);
            this.updateDate.Name = "updateDate";
            this.updateDate.Size = new System.Drawing.Size(69, 13);
            this.updateDate.TabIndex = 3;
            this.updateDate.Text = "[updateDate]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Release Date:";
            // 
            // ignoreButton
            // 
            this.ignoreButton.Location = new System.Drawing.Point(279, 109);
            this.ignoreButton.Name = "ignoreButton";
            this.ignoreButton.Size = new System.Drawing.Size(75, 23);
            this.ignoreButton.TabIndex = 4;
            this.ignoreButton.Text = "Ignore";
            this.ignoreButton.UseVisualStyleBackColor = true;
            this.ignoreButton.Click += new System.EventHandler(this.ignoreButton_Click);
            // 
            // getItNowButton
            // 
            this.getItNowButton.Location = new System.Drawing.Point(191, 109);
            this.getItNowButton.Name = "getItNowButton";
            this.getItNowButton.Size = new System.Drawing.Size(82, 23);
            this.getItNowButton.TabIndex = 5;
            this.getItNowButton.Text = "Get It Now";
            this.getItNowButton.UseVisualStyleBackColor = true;
            this.getItNowButton.Click += new System.EventHandler(this.getItNowButton_Click);
            // 
            // UpdateNotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 142);
            this.Controls.Add(this.getItNowButton);
            this.Controls.Add(this.ignoreButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainHeadingLabel);
            this.Name = "UpdateNotificationForm";
            this.Text = "New Update Available";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mainHeadingLabel;
        private System.Windows.Forms.Label newVersionNumber;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label updateDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ignoreButton;
        private System.Windows.Forms.Button getItNowButton;
    }
}