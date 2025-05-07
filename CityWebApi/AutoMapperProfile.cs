using System;
using AutoMapper;
using CityWebApi.Core;
using CityWebApi.Core.Dtos.City;

namespace CityWebApi;

public class AutoMapperProfile:Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CityEntity,GetCity>();
        CreateMap<AddCity,CityEntity>();
    }
}
