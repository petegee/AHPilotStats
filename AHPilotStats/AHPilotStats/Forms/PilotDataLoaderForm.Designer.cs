namespace My2Cents.HTC.AHPilotStats
{
    partial class PilotDataLoaderForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtbxPilotToLoad = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressBarLoading = new System.Windows.Forms.ProgressBar();
            this.txtbxStartTour = new System.Windows.Forms.TextBox();
            this.txtbxEndTour = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmboxTourType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUnitsToLoad = new System.Windows.Forms.Label();
            this.lblUnitsLoaded = new System.Windows.Forms.Label();
            this.lblLoading = new System.Windows.Forms.Label();
            this.cmbBoxSquadSelect = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pilot gameID to load";
            // 
            // txtbxPilotToLoad
            // 
            this.txtbxPilotToLoad.Location = new System.Drawing.Point(118, 22);
            this.txtbxPilotToLoad.MaximumSize = new System.Drawing.Size(162, 20);
            this.txtbxPilotToLoad.MinimumSize = new System.Drawing.Size(162, 20);
            this.txtbxPilotToLoad.Name = "txtbxPilotToLoad";
            this.txtbxPilotToLoad.Size = new System.Drawing.Size(162, 20);
            this.txtbxPilotToLoad.TabIndex = 1;
            this.txtbxPilotToLoad.Validating += new System.ComponentModel.CancelEventHandler(this.txtbxPilotToLoad_Validating);
            this.txtbxPilotToLoad.TextChanged += new System.EventHandler(this.txtbxPilotToLoad_TextChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Enabled = false;
            this.btnLoad.Location = new System.Drawing.Point(205, 190);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 15;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(124, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // progressBarLoading
            // 
            this.progressBarLoading.Location = new System.Drawing.Point(31, 161);
            this.progressBarLoading.Name = "progressBarLoading";
            this.progressBarLoading.Size = new System.Drawing.Size(249, 23);
            this.progressBarLoading.TabIndex = 11;
            // 
            // txtbxStartTour
            // 
            this.txtbxStartTour.Location = new System.Drawing.Point(118, 108);
            this.txtbxStartTour.Name = "txtbxStartTour";
            this.txtbxStartTour.Size = new System.Drawing.Size(42, 20);
            this.txtbxStartTour.TabIndex = 7;
            this.txtbxStartTour.Validating += new System.ComponentModel.CancelEventHandler(this.txtbxStartTour_Validating);
            this.txtbxStartTour.TextChanged += new System.EventHandler(this.txtbxStartTour_TextChanged);
            // 
            // txtbxEndTour
            // 
            this.txtbxEndTour.Location = new System.Drawing.Point(240, 108);
            this.txtbxEndTour.Name = "txtbxEndTour";
            this.txtbxEndTour.Size = new System.Drawing.Size(40, 20);
            this.txtbxEndTour.TabIndex = 9;
            this.txtbxEndTour.Validating += new System.ComponentModel.CancelEventHandler(this.txtbxEndTour_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Start Tour";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "End Tour";
            // 
            // cmboxTourType
            // 
            this.cmboxTourType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboxTourType.FormattingEnabled = true;
            this.cmboxTourType.Location = new System.Drawing.Point(118, 79);
            this.cmboxTourType.Name = "cmboxTourType";
            this.cmboxTourType.Size = new System.Drawing.Size(162, 21);
            this.cmboxTourType.TabIndex = 5;
            this.cmboxTourType.Validating += new System.ComponentModel.CancelEventHandler(this.cmboxTourType_Validating);
            this.cmboxTourType.TextChanged += new System.EventHandler(this.cmboxTourType_TextChanged);
            this.cmboxTourType.SelectedValueChanged += new System.EventHandler(this.cmboxTourType_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tour Type";
            // 
            // lblUnitsToLoad
            // 
            this.lblUnitsToLoad.AutoSize = true;
            this.lblUnitsToLoad.Location = new System.Drawing.Point(23, 200);
            this.lblUnitsToLoad.Name = "lblUnitsToLoad";
            this.lblUnitsToLoad.Size = new System.Drawing.Size(13, 13);
            this.lblUnitsToLoad.TabIndex = 12;
            this.lblUnitsToLoad.Text = "0";
            this.lblUnitsToLoad.Visible = false;
            // 
            // lblUnitsLoaded
            // 
            this.lblUnitsLoaded.AutoSize = true;
            this.lblUnitsLoaded.Location = new System.Drawing.Point(85, 200);
            this.lblUnitsLoaded.Name = "lblUnitsLoaded";
            this.lblUnitsLoaded.Size = new System.Drawing.Size(13, 13);
            this.lblUnitsLoaded.TabIndex = 13;
            this.lblUnitsLoaded.Text = "0";
            this.lblUnitsLoaded.Visible = false;
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(31, 142);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(196, 13);
            this.lblLoading.TabIndex = 10;
            this.lblLoading.Text = "Down-loading Pilot Data - Please Wait...";
            this.lblLoading.Visible = false;
            // 
            // cmbBoxSquadSelect
            // 
            this.cmbBoxSquadSelect.FormattingEnabled = true;
            this.cmbBoxSquadSelect.Location = new System.Drawing.Point(118, 50);
            this.cmbBoxSquadSelect.Name = "cmbBoxSquadSelect";
            this.cmbBoxSquadSelect.Size = new System.Drawing.Size(162, 21);
            this.cmbBoxSquadSelect.TabIndex = 3;
            this.cmbBoxSquadSelect.Validating += new System.ComponentModel.CancelEventHandler(this.cmbBoxSquadSelect_Validating);
            this.cmbBoxSquadSelect.SelectedIndexChanged += new System.EventHandler(this.cmbBoxSquadSelect_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "or Select Squad";
            // 
            // PilotDataLoaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(308, 225);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbBoxSquadSelect);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.lblUnitsLoaded);
            this.Controls.Add(this.lblUnitsToLoad);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmboxTourType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtbxEndTour);
            this.Controls.Add(this.txtbxStartTour);
            this.Controls.Add(this.progressBarLoading);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtbxPilotToLoad);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PilotDataLoaderForm";
            this.Text = "Download Pilot Details";
            this.Load += new System.EventHandler(this.PilotDataLoaderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbxPilotToLoad;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressBarLoading;
        private System.Windows.Forms.TextBox txtbxStartTour;
        private System.Windows.Forms.TextBox txtbxEndTour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmboxTourType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblUnitsToLoad;
        private System.Windows.Forms.Label lblUnitsLoaded;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.ComboBox cmbBoxSquadSelect;
        private System.Windows.Forms.Label label5;
    }
}