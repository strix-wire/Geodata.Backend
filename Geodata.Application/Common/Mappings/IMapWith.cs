﻿using AutoMapper;

namespace Geodata.Application.Common.Mappings;

public interface IMapWith<T>
{
    //Create from type t - selected type
    void Mapping(Profile profile) =>
        profile.CreateMap(typeof(T), GetType());
}
