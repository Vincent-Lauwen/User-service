using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace UserServer.Models
{
    public class User
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Website { get; private set; }
        [NotMapped]
        public Blob Profile_pic { get; private set; }
        public string Bio { get; private set; }
        public int Role { get; private set; }
        
    }
}
