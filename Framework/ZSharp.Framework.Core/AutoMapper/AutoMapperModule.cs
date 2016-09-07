using System.Reflection;
using AutoMapper;
using ZSharp.Framework.Modules;
using ZSharp.Framework.Reflection;
using Microsoft.Practices.Unity;

namespace ZSharp.Framework.AutoMapper
{
    [DependsOn(typeof(KernelModule))]
    public class AutoMapperModule : BaseModule
    {
        private readonly ITypeFinder _typeFinder;

        private static bool _createdMappingsBefore;

        private static readonly object SyncObj = new object();

        public AutoMapperModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public override void PreInitialize()
        {
            IocManager.Register<IAutoMapperConfiguration, AutoMapperConfiguration>();
        }

        public override void PostInitialize()
        {
            CreateMappings();

            IocManager.IocContainer.RegisterInstance(Mapper.Instance);
        }

        public void CreateMappings()
        {
            lock (SyncObj)
            {
                //We should prevent duplicate mapping in an application, since Mapper is static.
                if (_createdMappingsBefore)
                {
                    return;
                }

                Mapper.Initialize(configuration =>
                {
                    FindAndAutoMapTypes(configuration);
                    foreach (var configurator in Configuration.Modules.AbpAutoMapper().Configurators)
                    {
                        configurator(configuration);
                    }
                });

                _createdMappingsBefore = true;
            }
        }

        private void FindAndAutoMapTypes(IMapperConfigurationExpression configuration)
        {
            var types = _typeFinder.Find(type =>
                    type.IsDefined(typeof(AutoMapAttribute)) ||
                    type.IsDefined(typeof(AutoMapFromAttribute)) ||
                    type.IsDefined(typeof(AutoMapToAttribute))
            );

            Logger.DebugFormat("Found {0} classes defines auto mapping attributes", types.Length);
            foreach (var type in types)
            {
                Logger.Debug(type.FullName);
                configuration.CreateAbpAttributeMaps(type);
            }
        }
    }
}
