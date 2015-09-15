using Microsoft.Practices.Unity;
using My2Cents.HTC.AHPilotStats.DataRepository;
using My2Cents.HTC.AHPilotStats.ExtensionMethods;
using My2Cents.HTC.PilotScoreSvc.ServiceLayer;
using My2Cents.HTC.PilotScoreSvc.ServiceLayer.Interfaces;
using My2Cents.HTC.PilotScoreSvc.Utilities;

namespace My2Cents.HTC.AHPilotStats.DependencyManagement
{
    internal static class UnityBootstrapper
    {
        public static void Initialise()
        {
            var container = new UnityContainer();
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

            // helpers
            container.RegisterAsSingleton<SquadScoreStatsBuilder, SquadScoreStatsBuilder>();
            container.RegisterAsSingleton<GraphBuilder, GraphBuilder>();
            container.RegisterAsSingleton<IRegistry, Registry>();

            // service layer
            container.RegisterAsSingleton<IHTCTourDefinitionsSvc, HTCTourDefinitionsSvc>();
            container.RegisterAsSingleton<IHTCPilotStatsSvc, HTCPilotStatsSvc>();
            container.RegisterAsSingleton<IHTCPilotScoreSvc, HTCPilotScoreSvc>();
            container.RegisterAsSingleton<IHtmlToXMLLoader, HtmlToXMLLoader>();
        }
    }
}