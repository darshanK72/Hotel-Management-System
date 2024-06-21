
using HMS_API.DTO;
using HMS_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS_API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize(Policy = "OwnerPolicy")]
    public class UserController : ControllerBase
    {
        private readonly HMSDbContext _context;

        public UserController(HMSDbContext context)
        {
            _context = context;
        }


        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/user/register
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO userDto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == userDto.Username))
            {
                return BadRequest("Username is already taken.");
            }

            if (await _context.Users.AnyAsync(u => u.Email == userDto.Email))
            {
                return BadRequest("Email is already registered.");
            }

            var role = await _context.Roles.FindAsync(userDto.RoleId);
            if (role == null)
            {
                return BadRequest("Invalid role specified.");
            }

            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                RoleId = userDto.RoleId,
                Role = role
            };

            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "A problem occurred while registering the user. Please try again.");
            }

            user.Password = null;

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        // PUT: api/user/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (await _context.Users.AnyAsync(u => u.Username == userDto.Username && u.UserId != id))
            {
                return BadRequest("Username is already taken.");
            }

            if (await _context.Users.AnyAsync(u => u.Email == userDto.Email && u.UserId != id))
            {
                return BadRequest("Email is already registered to another user.");
            }

            user.Username = userDto.Username;
            user.Email = userDto.Email;

            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                user.Password = userDto.Password;
            }

            if (user.RoleId != userDto.RoleId)
            {
                var newRole = await _context.Roles.FindAsync(userDto.RoleId);
                if (newRole == null)
                {
                    return BadRequest("Invalid role specified.");
                }
                user.RoleId = userDto.RoleId;
                user.Role = newRole;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/user/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
