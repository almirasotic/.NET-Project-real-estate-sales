﻿using AutoMapper;
using BackInformSistemi.Dtos;
using BackInformSistemi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackInformSistemi.Controllers
{
    public class FurnishingTypeController: BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public FurnishingTypeController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFurnishingTypes()
        {
            var furnishingTypes = await uow.FurnishingTypeRepository.GetFurnishingTypesAsync();
            var furnishingTypesDto = mapper.Map<IEnumerable<KeyValuePairDto>>(furnishingTypes);
            return Ok(furnishingTypesDto);
        }
    }
}
