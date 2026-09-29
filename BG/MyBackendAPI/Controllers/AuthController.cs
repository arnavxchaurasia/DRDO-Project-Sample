using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBackendAPI.Data;
using MyBackendAPI.DTOs;
using MyBackendAPI.Models;
using MyBackendAPI.Services;

namespace MyBackendAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtTokenService _jwt;

        public AuthController(ApplicationDbContext context, JwtTokenService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
        {
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            var exists = await _context.Users.AnyAsync(u => u.Email == normalizedEmail);
            if (exists)
            {
                return Conflict(new { message = "An account with this email already exists." });
            }

            var user = new User
            {
                Email = normalizedEmail,
                PasswordHash = PasswordHasher.Hash(request.Password),
                Role = "Admin",
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new AuthResponse
            {
                Token = _jwt.CreateToken(user),
                Email = user.Email,
                Role = user.Role,
            });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == normalizedEmail);

            // Same "invalid credentials" message whether the email doesn't
            // exist or the password is wrong — don't leak which one it was.
            if (user == null || !PasswordHasher.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            return Ok(new AuthResponse
            {
                Token = _jwt.CreateToken(user),
                Email = user.Email,
                Role = user.Role,
            });
        }

        // GET: api/auth/me
        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value
                ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)?.Value;
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            return Ok(new { email, role });
        }
    }
}
