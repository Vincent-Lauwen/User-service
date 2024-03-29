﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace UserServer.Models
{
    public class User
    {
        public User(string id, string name, string email, string password, string website, Blob profile_pic, string bio, string role)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Website = website;
            Profile_pic = profile_pic;
            Bio = bio;
            Role = role;
        }

        public User()
        {
        }

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Website { get; set; }
        [NotMapped]
        public Blob Profile_pic { get; set; }
        public string Bio { get; set; }
        public string Role { get; set; }
        
    }
}
