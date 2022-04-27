using UserServer.Context;
using UserServer.Models;

namespace UserServer.Repositories
{
    public class UserRepo : IUserRepo
    {
        private ServerDbContext _context;

        public UserRepo(ServerDbContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            return (_context.Users.ToList());
        }

        public List<User> GetUserById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
