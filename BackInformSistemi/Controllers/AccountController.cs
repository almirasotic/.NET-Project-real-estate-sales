using BackInformSistemi.Data;
using BackInformSistemi.Dtos;
using BackInformSistemi.Interfaces;
using BackInformSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackInformSistemi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration configuration;
        private readonly DataContext context;

        public AccountController(IUnitOfWork uow, IConfiguration configuration, DataContext context)
        {
            this.uow = uow;
            this.configuration = configuration;
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await uow.UserRepository.GetAllUsers());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await uow.UserRepository.Authenticate(loginReq.UserName, loginReq.Password);
            if(user == null)
            {
                return Unauthorized("Invalid user Id or passwordddddd");
            }
            var loginRes = new LoginResDto();
            loginRes.UserName = loginReq.UserName;
            loginRes.Token = CreateJWT(user);
            loginRes.role = user.role;
            loginRes.Id = user.Id;
            return Ok(loginRes);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            if (await uow.UserRepository.UserAlreadyExists(loginReq.UserName))
                return BadRequest("user vec postoji, pokusajte neki drugi");
            uow.UserRepository.Register(loginReq.UserName, loginReq.Password);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        private string CreateJWT(User user)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var claims = new Claim[]
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature
                );
            var tokenDesctipor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesctipor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPut("promoteToAgent/{userId}")]
        public async Task<IActionResult> PromoteToAgent([FromRoute] int userId)
        {
            await uow.UserRepository.UpdateToAgent(userId);
            return Ok();
        }
        [HttpPut("promoteToMenager/{userId}")]
        public async Task<IActionResult> PromoteToMenager([FromRoute] int userId)
        {
            await uow.UserRepository.UpdateToManager(userId);
            return Ok();
        }
        [HttpPut("promoteToAdmin/{userId}")]
        public async Task<IActionResult> PromoteToAdmin([FromRoute] int userId)
        {
            await uow.UserRepository.UpdateToAdministrator(userId);
            return Ok();
        }
        [HttpPut("demoteToBuyer/{userId}")]
        public async Task<IActionResult> DemoteToBuyer([FromRoute] int userId)
        {
            await uow.UserRepository.DemoteToBuyer(userId);
            return Ok();
        }
        [HttpDelete("deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            var user = await uow.UserRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            uow.UserRepository.DeleteUser(user);
            await uow.SaveAsync();

            return Ok($"User with ID {userId} has been deleted.");
        }

        [HttpGet("agents")]
        public IActionResult GetAgents()
        {
            // Pretpostavimo da je "Agent" povezan sa `role = 2`
            var agents = context.Users.Where(u => u.role == 2).ToList();
            return Ok(agents);
        }
       



    }
}
