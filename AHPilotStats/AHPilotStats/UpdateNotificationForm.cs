using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using My2Cents.HTC.AHPilotStats.DomainObjects;

namespace My2Cents.HTC.AHPilotStats
{
    public partial class UpdateNotificationForm : Form
    {
        private bool userWantsToClose = false;
        private UpdateNotification updateInfo;

        internal bool UserWantsToClose
        {
            get { return userWantsToClose; }
            set { userWantsToClose = value; }
        }


        internal UpdateNotificationForm(UpdateNotification updateInfo)
        {
            InitializeComponent();

            this.updateInfo = updateInfo;
            this.updateDate.Text = updateInfo.ReleaseDate.ToString();
            this.newVersionNumber.Text = updateInfo.UpdatedVersion;
        }


        private void getItNowButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.updateInfo.NewVersionURL);
            UserWantsToClose = true;
            this.Close();
        }

        private void ignoreButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
