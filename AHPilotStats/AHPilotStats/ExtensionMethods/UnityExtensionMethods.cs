using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2Cents.HTC.AHPilotStats.ExtensionMethods
{
    public static class UnityExtensionMethods
    {
        public static IUnityContainer RegisterAsSingleton<TFrom, TTo>(this IUnityContainer container, params InjectionMember[] injectionMembers)
            where TTo : TFrom
        {
            return container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterAsPerResolve<TFrom, TTo>(this IUnityContainer container, params InjectionMember[] injectionMembers)
            where TTo : TFrom
        {
            return container.RegisterType<TFrom, TTo>(new PerResolveLifetimeManager(), injectionMembers);
        }
    }
}
