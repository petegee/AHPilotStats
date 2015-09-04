using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using My2Cents.HTC.PilotScoreSvc;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats
{
    public partial class NetConnectionSelectorForm : Form
    {
        private ProxySettingsDTO _proxySettings;

        public NetConnectionSelectorForm()
        {
            InitializeComponent();

            _proxySettings = ProxySettingsDTO.GetProxySettings();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _proxySettings.ProxyHost = txtBoxProxyName.Text;

            int port = _proxySettings.ProxyPort;
            try
            {
                port = System.Convert.ToInt32(this.txtBoxProxyPort.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Port");
                return;
            }

            _proxySettings.ProxyPort = port;

            XMLObjectSerialiser<ProxySettingsDTO> proxySerialiser = new XMLObjectSerialiser<ProxySettingsDTO>();
            proxySerialiser.WriteXMLFile(_proxySettings, ".\\netconxsettings.xml");

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButDirectConnection_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            _proxySettings.Option = ProxySettingsDTO.ProxyOption.Direct;
        }

        private void radButUseIESettings_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            _proxySettings.Option = ProxySettingsDTO.ProxyOption.UseIESettings;
        }

        private void radButCustom_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            _proxySettings.Option = ProxySettingsDTO.ProxyOption.Custom;
        }

        private void NetConnectionSelectorForm_Load(object sender, EventArgs e)
        {
            this.txtBoxProxyPort.Text = _proxySettings.ProxyPort.ToString();
            this.txtBoxProxyName.Text = _proxySettings.ProxyHost;

            switch (_proxySettings.Option)
            { 
                case ProxySettingsDTO.ProxyOption.Custom:
                    radButCustom.Checked = true;
                    break;
                case ProxySettingsDTO.ProxyOption.UseIESettings:
                    radButUseIESettings.Checked = true;
                    break;
                case ProxySettingsDTO.ProxyOption.Direct:
                    radButDirectConnection.Checked = true;
                    break;
            }
        }
    }
}