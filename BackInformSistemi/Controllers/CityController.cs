using AutoMapper;
using Azure;
using BackInformSistemi.Dtos;
using BackInformSistemi.Interfaces;
using BackInformSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BackInformSistemi.Controllers
{
    [Authorize]
    public class CityController : BaseController
    {
        
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CityController(  IUnitOfWork uow, IMapper mapper)
        {
            
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: api/City
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCities()
        {
            var cities = await uow.CityRepository.GetCitiesAsync();
          
            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(citiesDto);
        }

        // POST: api/City/add
        [HttpPost("post")]
        public async Task<IActionResult> AddCity([FromBody] CityDto cityDto)
        {
            if (cityDto == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid city data.");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var city = mapper.Map<City>(cityDto);
            city.LastUpdateBy = 1;
            city.LastUpdateOn= DateTime.Now;
            //var city = new City
            //{
            //    Name = cityDto.Name,
            //    LastUpdateBy = 1,
            //    LastUpdateOn = DateTime.Now,

            //};
            // ID će biti automatski generisan od baze
            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();

            return StatusCode(201, city); // Vraćanje kreiranog entiteta
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        {

            if (id != cityDto.Id)
                return BadRequest("update not allowed");
            var cityFromDb = await uow.CityRepository.FindCity(id);
            if (cityFromDb == null)
                return BadRequest("update not allowed");
            cityFromDb.LastUpdateBy = 1;
            cityFromDb.LastUpdateOn = DateTime.Now;
            mapper.Map(cityDto, cityFromDb);

            throw new Exception("some unknown error");
            await uow.SaveAsync();
            return StatusCode(200);
        }

        [HttpPut("updateCityName/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityUpdateDto cityDto)
        {
            var cityFromDb = await uow.CityRepository.FindCity(id);
            cityFromDb.LastUpdateBy = 1;
            cityFromDb.LastUpdateOn = DateTime.Now;
            mapper.Map(cityDto, cityFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
        }


        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateCityPatch(int id, JsonPatchDocument<City>  cityToPatch)
        {
            var cityFromDb = await uow.CityRepository.FindCity(id);
            cityFromDb.LastUpdateBy = 1;
            cityFromDb.LastUpdateOn = DateTime.Now;
            cityToPatch.ApplyTo(cityFromDb, ModelState);
            await uow.SaveAsync();
            return StatusCode(200);
        }

        // GET: api/City/{id}
        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetCityById(int id)
        //{
        //    var city = await repo.GetCityByIdAsync(id);
        //    if (city == null)
        //    {
        //        return NotFound($"City with ID {id} not found.");
        //    }

        //    return Ok(city);
        //}

        // DELETE: api/City/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            uow.CityRepository.DeleteCity(id);
            await uow.SaveAsync();
            return Ok(id);
        }

        // PUT: api/City/update/{id}
        //[HttpPut("update/{id:int}")]
        //public async Task<IActionResult> UpdateCity(int id, [FromBody] City updatedCity)
        //{
        //    if (updatedCity == null || id != updatedCity.Id)
        //    {
        //        return BadRequest("Invalid city data.");
        //    }

           
        //    var city = await uow.CityRepository.GetCityByIdAsync(id);
        //    if (city == null)
        //    {
        //        return NotFound($"City with ID {id} not found.");
        //    }

        //    city.Name = updatedCity.Name;

           
        //    uow.CityRepository.UpdateCity(city);

         
        //    await uow.SaveAsync();

        //    return Ok($"City with ID {id} updated successfully.");
        //}

    }
}
