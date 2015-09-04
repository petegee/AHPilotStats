namespace My2Cents.HTC.AHPilotStats
{
    partial class DefineSquadronForm
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
            this.cmbBoxSquadPicker = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNewSquad = new System.Windows.Forms.Button();
            this.grpBoxSquadDetails = new System.Windows.Forms.GroupBox();
            this.squad_SquadMemberDataGridView = new System.Windows.Forms.DataGridView();
            this.pilotNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTourDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTourDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.squadMemberBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxHomePage = new System.Windows.Forms.TextBox();
            this.btnSaveClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpBoxSquadDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.squad_SquadMemberDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadMemberBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbBoxSquadPicker
            // 
            this.cmbBoxSquadPicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxSquadPicker.FormattingEnabled = true;
            this.cmbBoxSquadPicker.Location = new System.Drawing.Point(233, 9);
            this.cmbBoxSquadPicker.Name = "cmbBoxSquadPicker";
            this.cmbBoxSquadPicker.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxSquadPicker.TabIndex = 1;
            this.cmbBoxSquadPicker.SelectedIndexChanged += new System.EventHandler(this.cmbBoxSquadPicker_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pick Squadron";
            // 
            // btnNewSquad
            // 
            this.btnNewSquad.Location = new System.Drawing.Point(233, 36);
            this.btnNewSquad.Name = "btnNewSquad";
            this.btnNewSquad.Size = new System.Drawing.Size(121, 23);
            this.btnNewSquad.TabIndex = 2;
            this.btnNewSquad.Text = "or Define New";
            this.btnNewSquad.UseVisualStyleBackColor = true;
            this.btnNewSquad.Click += new System.EventHandler(this.btnNewSquad_Click);
            // 
            // grpBoxSquadDetails
            // 
            this.grpBoxSquadDetails.Controls.Add(this.squad_SquadMemberDataGridView);
            this.grpBoxSquadDetails.Controls.Add(this.label3);
            this.grpBoxSquadDetails.Controls.Add(this.textBox1);
            this.grpBoxSquadDetails.Controls.Add(this.label2);
            this.grpBoxSquadDetails.Controls.Add(this.txtBoxHomePage);
            this.grpBoxSquadDetails.Location = new System.Drawing.Point(12, 63);
            this.grpBoxSquadDetails.Name = "grpBoxSquadDetails";
            this.grpBoxSquadDetails.Size = new System.Drawing.Size(348, 250);
            this.grpBoxSquadDetails.TabIndex = 3;
            this.grpBoxSquadDetails.TabStop = false;
            this.grpBoxSquadDetails.Text = "Squad Details";
            // 
            // squad_SquadMemberDataGridView
            // 
            this.squad_SquadMemberDataGridView.AutoGenerateColumns = false;
            this.squad_SquadMemberDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pilotNameDataGridViewTextBoxColumn,
            this.startTourDataGridViewTextBoxColumn,
            this.endTourDataGridViewTextBoxColumn});
            this.squad_SquadMemberDataGridView.DataSource = this.squadMemberBindingSource;
            this.squad_SquadMemberDataGridView.Location = new System.Drawing.Point(9, 46);
            this.squad_SquadMemberDataGridView.Name = "squad_SquadMemberDataGridView";
            this.squad_SquadMemberDataGridView.Size = new System.Drawing.Size(333, 150);
            this.squad_SquadMemberDataGridView.TabIndex = 2;
            this.squad_SquadMemberDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.squad_SquadMemberDataGridView_CellValidating);
            this.squad_SquadMemberDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.squad_SquadMemberDataGridView_CellEndEdit);
            // 
            // pilotNameDataGridViewTextBoxColumn
            // 
            this.pilotNameDataGridViewTextBoxColumn.DataPropertyName = "PilotName";
            this.pilotNameDataGridViewTextBoxColumn.HeaderText = "PilotName";
            this.pilotNameDataGridViewTextBoxColumn.Name = "pilotNameDataGridViewTextBoxColumn";
            // 
            // startTourDataGridViewTextBoxColumn
            // 
            this.startTourDataGridViewTextBoxColumn.DataPropertyName = "StartTour";
            this.startTourDataGridViewTextBoxColumn.HeaderText = "StartTour";
            this.startTourDataGridViewTextBoxColumn.Name = "startTourDataGridViewTextBoxColumn";
            // 
            // endTourDataGridViewTextBoxColumn
            // 
            this.endTourDataGridViewTextBoxColumn.DataPropertyName = "EndTour";
            this.endTourDataGridViewTextBoxColumn.HeaderText = "EndTour";
            this.endTourDataGridViewTextBoxColumn.Name = "endTourDataGridViewTextBoxColumn";
            // 
            // squadMemberBindingSource
            // 
            this.squadMemberBindingSource.DataSource = typeof(My2Cents.HTC.AHPilotStats.DomainObjects.Squad.SquadMember);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Squad Name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(81, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(261, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Home Page";
            // 
            // txtBoxHomePage
            // 
            this.txtBoxHomePage.Location = new System.Drawing.Point(7, 215);
            this.txtBoxHomePage.Name = "txtBoxHomePage";
            this.txtBoxHomePage.Size = new System.Drawing.Size(335, 20);
            this.txtBoxHomePage.TabIndex = 4;
            this.txtBoxHomePage.TextChanged += new System.EventHandler(this.txtBoxHomePage_TextChanged);
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.Location = new System.Drawing.Point(260, 319);
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.Size = new System.Drawing.Size(100, 23);
            this.btnSaveClose.TabIndex = 5;
            this.btnSaveClose.Text = "Save and Close";
            this.btnSaveClose.UseVisualStyleBackColor = true;
            this.btnSaveClose.Click += new System.EventHandler(this.btnSaveClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(179, 319);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DefineSquadronForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 349);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpBoxSquadDetails);
            this.Controls.Add(this.btnNewSquad);
            this.Controls.Add(this.btnSaveClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBoxSquadPicker);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(380, 383);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(380, 383);
            this.Name = "DefineSquadronForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Squadron";
            this.Load += new System.EventHandler(this.DefineSquadronForm_Load);
            this.grpBoxSquadDetails.ResumeLayout(false);
            this.grpBoxSquadDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.squad_SquadMemberDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadMemberBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBoxSquadPicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNewSquad;
        private System.Windows.Forms.GroupBox grpBoxSquadDetails;
        private System.Windows.Forms.Button btnSaveClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxHomePage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView squad_SquadMemberDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn pilotNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTourDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTourDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource squadMemberBindingSource;
    }
}