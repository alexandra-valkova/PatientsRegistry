using DataAccess.Entities;
using DataAccess.Repositories;

namespace DataAccess.Services
{
    public class AuthenticationService
    {
        private readonly UserRepository userRepository;

        public User LoggedUser { get; set; }

        public AuthenticationService()
        {
            userRepository = new UserRepository();
        }

        public void AuthenticateUser(string username, string password)
        {
            LoggedUser = userRepository.GetFirst(user => user.Username == username && user.Password == password);
        }
    }
}
