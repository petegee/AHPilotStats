using My2Cents.HTC.PilotScoreSvc.Types;
using System;
using System.Windows.Forms;

namespace My2Cents.HTC.AHPilotStats
{
    public partial class NetConnectionSelectorForm : Form
    {
        private readonly ProxySettingsDTO _proxySettings;

        public NetConnectionSelectorForm()
        {
            InitializeComponent();

            _proxySettings = ProxySettingsDTO.GetProxySettings();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _proxySettings.ProxyHost = txtBoxProxyName.Text;

            int port;
            try
            {
                port = Convert.ToInt32(txtBoxProxyPort.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Port");
                return;
            }

            _proxySettings.ProxyPort = port;

            var proxySerialiser = new XMLObjectSerialiser<ProxySettingsDTO>();
            proxySerialiser.WriteXmlFile(_proxySettings, ".\\netconxsettings.xml");

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radButDirectConnection_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            _proxySettings.Option = ProxySettingsDTO.ProxyOption.Direct;
        }

        private void radButCustom_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            _proxySettings.Option = ProxySettingsDTO.ProxyOption.Custom;
        }

        private void NetConnectionSelectorForm_Load(object sender, EventArgs e)
        {
            txtBoxProxyPort.Text = _proxySettings.ProxyPort.ToString();
            txtBoxProxyName.Text = _proxySettings.ProxyHost;

            switch (_proxySettings.Option)
            { 
                case ProxySettingsDTO.ProxyOption.Custom:
                    radButCustom.Checked = true;
                    break;
                case ProxySettingsDTO.ProxyOption.Direct:
                    radButDirectConnection.Checked = true;
                    break;
            }
        }
    }
}