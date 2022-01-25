using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Entities
{
    public class Cursuri
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        
        // public Ani An { get; set; }
        public ICollection<Catalog> CursStudent { get; set; }    // fiecare curs are o colectie de studenti care-l frecventeaza
        public ZiCurs ZiCurs { get; set; }
    }
}
