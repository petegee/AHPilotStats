using Microsoft.Practices.Unity;
using My2Cents.HTC.PilotScoreSvc.ServiceLayer;
using My2Cents.HTC.PilotScoreSvc.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My2Cents.HTC.AHPilotStats.ExtensionMethods;
using My2Cents.HTC.AHPilotStats.DataRepository;

namespace My2Cents.HTC.AHPilotStats.DependencyManagement
{
    static class UnityBootstrapper
    {
        public static void Initialise()
        {
            var container  = new UnityContainer();
            ServiceLocator.Instance = container;
            
            // forms
            container.RegisterAsPerResolve<MainMDI, MainMDI>();
            container.RegisterAsPerResolve<DeleteForm, DeleteForm>();
            container.RegisterAsPerResolve<PilotStatsForm, PilotStatsForm>();
            container.RegisterAsPerResolve<About, About>();
            container.RegisterAsPerResolve<NetConnectionSelectorForm, NetConnectionSelectorForm>();
            container.RegisterAsPerResolve<PilotDataLoaderForm, PilotDataLoaderForm>();
            container.RegisterAsPerResolve<DefineSquadronForm, DefineSquadronForm>();
            container.RegisterAsPerResolve<StartupTips, StartupTips>();

            container.RegisterAsSingleton<SquadScoreStatsBuilder, SquadScoreStatsBuilder>();
            container.RegisterAsSingleton<IRegistry, Registry>();


            container.RegisterAsSingleton<IHTCTourDefinitionsSvc, HTCTourDefinitionsSvc>();
            container.RegisterAsSingleton<IHTCPilotStatsSvc, HTCPilotStatsSvc>();
            container.RegisterAsSingleton<IHTCTourDefinitionsSvc, HTCTourDefinitionsSvc>();
        }
    }
}
