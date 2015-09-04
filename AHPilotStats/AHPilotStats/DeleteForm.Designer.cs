namespace My2Cents.HTC.AHPilotStats
{
    partial class DeleteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteForm));
            this.deletePilotGroupBox = new System.Windows.Forms.GroupBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.deletePilotButton = new System.Windows.Forms.Button();
            this.pilotListCmbBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.deletePilotGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // deletePilotGroupBox
            // 
            this.deletePilotGroupBox.Controls.Add(this.statusLabel);
            this.deletePilotGroupBox.Controls.Add(this.deletePilotButton);
            this.deletePilotGroupBox.Controls.Add(this.pilotListCmbBox);
            this.deletePilotGroupBox.Location = new System.Drawing.Point(12, 12);
            this.deletePilotGroupBox.Name = "deletePilotGroupBox";
            this.deletePilotGroupBox.Size = new System.Drawing.Size(237, 78);
            this.deletePilotGroupBox.TabIndex = 0;
            this.deletePilotGroupBox.TabStop = false;
            this.deletePilotGroupBox.Text = "Delete Pilot";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(7, 59);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(35, 13);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "label1";
            this.statusLabel.Visible = false;
            // 
            // deletePilotButton
            // 
            this.deletePilotButton.Location = new System.Drawing.Point(152, 19);
            this.deletePilotButton.Name = "deletePilotButton";
            this.deletePilotButton.Size = new System.Drawing.Size(75, 23);
            this.deletePilotButton.TabIndex = 1;
            this.deletePilotButton.Text = "Delete";
            this.deletePilotButton.UseVisualStyleBackColor = true;
            this.deletePilotButton.Click += new System.EventHandler(this.deletePilotButton_Click);
            // 
            // pilotListCmbBox
            // 
            this.pilotListCmbBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pilotListCmbBox.FormattingEnabled = true;
            this.pilotListCmbBox.Location = new System.Drawing.Point(6, 19);
            this.pilotListCmbBox.Name = "pilotListCmbBox";
            this.pilotListCmbBox.Size = new System.Drawing.Size(133, 21);
            this.pilotListCmbBox.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(174, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DeleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 131);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.deletePilotGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeleteForm";
            this.Text = "Delete";
            this.deletePilotGroupBox.ResumeLayout(false);
            this.deletePilotGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox deletePilotGroupBox;
        private System.Windows.Forms.Button deletePilotButton;
        private System.Windows.Forms.ComboBox pilotListCmbBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button button1;

    }
}