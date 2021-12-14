using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using System.Linq;
using SCMS.API.Models;
using SCMS.API.DTO;
using SCMS.API.Data;
using AutoMapper;
using SCMS.API.Services.Interfaces;

namespace SCMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IStreamChatService _streamChatService;
        private readonly IMapper _mapper;

        public AuthenticationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IStreamChatService streamChatService, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _streamChatService = streamChatService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddYears(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    User = _mapper.Map<User, GetUserDto>(user)
                }); ;
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                Role = UserRoles.User,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse { Status = "Error", Message = $"User creation failed! Please check user details and try again." });
            
            await _userManager.AddToRoleAsync(user, UserRoles.User);

            _streamChatService.GetToken(user.Id);
            _streamChatService.UpdateUser(user.Id, user.Email);
            _streamChatService.AddToMainChannel(user.Id);

            return Ok(new RegisterResponse { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                Role = UserRoles.Admin,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            _streamChatService.GetToken(user.Email);

            return Ok(new RegisterResponse { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("RegisterEmployee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeModel model)
        {
            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse { Status = "Error", Message = " User Already Exist" });

            User user = new User
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                Role = UserRoles.Employee,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse { Status = $"{result.Errors.ToList()[0].Code}", Message = $"{result.Errors.ToList()[0].Description}" });


            if (await _roleManager.RoleExistsAsync(UserRoles.Employee))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Employee);
            }

            _streamChatService.GetToken(user.Email);
            _streamChatService.UpdateUser(user.Id, $"{user.Name + user.Surname}");
            _streamChatService.AddToMainChannel(user.Id);

            return Ok(new RegisterResponse { Status = "Success", Message = "Employee user Created Successfully" });
        }
    }
}  
