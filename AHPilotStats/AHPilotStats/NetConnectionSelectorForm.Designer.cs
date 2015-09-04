namespace My2Cents.HTC.AHPilotStats
{
    partial class NetConnectionSelectorForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpBxSelectMode = new System.Windows.Forms.GroupBox();
            this.radButCustom = new System.Windows.Forms.RadioButton();
            this.radButUseIESettings = new System.Windows.Forms.RadioButton();
            this.radButDirectConnection = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxProxyName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBoxProxyPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpBxSelectMode.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(343, 208);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(79, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(258, 208);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(79, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpBxSelectMode
            // 
            this.grpBxSelectMode.Controls.Add(this.radButCustom);
            this.grpBxSelectMode.Controls.Add(this.radButUseIESettings);
            this.grpBxSelectMode.Controls.Add(this.radButDirectConnection);
            this.grpBxSelectMode.Controls.Add(this.groupBox1);
            this.grpBxSelectMode.Location = new System.Drawing.Point(12, 12);
            this.grpBxSelectMode.Name = "grpBxSelectMode";
            this.grpBxSelectMode.Size = new System.Drawing.Size(410, 190);
            this.grpBxSelectMode.TabIndex = 10;
            this.grpBxSelectMode.TabStop = false;
            this.grpBxSelectMode.Text = "Select Connection Mode";
            // 
            // radButCustom
            // 
            this.radButCustom.AutoSize = true;
            this.radButCustom.Location = new System.Drawing.Point(6, 67);
            this.radButCustom.Name = "radButCustom";
            this.radButCustom.Size = new System.Drawing.Size(123, 17);
            this.radButCustom.TabIndex = 2;
            this.radButCustom.Text = "Use Custom Settings";
            this.radButCustom.UseVisualStyleBackColor = true;
            this.radButCustom.CheckedChanged += new System.EventHandler(this.radButCustom_CheckedChanged);
            // 
            // radButUseIESettings
            // 
            this.radButUseIESettings.AutoSize = true;
            this.radButUseIESettings.Enabled = false;
            this.radButUseIESettings.Location = new System.Drawing.Point(6, 43);
            this.radButUseIESettings.Name = "radButUseIESettings";
            this.radButUseIESettings.Size = new System.Drawing.Size(165, 17);
            this.radButUseIESettings.TabIndex = 1;
            this.radButUseIESettings.Text = "Use Internet Explorer Settings";
            this.radButUseIESettings.UseVisualStyleBackColor = true;
            this.radButUseIESettings.CheckedChanged += new System.EventHandler(this.radButUseIESettings_CheckedChanged);
            // 
            // radButDirectConnection
            // 
            this.radButDirectConnection.AutoSize = true;
            this.radButDirectConnection.Checked = true;
            this.radButDirectConnection.Location = new System.Drawing.Point(6, 19);
            this.radButDirectConnection.Name = "radButDirectConnection";
            this.radButDirectConnection.Size = new System.Drawing.Size(132, 17);
            this.radButDirectConnection.TabIndex = 0;
            this.radButDirectConnection.TabStop = true;
            this.radButDirectConnection.Text = "Use Direct Connection";
            this.radButDirectConnection.UseVisualStyleBackColor = true;
            this.radButDirectConnection.CheckedChanged += new System.EventHandler(this.radButDirectConnection_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Proxy Address";
            // 
            // txtBoxProxyName
            // 
            this.txtBoxProxyName.Location = new System.Drawing.Point(107, 17);
            this.txtBoxProxyName.Name = "txtBoxProxyName";
            this.txtBoxProxyName.Size = new System.Drawing.Size(204, 20);
            this.txtBoxProxyName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBoxProxyPort);
            this.groupBox1.Controls.Add(this.txtBoxProxyName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(25, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 75);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User proxy server";
            // 
            // txtBoxProxyPort
            // 
            this.txtBoxProxyPort.Location = new System.Drawing.Point(107, 44);
            this.txtBoxProxyPort.Name = "txtBoxProxyPort";
            this.txtBoxProxyPort.Size = new System.Drawing.Size(58, 20);
            this.txtBoxProxyPort.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Proxy Port";
            // 
            // NetConnectionSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 240);
            this.Controls.Add(this.grpBxSelectMode);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NetConnectionSelectorForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Select Internet Connection Type";
            this.Load += new System.EventHandler(this.NetConnectionSelectorForm_Load);
            this.grpBxSelectMode.ResumeLayout(false);
            this.grpBxSelectMode.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpBxSelectMode;
        private System.Windows.Forms.RadioButton radButDirectConnection;
        private System.Windows.Forms.RadioButton radButCustom;
        private System.Windows.Forms.RadioButton radButUseIESettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBoxProxyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxProxyPort;
    }
}