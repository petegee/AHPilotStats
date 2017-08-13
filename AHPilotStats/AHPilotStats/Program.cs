using My2Cents.HTC.AHPilotStats.DependencyManagement;
using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Practices.Unity;

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
            var owned = false;
            var instanceMutex = new Mutex(false, "My2Cents.HTC.AHPilotStats.App", out owned);
            if (owned)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                UnityBootstrapper.Initialise();
                Application.Run(ServiceLocator.Instance.Resolve<MainMDI>());
            }
            else
            {
                MessageBox.Show("Sorry, but you can only start one version at a time.","Aces High Pilot Stats - Error");
            }
        }


    }
}