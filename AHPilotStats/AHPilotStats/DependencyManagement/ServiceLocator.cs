using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2Cents.HTC.AHPilotStats.DependencyManagement
{
    static class ServiceLocator
    {
        public static IUnityContainer Instance { get; set; }
    }
}
