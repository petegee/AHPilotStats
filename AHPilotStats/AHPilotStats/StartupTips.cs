using System;
using System.Windows.Forms;
using My2Cents.HTC.AHPilotStats.Properties;

namespace My2Cents.HTC.AHPilotStats
{
    public partial class StartupTips : Form
    {
        private const int NumberOfTips = 3;

        private readonly string[] _tips = new string[NumberOfTips];
        private int _currentTip = NumberOfTips - 1;

        public StartupTips()
        {
            InitializeComponent();
            showTipsChkBx.Checked = Settings.Default.ShowTipsAtStart;
            SetupTipArray();
            richTextBox.Rtf = GetTip();
        }

        private void SetupTipArray()
        {
            _tips[0] = Resources.Tip0;
            _tips[1] = Resources.Tip2;
            _tips[2] = Resources.Tip1;
        }

        private string GetTip()
        {
            return _tips[_currentTip];
        }

        private void prevTipButton_Click(object sender, EventArgs e)
        {
            if (_currentTip == 0)
                _currentTip = NumberOfTips - 1;
            else
                _currentTip--;

            richTextBox.Rtf = GetTip();
        }

        private void nextTipButton_Click(object sender, EventArgs e)
        {
            if (_currentTip == NumberOfTips - 1)
                _currentTip = 0;
            else
                _currentTip++;

            richTextBox.Rtf = GetTip();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void showTipsChkBx_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowTipsAtStart = showTipsChkBx.Checked;
            Settings.Default.Save();
        }
    }
}