using InscrieriStudenti.Entities;
using InscrieriStudenti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Managers
{
    public interface IAuthenticationManager
    {
        Task SignUp(RegisterModel registerModel);
        Task<TokensModel> Login(LoginModel loginModel);
        /*
        List<User> GetUseri();
        Task Create(User user);
        Task Update(User user);
        Task Delete(User user);*/
    }
}
