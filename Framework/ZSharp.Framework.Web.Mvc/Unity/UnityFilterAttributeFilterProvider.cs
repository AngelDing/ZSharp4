using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Microsoft.Practices.Unity.Mvc
{
    public class UnityFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
        private readonly IUnityContainer container;

        public UnityFilterAttributeFilterProvider(IUnityContainer container)
        {
            this.container = container;
        }

        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            IEnumerable<FilterAttribute> actionAttributes = base.GetActionAttributes(controllerContext, actionDescriptor);
            foreach (FilterAttribute current in actionAttributes)
            {
                container.BuildUp(current.GetType(), current, new ResolverOverride[0]);
            }
            return actionAttributes;
        }

        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            IEnumerable<FilterAttribute> controllerAttributes = base.GetControllerAttributes(controllerContext, actionDescriptor);
            foreach (FilterAttribute current in controllerAttributes)
            {
                container.BuildUp(current.GetType(), current, new ResolverOverride[0]);
            }
            return controllerAttributes;
        }
    }
}
