using DataAccess.Entities;
using DataAccess.Repositories;

namespace DataAccess.Services
{
    public class AuthenticationService
    {
        public User LoggedUser { get; set; }

        public void AuthenticateUser(string username, string password)
        {
            UserRepository userRepo = new UserRepository();

            LoggedUser = userRepo.GetFirst(u => u.Username == username && u.Password == password);
        }
    }
}
