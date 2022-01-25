using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Entities
{
    public class ZiCurs
    {
        public int Id { get; set; } // 1 -> 7
        public string Nume { get; set; } // Luni -> Duminica
        public int IdCurs { get; set; }
        public ICollection<Cursuri> Cursuri { get; set; } // lista de cursuri ale unei zile
    }
}
