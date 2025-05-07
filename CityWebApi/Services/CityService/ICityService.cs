using System;
using CityWebApi.Core;
using CityWebApi.Core.Dtos.City;

namespace CityWebApi.Services.CityService;

public interface ICityService
{
    Task<ServiceResponse<List<GetCity>>> GetAllCity();
    Task<ServiceResponse<GetCity>> GetCityById(int id);
    Task<ServiceResponse<List<GetCity>>> AddCity(AddCity newCity);
    Task<ServiceResponse<GetCity>> UpdateCity(UpdateCity updatedCity);
    Task<ServiceResponse<List<GetCity>>> DeleteCity(int id);
}
