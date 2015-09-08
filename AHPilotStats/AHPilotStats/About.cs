using System.Reflection;
using System.Windows.Forms;

namespace My2Cents.HTC.AHPilotStats
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly();
            var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

            label2.Text = "Version " + fvi.FileVersion;
        }
    }

}
