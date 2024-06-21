using HMS_API.Models;

namespace HMS_API.Services
{
    public interface ISharedService
    {
        Task<User> Authenticate(string username, string password);
    }
}
