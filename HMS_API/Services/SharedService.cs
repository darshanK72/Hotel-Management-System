using HMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace HMS_API.Services
{
    public class SharedService : ISharedService
    {
        private readonly HMSDbContext _context;

        public SharedService(HMSDbContext context)
        {
            _context = context;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _context.Users.Include(u => u.Role)
                                           .SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
            return user;
        }
    }
}
