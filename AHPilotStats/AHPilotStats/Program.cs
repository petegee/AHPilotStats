using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;
using System.Net;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using My2Cents.HTC.AHPilotStats.DomainObjects;

namespace My2Cents.HTC.AHPilotStats
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool owned = false;
            Mutex instanceMutex = new Mutex(false, "My2Cents.HTC.AHPilotStats.App", out owned);
            if (owned)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainMDI());
            }
            else
            {
                MessageBox.Show("Sorry, but you can only start one version at a time.","Aces High Pilot Stats - Error");
            }
        }


    }
}