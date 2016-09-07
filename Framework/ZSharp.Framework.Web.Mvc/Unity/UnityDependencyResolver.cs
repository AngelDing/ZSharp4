using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Microsoft.Practices.Unity.Mvc
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            if (typeof(IController).IsAssignableFrom(serviceType))
            {
                return container.Resolve(serviceType, new ResolverOverride[0]);
            }
            object result;
            try
            {
                result = container.Resolve(serviceType, new ResolverOverride[0]);
            }
            catch (ResolutionFailedException)
            {
                result = null;
            }
            return result;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.ResolveAll(serviceType, new ResolverOverride[0]);
        }
    }
}
