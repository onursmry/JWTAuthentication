using IsProje.Data;
using IsProje.Models;
using IsProje.Repo;
using IsProje.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace IsProje.Services
{
    public class UserService:IUserService

    {
        private readonly ProjectContext _context;
        private readonly IConfiguration _config;
        public UserService(ProjectContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task <User> FindByEmailCheckPasswordAsync(string userName, string userPassword)
        {
            string hashedPassword = Method.Sha256_hash(userPassword);
            return await _context.Users.FirstOrDefaultAsync(x=>x.UserName==userName && x.UserPassword==hashedPassword);
        }
    }
}
