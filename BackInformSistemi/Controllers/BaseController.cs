﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackInformSistemi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        //protected int GetUserId()
        //{
        //    return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //}
    }
}
