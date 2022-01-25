using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Entities
{
    public class Ani
    {
        public int Id { get; set; }
        public int IdStudent { get; set; }
        public ICollection<Studenti> Studenti { get; set; } // fiecare an are o colectie de studenti

        // public Studenti Student { get; set; }
        public int IdCurs { get; set; }
        public ICollection<Cursuri> Cursuri { get; set; }  // fiecare an are o colectie de cursuri

        // public Cursuri Cursuri { get; set; }
    }
}
