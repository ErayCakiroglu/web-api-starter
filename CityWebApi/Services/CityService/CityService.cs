using System;
using AutoMapper;
using CityWebApi.Core;
using CityWebApi.Core.Dtos.City;
using CityWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CityWebApi.Services.CityService;

public class CityService : ICityService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public CityService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ServiceResponse<List<GetCity>>> AddCity(AddCity newCity)
    {
        var serviceResponse = new ServiceResponse<List<GetCity>>();
        CityEntity cityEntity = _mapper.Map<CityEntity>(newCity);
        _context.CityEntities.Add(cityEntity);
        await _context.SaveChangesAsync();
        serviceResponse.Data = await _context.CityEntities.Select(x => _mapper.Map<GetCity>(x)).ToListAsync();
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCity>>> DeleteCity(int id)
    {
        ServiceResponse<List<GetCity>> serviceResponse = new ServiceResponse<List<GetCity>>();

        try
        {
            CityEntity cityEntity = await _context.CityEntities.FirstAsync(x => x.Id == id);
            _context.CityEntities.Remove(cityEntity);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.CityEntities.Select(x => _mapper.Map<GetCity>(x)).ToListAsync();
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
        var serviceResponse = new ServiceResponse<List<GetCity>>();
        var dbCityEntities = await _context.CityEntities.ToListAsync();
        serviceResponse.Data = dbCityEntities.Select(x => _mapper.Map<GetCity>(x)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCity?>> GetCityById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCity?>();
        var dbCity = await _context.CityEntities.FirstOrDefaultAsync(x => x.Id == id);
        serviceResponse.Data = _mapper.Map<GetCity>(dbCity);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCity>> UpdateCity(UpdateCity updatedCity)
    {
        ServiceResponse<GetCity> serviceResponse = new ServiceResponse<GetCity>();

        try
        {
            CityEntity cityEntity = await _context.CityEntities.FirstOrDefaultAsync(x => x.Id == updatedCity.Id);
            cityEntity.Name = updatedCity.Name;
            cityEntity.Population = updatedCity.Population;
            cityEntity.rgbCity = updatedCity.rgbCity;

            await _context.SaveChangesAsync();

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
