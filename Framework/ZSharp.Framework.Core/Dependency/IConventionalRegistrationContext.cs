using System.Reflection;

namespace ZSharp.Framework.Dependency
{
    /// <summary>
    /// Used to pass needed objects on conventional registration process.
    /// </summary>
    public interface IConventionalRegistrationContext
    {
        /// <summary>
        /// Gets the registering Assembly.
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Reference to the IOC Container to register types.
        /// </summary>
        IIocManager IocManager { get; }
    }
}