using System;
using System.Security.Claims;
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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CityService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    public async Task<ServiceResponse<List<GetCity>>> AddCity(AddCity newCity)
    {
        var serviceResponse = new ServiceResponse<List<GetCity>>();
        CityEntity cityEntity = _mapper.Map<CityEntity>(newCity);
        cityEntity.User = await _context.UserEntities.FirstOrDefaultAsync(x => x.Id == GetUserId());
        _context.CityEntities.Add(cityEntity);
        await _context.SaveChangesAsync();
        serviceResponse.Data = await _context.CityEntities.Where(x => x.User.Id == GetUserId()).Select(x => _mapper.Map<GetCity>(x)).ToListAsync();
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCity>>> DeleteCity(int id)
    {
        ServiceResponse<List<GetCity>> serviceResponse = new ServiceResponse<List<GetCity>>();
        try
        {
            CityEntity cityEntity = await _context.CityEntities.FirstOrDefaultAsync(x => x.Id == id && x.User.Id == GetUserId());
            if(cityEntity != null)
            {
                _context.CityEntities.Remove(cityEntity);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.CityEntities.Where(x => x.User.Id == GetUserId()).Select(x => _mapper.Map<GetCity>(x)).ToListAsync();
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "City is not found";
            }
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
        var dbCityEntities = await _context.CityEntities.Where(x => x.User.Id == GetUserId()).ToListAsync();
        serviceResponse.Data = dbCityEntities.Select(x => _mapper.Map<GetCity>(x)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCity?>> GetCityById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCity?>();
        var dbCity = await _context.CityEntities.FirstOrDefaultAsync(x => x.Id == id && x.User.Id == GetUserId());
        serviceResponse.Data = _mapper.Map<GetCity>(dbCity);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCity>> UpdateCity(UpdateCity updatedCity)
    {
        ServiceResponse<GetCity> serviceResponse = new ServiceResponse<GetCity>();

        try
        {
            CityEntity cityEntity = await _context.CityEntities.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == updatedCity.Id);
            if(cityEntity.User.Id == GetUserId())
            {
                cityEntity.Name = updatedCity.Name;
                cityEntity.Population = updatedCity.Population;
                cityEntity.rgbCity = updatedCity.rgbCity;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCity>(cityEntity);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "City is not found";
            }
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }
}
