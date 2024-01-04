using TWADotNetCore.ATMWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace TWADotNetCore.ATMWebApp.Service
{
    public class UserService
    {
        private readonly AtmDbContext _context;

        public UserService(AtmDbContext context)
        {
            _context = context;
        }

        public int Save(UserModel user)
        {
            _context.User.Add(user);
            _context.SaveChanges();

            return user.Id;
        }

        public UserModel GetUser(int id) 
        {
            UserModel user = _context.User.Find(id);
            return user;
        }
    }
}
