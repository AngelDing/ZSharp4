using System;
using System.Collections.Generic;
using AutoMapper;

namespace ZSharp.Framework.AutoMapper
{
    public interface IAutoMapperConfiguration
    {
        List<Action<IMapperConfigurationExpression>> Configurators { get; }
    }
}