using AutoMapper;
using Blazor.SankoreAPI.Models.DataTransfer.Login;
using Blazor.SankoreAPI.Models.DataTransfer.User;
using Blazor.SankoreAPI.Models.Domain;
using Blazor.SankoreAPI.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blazor.SankoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            _logger.LogInformation($"Making request create user whose username is {userDto.Email} in {nameof(Register)}");

            try
            {
                var user = _mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.Email;
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogWarning($"Encountered {error.Description} when attempting to create user {userDto.Email} in {nameof(Register)}");
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await _userManager.AddToRoleAsync(user, "User");
                _logger.LogInformation($"Successfully called {nameof(Register)} and created new user {userDto.Email} for {userDto.FirstName} {userDto.LastName}");
                return Accepted();
            }
            catch (Exception exep)
            {
                _logger.LogError($"Encountered problems in {nameof(Register)} when creating new user {exep}");
                return Problem($"Something went terribly wrong in {nameof(Register)} when attempting to create user", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserDto loginUserDto)
        {
            _logger.LogInformation($"Making request create user whose username is {loginUserDto.Email} in {nameof(Login)}");
            try
            {
                var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
                var passwordIsValid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
                if (user == null || passwordIsValid == false)
                {
                    _logger.LogWarning($"The user {loginUserDto.Email} and or password was not found");
                    return Unauthorized(loginUserDto);
                }
                string tokenString = await GenerateToken(user);

                var response = new AuthResponse
                {
                    UserId = user.Id,
                    Email = loginUserDto.Email,
                    Token = tokenString
                };
                _logger.LogInformation($"Successfully called {nameof(Login)} method by username {loginUserDto.Email}");
                return Accepted(response);

            }
            catch (Exception exep)
            {
                _logger.LogError($"An exception {exep} was thrown in {nameof(Login)} when attempting to login for user{loginUserDto.Email}");
                return Problem($"Something went terribly wrong in {nameof(Login)} when attempting to login by {loginUserDto.Email}", statusCode: 500);
            }
        }

        private async Task<string> GenerateToken(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(user);

            //add claims and role claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(roleClaims)
            .Union(userClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["JwtSettings:Duration"])),
                signingCredentials: credentials);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
