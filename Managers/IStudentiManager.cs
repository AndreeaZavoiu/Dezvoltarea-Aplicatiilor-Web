using InscrieriStudenti.Entities;
using InscrieriStudenti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Managers
{
    public interface IStudentiManager
    {
        // returneaza liste
        List<Studenti> GetStudenti(); // functionalitatea de a lua studenti
        List<int> GetIduriStudenti();
        List<Studenti> GetStudentiWithJoin();
        List<StudentiPrimulCursModel> GetStudentiFiltered();  
        List<StudentiPrimulCursModel> GetStudentiFilteredOrdered();
        Task Create(Studenti student);
        Task Update(StudentModel student);
        Task Delete(Studenti student);

    }
}
