using InscrieriStudenti.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InscrieriStudenti.Managers
{
    public class TokenManager: ITokenManager
    {
        private readonly IConfiguration configuration; // ajuta la apelarea secret key-ului din appsetting.json
        private readonly UserManager<User> userManager;
        //private readonly ITokenManager tokenManager; // 
        public TokenManager(IConfiguration configuration, UserManager<User> userManager) //, ITokenManager tokenManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            //this.tokenManager = tokenManager;
        }

        // token-ul e un string care are la baza o cheie pe care doar aplicatia noastra o stie => daca e descoperita, s-a dus securitatea aplicatiei
        public async Task<String> GenerateToken(User user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>(); // claim-uri date de security
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // adaug mai multe claim-uri in fct de cate roluri are userul in aplicatie
            }

            var secretKey = configuration.GetSection("Jwt").GetSection("SecretKey").Get<String>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(2), // expira in 2h, cu cat mai repede cu atat mai bine
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
