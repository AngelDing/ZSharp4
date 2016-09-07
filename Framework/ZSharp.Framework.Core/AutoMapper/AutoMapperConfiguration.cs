using System;
using System.Collections.Generic;
using AutoMapper;

namespace ZSharp.Framework.AutoMapper
{
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        public List<Action<IMapperConfigurationExpression>> Configurators { get; }

        public AutoMapperConfiguration()
        {
            Configurators = new List<Action<IMapperConfigurationExpression>>();
        }
    }
}