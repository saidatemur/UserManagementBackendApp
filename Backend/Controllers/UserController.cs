using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementApp.Dtos;
using UserManagementApp.Models;
using System.Security.Cryptography;
using System.Text;
using UserManagementApp.Services;
namespace UserManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _context.Users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                LastLogin = u.LastLogin ?? DateTime.MinValue,
                RegisteredAt = u.RegistrationDate,
                Status = u.IsBlocked ? "blocked" : "active"
            }).ToListAsync();

            return Ok(users);
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            return Ok(new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                LastLogin = user.LastLogin ?? DateTime.MinValue,
                RegisteredAt = user.RegistrationDate,
                Status = user.IsBlocked ? "blocked" : "active"
            });
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(UserRegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email already exists");

            var newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, new UserDto
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                RegisteredAt = newUser.RegistrationDate,
                LastLogin = newUser.LastLogin ?? DateTime.MinValue,
                Status = "active"
            });
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserRegisterDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/User/delete
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUsers(UserActionDto dto)
        {
            if (dto.UserIds == null || !dto.UserIds.Any())
                return BadRequest("No user IDs provided.");

            var users = await _context.Users
                .Where(u => dto.UserIds.Contains(u.Id))
                .ToListAsync();

            if (!users.Any())
                return NotFound("No matching users found.");

            _context.Users.RemoveRange(users);
            await _context.SaveChangesAsync();

            return Ok("Selected users have been deleted.");
        }

        // POST: api/User/block
        [HttpPost("block")]
        public async Task<IActionResult> BlockUsers(UserActionDto dto)
        {
            var users = await _context.Users.Where(u => dto.UserIds.Contains(u.Id)).ToListAsync();
            foreach (var user in users)
                user.IsBlocked = true;

            await _context.SaveChangesAsync();
            return Ok("Selected users have been blocked.");
        }

        // POST: api/User/unblock
        [HttpPost("unblock")]
        public async Task<IActionResult> UnblockUsers(UserActionDto dto)
        {
            var users = await _context.Users.Where(u => dto.UserIds.Contains(u.Id)).ToListAsync();
            foreach (var user in users)
                user.IsBlocked = false;

            await _context.SaveChangesAsync();
            return Ok("Selected users have been unblocked.");
        }

    }
}
