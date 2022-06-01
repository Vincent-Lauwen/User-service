using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace UserServer.Models
{
    public class UserRegister
    {
        public UserRegister(string name, string email, string password, string website, Blob profile_pic, string bio)
        {
            Name = name;
            Email = email;
            Password = password;
            Website = website;
            Profile_pic = profile_pic;
            Bio = bio;
        }

        public UserRegister()
        {
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Website { get; set; }
        [JsonIgnore]
        [NotMapped]
        public Blob Profile_pic { get; set; }
        public string Bio { get; set; }
    }
}
