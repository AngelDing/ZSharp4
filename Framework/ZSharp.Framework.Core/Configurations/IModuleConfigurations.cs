
namespace ZSharp.Framework.Configurations
{
    public interface IModuleConfigurations
    {
        /// <summary>
        /// Gets the ABP configuration object.
        /// </summary>
        IStartupConfiguration Configuration { get; }
    }
}
