using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using FinantialManager.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Identity.Jwt;
using NetDevPack.Identity.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FinantialManager.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AccountController(
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _config = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _userRepository.ValidatePassword(loginUser.Email, loginUser.Password);

            if (result)
            {
                var userModel = await _userRepository.GetByEmail(loginUser.Email);
                var userString = JsonConvert.SerializeObject(userModel, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                var fullJwt = GerarTokenJWT(userString);
                return CustomResponse(fullJwt);
            }

            AddError("Incorrect user or password");
            return CustomResponse();
        }

        [Authorize]
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var res = await _userRepository.GetAll();
            return CustomResponse(res);
        }

        private string GerarTokenJWT(object user)
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(1440);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience,
                                             expires: expiry, signingCredentials: credentials);
            token.Payload.Add("user", user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
