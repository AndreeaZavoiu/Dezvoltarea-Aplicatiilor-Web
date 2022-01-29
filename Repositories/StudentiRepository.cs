using InscrieriStudenti.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Repositories
{
    // clasa valida sa foloseasca baza de date pt ca are injectat contextul, deci practic acum repository-ul injecteaza contextul, nu controllerul
    public class StudentiRepository : IStudentiRepository // tot ce contine interfata trb implementat in repository
    {
        private readonly StudentiContext db;

        public StudentiRepository(StudentiContext db) // constructor
        {
            this.db = db; // initializare in constructor
        }

        public IQueryable<Studenti> GetStudenti()   // acum fol metoda asta in controller peste tot pe unde am db.Studenti
        {
            var studenti = db.Studenti;
            return studenti;
        }

        public IQueryable<int> GetIduriStudenti()   // acum fol metoda asta in controller peste tot pe unde am db.Studenti
        {
            var iduriStudenti = db.Studenti.Select(x => x.Id);
            return iduriStudenti;
        }

        public IQueryable<Studenti> GetStudentiWithJoin()                   // nush daca e ok
        {
            var studentiJoin = db.Studenti
                .Include(x => x.CursStudent)
                .Include(x => x.CamereCamin);
            
                return studentiJoin;
        }

        public async Task Create(Studenti student)
        {
            await db.Studenti.AddAsync(student);
            await db.SaveChangesAsync();
        }

        public async Task Update(Studenti student)
        {
            db.Studenti.Update(student);
            await db.SaveChangesAsync();
        }

        public async Task Delete(Studenti student)
        {
            db.Studenti.Remove(student);
            await db.SaveChangesAsync();
        }
    }
}
