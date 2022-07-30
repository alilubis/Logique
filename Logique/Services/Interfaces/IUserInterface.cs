using Logique.Models.Entities;

namespace Logique.Services.Interfaces
{
    public interface IUserInterface
    {
        IEnumerable<User> GetUsers();
        User Register(User user);
        User? Login(User user);
        User? ExistUser(string email);
    }
}
