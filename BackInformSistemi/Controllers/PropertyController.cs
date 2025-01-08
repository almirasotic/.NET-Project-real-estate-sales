using AutoMapper;
using BackInformSistemi.Data;
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
        private readonly DataContext context;

        public PropertyController(IUnitOfWork uow, IMapper mapper, DataContext context)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.context = context;
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
        //[HttpPost("add")]
        //[AllowAnonymous]
        //public async Task<IActionResult> AddProperty(PropertyDto propertyDto)
        //{
        //    Property property = mapper.Map<Property>(propertyDto);

        //    await uow.PropertyRepository.AddProperty(property);
        //    return StatusCode(201);
        //}

        //[HttpPost("add")]
        //[AllowAnonymous]
        //public async Task<IActionResult> AddProperty(PropertyDto propertyDto)
        //{

        //    Property property = mapper.Map<Property>(propertyDto);


        //    await uow.PropertyRepository.AddProperty(property);
        //    await uow.SaveAsync();


        //    var sale = new Sale
        //    {
        //        Date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
        //        Price = property.Price,                     
        //        sellerId = property.PostedBy,              
        //        buyerId = null,                             
        //        agentId = null,                           
        //        propertyId = property.Id               
        //    };


        //    await uow.SalesRepository.AddSale(sale);
        //    await uow.SaveAsync();

        //    return StatusCode(201); 
        //}

        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<IActionResult> AddProperty([FromForm] PropertyDto propertyDto)
        {
            try
            {
                // Mapiraj PropertyDto u Property model
                var property = mapper.Map<Property>(propertyDto);

                if (propertyDto.Image != null && propertyDto.Image.Length > 0)
                {
                    // Kreirajte direktorijum za korisnika
                    var userFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", propertyDto.PostedBy.ToString());
                    if (!Directory.Exists(userFolder))
                    {
                        Directory.CreateDirectory(userFolder);
                    }

                    // Generišite ime za sliku
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(propertyDto.Image.FileName);
                    var filePath = Path.Combine(userFolder, fileName);

                    // Sačuvajte sliku
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await propertyDto.Image.CopyToAsync(stream);
                    }

                    // Postavite relativnu putanju u model nekretnine
                    property.ImagePath = Path.Combine("images", propertyDto.PostedBy.ToString(), fileName);
                }

                // Dodajte nekretninu u bazu
                await uow.PropertyRepository.AddProperty(property);
                await uow.SaveAsync();

                return StatusCode(201, new
                {
                    StatusCode = 201,
                    Message = "Property added successfully.",
                    PropertyId = property.Id,
                    ImagePath = property.ImagePath
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding property: {ex.Message}");
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while adding the property.",
                    Detail = ex.Message
                });
            }
        }

        [HttpDelete("delete/{id}")]
    
        public async Task<IActionResult> DeleteProperty(int id)
        {
            try
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

                uow.PropertyRepository.DeleteProperty(id);
                await uow.SaveAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = $"Property with ID {id} has been deleted successfully."
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting property: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred.",
                    Detail = ex.InnerException?.Message ?? ex.Message
                });
            }
        }




    }
}
