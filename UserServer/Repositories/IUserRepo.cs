using UserServer.Models;

namespace UserServer.Repositories
{
    public interface IUserRepo
    {
        List<User> GetAll();
        List<User> GetUserById(string id);
        string RegisterUser(UserRegister user);
        User UpdateUser(User user);
        string LoginUser(UserLogin user);
    }
}
