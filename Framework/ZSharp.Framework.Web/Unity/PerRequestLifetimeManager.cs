using System;

namespace Microsoft.Practices.Unity
{
    public class PerRequestLifetimeManager : LifetimeManager
    {
        private readonly object lifetimeKey = new object();

        public override object GetValue()
        {
            return UnityPerRequestHttpModule.GetValue(lifetimeKey);
        }

        public override void SetValue(object newValue)
        {
            UnityPerRequestHttpModule.SetValue(lifetimeKey, newValue);
        }

        public override void RemoveValue()
        {
            IDisposable disposable = GetValue() as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
            UnityPerRequestHttpModule.SetValue(lifetimeKey, null);
        }
    }
}
