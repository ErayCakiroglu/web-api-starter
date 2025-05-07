using System;
using AutoMapper;
using CityWebApi.Core;
using CityWebApi.Core.Dtos.City;

namespace CityWebApi.Services.CityService;

public class CityService : ICityService
{
    public static List<CityEntity> cityEntities = new List<CityEntity>()
        {
            new CityEntity(),
            new CityEntity{Id = 1, Name = "Trabzon"}
        };
    private readonly IMapper _mapper;

    public CityService(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<ServiceResponse<List<GetCity>>> AddCity(AddCity newCity)
    {
        var serviceResponse = new ServiceResponse<List<GetCity>>();
        CityEntity cityEntity = _mapper.Map<CityEntity>(newCity);
        cityEntity.Id = cityEntities.Max(x => x.Id) + 1;
        cityEntities.Add(cityEntity);
        serviceResponse.Data = cityEntities.Select(x => _mapper.Map<GetCity>(x)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCity>>> DeleteCity(int id)
    {
        ServiceResponse<List<GetCity>> serviceResponse = new ServiceResponse<List<GetCity>>();

        try
        {
            CityEntity cityEntity = cityEntities.Find(x => x.Id == id);
            cityEntities.Remove(cityEntity);
            serviceResponse.Data = cityEntities.Select(x => _mapper.Map<GetCity>(x)).ToList();
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCity>>> GetAllCity()
    {
        var serviceResponse = new ServiceResponse<List<GetCity>>{Data = cityEntities.Select(x => _mapper.Map<GetCity>(x)).ToList()};
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCity?>> GetCityById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCity>();
        var city = cityEntities.Find(x => x.Id == id);
        serviceResponse.Data = _mapper.Map<GetCity>(city);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCity>> UpdateCity(UpdateCity updatedCity)
    {
        ServiceResponse<GetCity> serviceResponse = new ServiceResponse<GetCity>();

        try
        {
            CityEntity cityEntity = cityEntities.FirstOrDefault(x => x.Id == updatedCity.Id);
            cityEntity.Name = updatedCity.Name;
            cityEntity.Population = updatedCity.Population;
            cityEntity.rgbCity = updatedCity.rgbCity;

            serviceResponse.Data = _mapper.Map<GetCity>(cityEntity);
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }
}
