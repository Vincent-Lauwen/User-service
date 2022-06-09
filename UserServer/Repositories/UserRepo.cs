using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserServer.Context;
using UserServer.Models;

namespace UserServer.Repositories
{
    public class UserRepo : IUserRepo
    {
        private ServerDbContext _context;
        private IConfiguration _config;

        public UserRepo(ServerDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public string RegisterUser(UserRegister userRegister)
        {
            if (_context.Users.Any(o => o.Name.ToLower() == userRegister.Name.ToLower() || o.Password == userRegister.Password))
                throw new Exception("User allready exists");

            var hashedPassword = Encrypt(userRegister.Password);
            
            if (hashedPassword == null)
                throw new Exception("Hashing password failed");

            User user = new User(Guid.NewGuid().ToString(), userRegister.Name, userRegister.Email, hashedPassword, userRegister.Website, userRegister.Profile_pic, userRegister.Bio, "user");
            _context.Users.Add(user);
            _context.SaveChanges();
            return GenerateToken(user);
        }

        public string LoginUser(UserLogin userLogin)
        {
            var currentUser = _context.Users.FirstOrDefault(o =>
                (o.Name.ToLower() == userLogin.UserNameOrEmail.ToLower()
                || o.Email.ToLower() == userLogin.UserNameOrEmail.ToLower())
                && o.Password == userLogin.Password);

            if (currentUser == null)
            {
                throw new Exception("No user found");
                
            }

            return GenerateToken(currentUser);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Website", user.Website),
                new Claim("Bio", user.Bio),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string Encrypt(string password)
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }


            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        private string Decrypt(string hash)
        {
            return "";
        }




        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(string id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
