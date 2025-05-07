using CityWebApi.Core;
using CityWebApi.Core.Dtos.City;
using CityWebApi.Services.CityService;
using Microsoft.AspNetCore.Mvc;

namespace CityWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<GetCity>>> Get()
        {
            return Ok(await _cityService.GetAllCity());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCity>>> Get(int id)
        {
            return Ok(await _cityService.GetCityById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCity>>>> AddCity(AddCity newCity)
        {
            return Ok(await _cityService.AddCity(newCity));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCity>>> UpdateCity(UpdateCity updatedCity)
        {
            var serviceResponse = await _cityService.UpdateCity(updatedCity);
            if(serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<GetCity>>>> DeleteCity(int id)
        {
            var serviceResponse = await _cityService.DeleteCity(id);
            if(serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }
    }
}
