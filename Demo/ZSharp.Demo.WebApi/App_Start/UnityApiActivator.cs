using Microsoft.Practices.Unity;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ZSharp.Demo.WebApi.UnityApiActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(ZSharp.Demo.WebApi.UnityApiActivator), "Shutdown")]

namespace ZSharp.Demo.WebApi
{
    /// <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    public static class UnityApiActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();           
            container.Dispose();
        }
    }
}