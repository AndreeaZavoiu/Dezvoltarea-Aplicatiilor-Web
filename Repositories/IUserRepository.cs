using InscrieriStudenti.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> GetUser();
        Task Create(User user);
        Task Update(User user);
        Task Delete(User user);
    }
}
