using InscrieriStudenti.Entities;
using InscrieriStudenti.Models;
using InscrieriStudenti.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Managers
{
    public class StudentiManager : IStudentiManager
    {
        private readonly IStudentiRepository studentiRepository;

        public StudentiManager(IStudentiRepository repository)  // acum controllerul are instanta la manager care are instanta la repository
        {
            this.studentiRepository = repository;
        }


        public List<Studenti> GetStudenti()
        {
            return studentiRepository.GetStudenti().ToList();
        }


        public List<int> GetIduriStudenti()
        {
            return studentiRepository.GetIduriStudenti().ToList();
        }


        public List<Studenti> GetStudentiWithJoin()
        {
            return studentiRepository.GetStudentiWithJoin().ToList();
        }


        public List<StudentiPrimulCursModel> GetStudentiFiltered()
        {
            var studenti = studentiRepository.GetStudenti(); // inca nu il vreau ca lista ca sa i mai pot aplica filtrari sql
            var studentiFiltered = studenti.Where(x => x.CursStudent.Count > 0)
                .Select(x => new StudentiPrimulCursModel { Id = x.Id, PrimulCursSauNull = x.CursStudent.FirstOrDefault().IdCurs })
                .ToList();
            return studentiFiltered;
        }


        public List<StudentiPrimulCursModel> GetStudentiFilteredOrdered()
        {
            var studentiFiltered = GetStudentiFiltered();   // folosesc direct metoda de mai sus
            var studentiFilteredOrdered = studentiFiltered
                .OrderBy(x => x.PrimulCursSauNull) 
                .OrderByDescending(x => x.Id) 
                .ToList();
            return studentiFilteredOrdered;
        }


        public async Task Create(Studenti student)
        {
            await studentiRepository.Create(student);
        }


        public async Task Update(StudentModel studentCreationModel)
        {
            var studenti = studentiRepository
                .GetStudenti()
                .FirstOrDefault(x => x.Id == studentCreationModel.Id);

            studenti.Nume = studentCreationModel.Name; // updatez schimbandu-i numele
            await studentiRepository.Update(studenti);
        }


        public async Task Delete(Studenti student)
        {
            await studentiRepository.Delete(student);
        }
    }
}
