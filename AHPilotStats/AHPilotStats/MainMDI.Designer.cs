namespace My2Cents.HTC.AHPilotStats
{
    partial class MainMDI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMDI));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.internetConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadNewPilotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defineSquadronToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editConnectionTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPilotStatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.squadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.internetConnectionToolStripMenuItem,
            this.newPilotStatsToolStripMenuItem,
            this.squadsToolStripMenuItem,
            this.windowsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1016, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // internetConnectionToolStripMenuItem
            // 
            this.internetConnectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadNewPilotToolStripMenuItem,
            this.defineSquadronToolStripMenuItem,
            this.editConnectionTypeToolStripMenuItem});
            this.internetConnectionToolStripMenuItem.Name = "internetConnectionToolStripMenuItem";
            this.internetConnectionToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.internetConnectionToolStripMenuItem.Text = "&Options";
            // 
            // loadNewPilotToolStripMenuItem
            // 
            this.loadNewPilotToolStripMenuItem.Name = "loadNewPilotToolStripMenuItem";
            this.loadNewPilotToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.loadNewPilotToolStripMenuItem.Text = "&Download Data";
            this.loadNewPilotToolStripMenuItem.Click += new System.EventHandler(this.loadNewPilotToolStripMenuItem_Click);
            // 
            // defineSquadronToolStripMenuItem
            // 
            this.defineSquadronToolStripMenuItem.Name = "defineSquadronToolStripMenuItem";
            this.defineSquadronToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.defineSquadronToolStripMenuItem.Text = "Define &Squadron";
            this.defineSquadronToolStripMenuItem.Click += new System.EventHandler(this.defineSquadronToolStripMenuItem_Click);
            // 
            // editConnectionTypeToolStripMenuItem
            // 
            this.editConnectionTypeToolStripMenuItem.Name = "editConnectionTypeToolStripMenuItem";
            this.editConnectionTypeToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.editConnectionTypeToolStripMenuItem.Text = "&Edit Internet Connection Type";
            this.editConnectionTypeToolStripMenuItem.Click += new System.EventHandler(this.editConnectionTypeToolStripMenuItem_Click);
            // 
            // newPilotStatsToolStripMenuItem
            // 
            this.newPilotStatsToolStripMenuItem.Name = "newPilotStatsToolStripMenuItem";
            this.newPilotStatsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.newPilotStatsToolStripMenuItem.Text = "&Pilots";
            // 
            // squadsToolStripMenuItem
            // 
            this.squadsToolStripMenuItem.Name = "squadsToolStripMenuItem";
            this.squadsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.squadsToolStripMenuItem.Text = "&Squads";
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tipsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.newsToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.windowsToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // newsToolStripMenuItem
            // 
            this.newsToolStripMenuItem.Name = "newsToolStripMenuItem";
            this.newsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newsToolStripMenuItem.Text = "&News";
            this.newsToolStripMenuItem.Click += new System.EventHandler(this.newsToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 712);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1016, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(446, 17);
            this.toolStripStatusLabel1.Text = "If you like this application, please consider supporting it by making a donation " +
                "via paypal to:";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(139, 17);
            this.toolStripStatusLabel2.Text = "taurineman@lycos.com";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.IsLink = true;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(62, 17);
            this.toolStripStatusLabel3.Text = "PayPal.com";
            this.toolStripStatusLabel3.Click += new System.EventHandler(this.toolStripStatusLabel3_Click);
            // 
            // tipsToolStripMenuItem
            // 
            this.tipsToolStripMenuItem.Name = "tipsToolStripMenuItem";
            this.tipsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tipsToolStripMenuItem.Text = "&Tips";
            this.tipsToolStripMenuItem.Click += new System.EventHandler(this.tipsToolStripMenuItem_Click);
            // 
            // MainMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMDI";
            this.ShowIcon = false;
            this.Text = "Aces High Pilot Statistics";
            this.Shown += new System.EventHandler(this.MainMDI_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPilotStatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internetConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editConnectionTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadNewPilotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem squadsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defineSquadronToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripMenuItem tipsToolStripMenuItem;
    }
}

