using System;
using System.Windows.Forms;

namespace My2Cents.HTC.AHPilotStats
{
    public partial class StartupTips : Form
    {
        private const int NumberOfTips = 3;

        private string[] tips = new string[NumberOfTips];
        private int currentTip = NumberOfTips-1;


        public StartupTips()
        {
            InitializeComponent();
            showTipsChkBx.Checked = Properties.Settings.Default.ShowTipsAtStart;
            SetupTipArray();
            this.richTextBox.Rtf = GetTip();
        }

        private void SetupTipArray()
        {
            tips[0] = Properties.Resources.Tip0;
            tips[1] = Properties.Resources.Tip2;
            tips[2] = Properties.Resources.Tip1;
        }


        private string GetTip()
        {
            return tips[currentTip];
        }


        private void prevTipButton_Click(object sender, EventArgs e)
        {
            if (currentTip == 0)
                currentTip = NumberOfTips - 1;
            else
                currentTip--;

            this.richTextBox.Rtf = GetTip();
        }

        private void nextTipButton_Click(object sender, EventArgs e)
        {
            if (currentTip == NumberOfTips - 1)
                currentTip = 0;
            else
                currentTip++;

            this.richTextBox.Rtf = GetTip();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showTipsChkBx_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowTipsAtStart = showTipsChkBx.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
