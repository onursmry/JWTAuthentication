using IsProje.Models;
using IsProje.Repo;
using Microsoft.EntityFrameworkCore;

namespace IsProje.Services.Interface
{
    public interface IUserService
    {
        Task<User> FindByEmailCheckPasswordAsync(string userName, string userPassword);
        
    }
}
