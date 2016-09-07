using System;

namespace ZSharp.Framework.Dependency
{
    /// <summary>
    /// Define interface for classes those are used to resolve dependencies.
    /// </summary>
    public interface IIocResolver
    {
        T Resolve<T>();

        T Resolve<T>(Type type);

        object Resolve(Type type);
    }
}