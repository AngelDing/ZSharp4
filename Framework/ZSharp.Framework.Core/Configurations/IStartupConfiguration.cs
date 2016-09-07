namespace ZSharp.Framework.Configurations
{
    public interface IStartupConfiguration
    {
        IModuleConfigurations Modules { get; }

        T Get<T>();
    }
}