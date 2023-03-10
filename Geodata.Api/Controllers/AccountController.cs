using Geodata.Api.Models.Identity;
using Geodata.Persistence.IdentityEF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Geodata.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class AccountController : BaseController
    {
        private readonly UserManager<MyIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<MyIdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<MyIdentityUser> userManager, RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration, SignInManager<MyIdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return BadRequest("User already exists!");

            MyIdentityUser user = new()
            {
                Name = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            var isAddToRoleUser = await _userManager.AddToRoleAsync(user, UserRoles.User);
            if (!result.Succeeded && !isAddToRoleUser.Succeeded)
                return BadRequest("User creation failed! Please check user details and try again.");

            return Ok();
        }

        [HttpPost]
        [Route("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return BadRequest("User already exists!");

            MyIdentityUser user = new()
            {
                Name = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest("User creation failed! Please check user details and try again.");

            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            await _userManager.AddToRoleAsync(user, UserRoles.Moderator);
            await _userManager.AddToRoleAsync(user, UserRoles.User);

            return Ok();
        }

        /// <summary>
        /// Делает модером юзера
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost]
        [Route("makemoderator")]
        public async Task<IActionResult> MakeModerator([FromBody] string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return BadRequest("User is not exists!");

            //ПРОВЕРИТЬ!!!!!!!!!!!!!
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Admin")
                return BadRequest("user is already a admin!");

            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Moderator")
                return BadRequest("user is already a moderator!");

            await _userManager.AddToRoleAsync(user, UserRoles.Moderator);
            
            return Ok();
        }

        [HttpPost]
        [Route("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            if (tokenModel is null)
                return BadRequest("Invalid client request");

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
                return BadRequest("Invalid access token or refresh token");
            
            string username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid access token or refresh token");

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            });
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetUser/{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            if (User.Identity.Name != username && !User.IsInRole(UserRoles.Admin))
                return Forbid();

            IdentityUser user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return NotFound();

            return new User
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = _userManager.Users.ToList();
            var userList = new List<User>();

            foreach (IdentityUser user in users)
            {
                userList.Add(new User
                {
                    UserName = user.UserName,
                    Email = user.Email
                });
            }

            return userList;
        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        [Route("deleteuser")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) return BadRequest("Invalid user name");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete user");

            return NoContent();
        }

        [Authorize]
        [HttpPost]
        [Route("deletealluser")]
        public async Task<IActionResult> DeleteAllUsers()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return NoContent();
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [Authorize]
        [HttpPost]
        [Route("GenerateAccessToken")]
        public async Task<IActionResult> GenerateAccessToken(MyIdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
                
            };

            var token = CreateToken(claims);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            user.AccessToken = tokenString;
            await _userManager.UpdateAsync(user);

            return Ok(tokenString);
        }

        [Authorize]
        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);

            // получаем настройки из токена
            var name = token.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            var email = token.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;

            // получаем пользователя из базы данных по токену доступа
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.AccessToken == accessToken);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        [Route("GetListUsers")]
        public async Task<IActionResult> GetListUsers()
        {
            var listUsers = await _userManager.Users.ToListAsync();
            
            return Ok(listUsers);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}
