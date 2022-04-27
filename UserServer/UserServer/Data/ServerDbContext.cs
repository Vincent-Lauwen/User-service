using Microsoft.EntityFrameworkCore;
using UserServer.Models;

namespace UserServer.Context
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
