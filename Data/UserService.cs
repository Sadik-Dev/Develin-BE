using Backend_Develin.Helpers;
using Backend_Develin.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Develin.Data
{
    public class UserService : IUserService
    {
        private readonly ProjectContext _context;
        private IEnumerable<Employee> _users;
        private readonly AppSettings _appSettings;

        public UserService(ProjectContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _users = context.Employees;
            _appSettings = appSettings.Value;

        }

        public Employee Authenticate(string username, string password)
        {
            bool isManager; 
            string passEncypted =  Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

            var user = _users.SingleOrDefault(x => x.Email == username && x.Password == passEncypted);

            // return null if user not found
            if (user == null)
                return null;

            //Check if he is manager
            isManager = user.GetType().Name.Equals("Manager")
                ? true : false;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            Claim claim;

            if (isManager)
                claim = new Claim(ClaimTypes.Role, "Manager");
            else
                claim = new Claim(ClaimTypes.Role, "Worker");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    claim,
                     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())

        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            Console.WriteLine("log: " + claim);
            return user;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _users;

        }
    }
}
