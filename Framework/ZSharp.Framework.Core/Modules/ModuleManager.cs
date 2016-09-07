using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using System.Collections.Immutable;
using ZSharp.Framework.Configurations;
using ZSharp.Framework.Dependency;

namespace ZSharp.Framework.Modules
{
    /// <summary>
    /// This class is used to manage modules.
    /// </summary>
    public class ModuleManager : IModuleManager
    {
        public ModuleInfo StartupModule { get; private set; }

        private Type _startupModuleType;

        public IReadOnlyList<ModuleInfo> Modules => _modules.ToImmutableList();

        public ILog Logger { get; set; }

        private readonly IIocManager _iocManager;
        private readonly ModuleCollection _modules;

        public ModuleManager(IIocManager iocManager)
        {
            _modules = new ModuleCollection();
            _iocManager = iocManager;
            Logger = LogManager.GetLogger(GetType());
        }

        public virtual void Initialize(Type startupModule)
        {
            _startupModuleType = startupModule;
            LoadAllModules();
        }

        public virtual void StartModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.ForEach(module => module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }

        public virtual void ShutdownModules()
        {
            Logger.Debug("Shutting down has been started");

            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(sm => sm.Instance.Shutdown());

            Logger.Debug("Shutting down completed.");
        }

        private void LoadAllModules()
        {
            Logger.Debug("Loading Abp modules...");

            var moduleTypes = FindAllModules();

            Logger.Debug("Found " + moduleTypes.Count + " ABP modules in total.");

            RegisterModules(moduleTypes);
            CreateModules(moduleTypes);

            ModuleCollection.EnsureKernelModuleToBeFirst(_modules);

            SetDependencies();

            Logger.DebugFormat("{0} modules loaded.", _modules.Count);
        }

        private List<Type> FindAllModules()
        {
            var modules = BaseModule.FindDependedModuleTypesRecursively(_startupModuleType);
            return modules;
        }

        private void CreateModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                var moduleObject = _iocManager.Resolve(moduleType) as BaseModule;
                if (moduleObject == null)
                {
                    throw new ZSharpInitializationException("This type is not an ZSharp module: " + moduleType.AssemblyQualifiedName);
                }

                moduleObject.IocManager = _iocManager;
                moduleObject.Configuration = _iocManager.Resolve<IStartupConfiguration>();

                var moduleInfo = new ModuleInfo(moduleType, moduleObject);

                _modules.Add(moduleInfo);

                if (moduleType == _startupModuleType)
                {
                    StartupModule = moduleInfo;
                }

                Logger.DebugFormat("Loaded module: " + moduleType.AssemblyQualifiedName);
            }
        }

        private void RegisterModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                _iocManager.RegisterIfNot(moduleType);
            }
        }

        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                moduleInfo.Dependencies.Clear();

                //Set dependencies for defined DependsOnAttribute attribute(s).
                foreach (var dependedModuleType in BaseModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new ZSharpInitializationException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null))
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }
    }
}
