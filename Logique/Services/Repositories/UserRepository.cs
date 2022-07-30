using Logique.Models.Entities;
using Logique.Services.Interfaces;
using TeleCentre.Web.Portal.Models;
using System.Security.Cryptography;
using System.Text;

namespace Logique.Services.Repositories
{
    public class UserRepository : IUserInterface
    {
        private readonly LogiqueDBContext _db;

        public UserRepository(LogiqueDBContext db)
        {
            this._db = db;
        }
        public IEnumerable<User> GetUsers()
        {
            return _db.users.ToList();
        }

        public User Register(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.Password = HashPassword(user.Password); ;
            user.ConfirmPassword = HashPassword(user.ConfirmPassword);
            _db.users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User? Login(User user)
        {
            if (_db.users.Where(u => u.Email.Equals("admin@demo.com")).FirstOrDefault() == null)
            {
                User add = new User();
                user.Email = "admin@demo.com";
                user.LastName = "admin";
                user.Password = HashPassword("Admin@123");
                user.ConfirmPassword = HashPassword("Admin@123");
                _db.users.Add(add);
                _db.SaveChanges();
            }

            user.Password = HashPassword(user.Password);
            var obj = _db.users.Where(u => u.Email.Equals(user.Email) && u.Password.Equals(user.Password)).FirstOrDefault();
            if (obj == null)
            {
                return null;
            }

            obj.Password = string.Empty;
            return obj;
        }

        public User? ExistUser(string email)
        {
            var obj = _db.users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            if (obj == null)
            {
                return null;
            }
            return obj;
        }

        #region Helper

        public string HashPassword(string password)
        {
            var pwdarray = Encoding.ASCII.GetBytes(password);
            var sha1 = new SHA1CryptoServiceProvider();
            var hash = sha1.ComputeHash(pwdarray);
            var hashpwd = new StringBuilder(hash.Length);
            foreach (byte b in hash)
            {
                hashpwd.Append(b.ToString());
            }
            return hashpwd.ToString();
        }

        #endregion
    }
}
