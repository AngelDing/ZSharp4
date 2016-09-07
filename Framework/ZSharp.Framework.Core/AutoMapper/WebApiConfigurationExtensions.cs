using ZSharp.Framework.Configurations;

namespace ZSharp.Framework.AutoMapper
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure ZSharp.Framework.AutoMapper module.
    /// </summary>
    public static class AbpWebApiConfigurationExtensions
    {
        /// <summary>
        /// Used to configure ZSharp.Framework.AutoMapper module.
        /// </summary>
        public static IAutoMapperConfiguration AbpAutoMapper(this IModuleConfigurations configurations)
        {
            return configurations.Configuration.Get<IAutoMapperConfiguration>();
        }
    }
}