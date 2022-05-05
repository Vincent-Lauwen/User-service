using UserServer.Models;

namespace UserServer.Repositories
{
    public interface IUserRepo
    {
        List<User> GetAll();
        List<User> GetUserById(string id);
        void CreateUser(User user);
    }
}
