using AutoMapper;
using BackInformSistemi.Dtos;
using BackInformSistemi.Interfaces;
using BackInformSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackInformSistemi.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public PropertyController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet("list/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int sellRent)
        {
            var properties= await uow.PropertyRepository.GetPropertiesAsync(sellRent);
            var propertyListDto= mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(propertyListDto);  
        }

        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var property = await uow.PropertyRepository.GetPropertyDetailAsync(id);

            if (property == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = $"Property with ID {id} not found."
                });
            }

            var propertyDto = mapper.Map<PropertyDetailDto>(property);
            return Ok(propertyDto);
        }
        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<IActionResult> AddProperty(PropertyDto propertyDto)
        {
            Property property = mapper.Map<Property>(propertyDto);
           
            await uow.PropertyRepository.AddProperty(property);
            return StatusCode(201);
        }
       

    }
}
