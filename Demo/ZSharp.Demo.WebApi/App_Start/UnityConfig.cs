using Microsoft.Practices.Unity;
using System;
using ZSharp.Framework.Infrastructure;

namespace ZSharp.Demo.WebApi
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            ServiceLocator.SetLocatorProvider(container);

            //container.RegisterType<IGroupTourRepositoryContext, GroupTourRepositoryContext>(new PerRequestLifetimeManager());

            //container.RegisterType(
            //    typeof(IEfRepository<>),
            //    typeof(BaseOfflineBookingRepository<>),
            //    new PerRequestLifetimeManager()
            //);

            //container.RegisterTypes(
            //    AllClasses.FromAssemblies(true, Assembly.Load("GroupTour.OfflineBooking.RemoteServices")),
            //    WithMappings.FromMatchingInterface,
            //    WithName.Default,
            //    WithLifetime.Custom<PerRequestLifetimeManager>
            //);

            //container.RegisterTypes(
            //    AllClasses.FromAssemblies(true, Assembly.Load("GroupTour.OfflineBooking.Repositories")),
            //    WithMappings.FromMatchingInterface,
            //    WithName.Default,
            //    WithLifetime.Custom<PerRequestLifetimeManager>
            //);

            //container.RegisterTypes(
            //    AllClasses.FromAssemblies(true, Assembly.Load("GroupTour.OfflineBooking.Domain")),
            //    WithMappings.FromMatchingInterface,
            //    WithName.Default,
            //    WithLifetime.Custom<PerRequestLifetimeManager>
            //);

            //container.RegisterTypes(
            //    AllClasses.FromAssemblies(true, Assembly.Load("GroupTour.OfflineBooking.Services")),
            //    WithMappings.FromMatchingInterface,
            //    WithName.Default,
            //    WithLifetime.Custom<PerRequestLifetimeManager>
            //);

            //container.RegisterType(typeof(IConsumerFactory), 
            //    typeof(PaymentConsumerCreator),
            //    WithLifetime.ContainerControlled(typeof(PaymentConsumerCreator))
            //);
        }
    }
}
