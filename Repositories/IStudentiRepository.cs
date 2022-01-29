using InscrieriStudenti.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Repositories
{
    public interface IStudentiRepository   
    {
        // lista de functionalitati pe care le oferim unui serviciu (iar modul lor de implementare se afla in clasa StudentiRepository)
        IQueryable<Studenti> GetStudenti(); // functionalitatea de a lua studenti
        IQueryable<int> GetIduriStudenti();
        IQueryable<Studenti> GetStudentiWithJoin();
        Task Create(Studenti student);
        Task Update(Studenti student);
        Task Delete(Studenti student);
    }
}
