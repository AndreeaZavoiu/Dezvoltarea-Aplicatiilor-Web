using InscrieriStudenti.Entities;
using InscrieriStudenti.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenManager tokenManager;
        public AuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager, ITokenManager tokenManager) // constructor
        {
            this.userManager = userManager;
            this.tokenManager = tokenManager;
        }
        private static string registerModel;

        public async Task SignUp(RegisterModel registerModel)
        {
            var user = new User
            {
                Email = registerModel.Email,
                Username = registerModel.Username // nu si parola!
            };

            var result = await userManager.CreateAsync(user, registerModel.Password); // creeaza un user si o parola
            if (result.Succeeded) // daca a reusit sa faca userul
            {
                await userManager.AddToRoleAsync(user, registerModel.Role); // ii adaug un rol
            }
        }

        public async Task<TokensModel> Login(LoginModel loginModel)
        {
            // e vreun user in baza pt emailul primit?
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            if (user != null)
            {
                var result = await signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false); 
                if (result.Succeeded)
                {
                    // returnez un token pt a-si autentifica request-urile => fol un manager pt functionalitatea de a crea useri
                    var token = await tokenManager.GenerateToken(user);
                    return new TokensModel
                    {
                        AccessToken = token
                    };
                }
            }
            return null; // daca nu a mers, returneaza null
        }


        /*
        public List<Studenti> GetStudenti()
        {
            return studentiRepository.GetStudenti().ToList();
        }
        public List<User> GetUseri()
        {
            return user
        }
        Task Create(User user);
        Task Update(User user);
        Task Delete(User user);
        */
    }
}
