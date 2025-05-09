using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReStyleUp.Models;

namespace ReStyleUp.Controllers
{
    namespace AuthAPI.Controllers
    {
        [ApiController]
        [Route("api/auth")]
        public class AuthController : ControllerBase
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly JwtBearerTokenSettings _jwtBearerTokenSettings;


            public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtBearerTokenSettings> jwtTokenOptions)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _jwtBearerTokenSettings = jwtTokenOptions.Value;

            }

            //[HttpPost("register")]
            //public async Task<IActionResult> Register([FromBody] RegisterCredentials userDetails)
            //{
            //    if (userDetails == null)
            //        return BadRequest("Invalid user details");

            //    var user = new IdentityUser { UserName = userDetails.Username, Email = userDetails.Email };
            //    var result = await _userManager.CreateAsync(user, userDetails.Password);

            //    if (!result.Succeeded)
            //        return BadRequest(result.Errors);

            //    return Ok("User Registered");
            //}
            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegisterCredentials userDetails)
            {
                if (userDetails == null)
                    return BadRequest("Invalid user details");

                var user = new IdentityUser
                {
                    UserName = userDetails.Username,
                    Email = userDetails.Email
                };

                var result = await _userManager.CreateAsync(user, userDetails.Password);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                // Récupère le rôle ou assigne "User" par défaut
                string roleToAssign = string.IsNullOrWhiteSpace(userDetails.Role) ? "User" : userDetails.Role;

                // Crée le rôle s’il n’existe pas
                if (!await _roleManager.RoleExistsAsync(roleToAssign))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleToAssign));
                }

                // Assigne le rôle à l’utilisateur
                await _userManager.AddToRoleAsync(user, roleToAssign);

                return Ok($"User registered with role '{roleToAssign}'");
            }

            public string? Role { get; set; }
            [HttpGet("roles/{username}")]
            public async Task<IActionResult> GetUserRoles(string username)
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                    return NotFound("User not found");

                var roles = await _userManager.GetRolesAsync(user);
                return Ok(roles);
            }


            [HttpGet("test")]
            public IActionResult Test()
            {
                Console.WriteLine("Test route hit");

                return Ok("API is working");
            }
            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
            {
                if (!ModelState.IsValid || credentials == null)
                    return BadRequest("Invalid credentials");

                var user = await _userManager.FindByNameAsync(credentials.Username);
                if (user == null)
                    return Unauthorized("Invalid username or password");

                var passwordValid = await _userManager.CheckPasswordAsync(user, credentials.Password);
                if (!passwordValid)
                    return Unauthorized("Invalid username or password");

                // Generate JWT token
                var token = GenerateToken(user);

                return Ok(new { Token = token, Message = "Success" });
            }

            [HttpPost("logout")]
            public IActionResult Logout()
            {
                // Log out the user by invalidating the token or by other means
                // JWTs are stateless so there's no "logout" in the traditional sense, but you can invalidate tokens in the server side if needed
                return Ok(new { Message = "Logged out" });
            }

            private string GenerateToken(IdentityUser user)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtBearerTokenSettings.SecretKey);

                var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id), // ✅ Ajout de l'ID ici
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email)
    };

                // ➕ Ajout des rôles de l'utilisateur
                var roles = _userManager.GetRolesAsync(user).Result;
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(_jwtBearerTokenSettings.ExpireTimeInSeconds),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Audience = _jwtBearerTokenSettings.Audience,
                    Issuer = _jwtBearerTokenSettings.Issuer
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }


        }
    }
}
