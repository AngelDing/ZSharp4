using Microsoft.Practices.Unity;
using System;

namespace ZSharp.Framework.Dependency
{
    /// <summary>
    /// This interface is used to directly perform dependency injection tasks.
    /// </summary>
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        /// <summary>
        /// Reference to the Unity Container.
        /// </summary>
        IUnityContainer IocContainer { get; }
    }
}