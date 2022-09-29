using System.Linq;
using System;
using MyFace.Models.Database;
using MyFace.Helpers;

namespace MyFace.Repositories
{
    public interface IAuthRepo
        {
            bool ValidateUsernamePassword(string Username, string Password);
        }
    public class AuthRepo : IAuthRepo
    {
        private readonly MyFaceDbContext _context;

        public AuthRepo(MyFaceDbContext context)
        {
            _context = context;
        }
        public bool ValidateUsernamePassword(string username, string password)
        {
            User user;
            try
            {
                user = _context.Users.Where(u => u.Username == username).Single();
            }
          
            catch(InvalidOperationException e)
            {
                throw (e);
            }

            string hash = PasswordHelper.GenerateSaltedHash( password,user.Salt);
            Console.WriteLine(hash);

            if(user.HashedPassword == hash)
            {
                return true;
            }

            return false;
        }
    }

}