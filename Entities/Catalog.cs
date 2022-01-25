using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Entities
{
    public class Catalog        // tabela asociativa de la relatia M:M dintre Cursuri si Studenti
    {
        public int IdStudent { get; set; }
        public Studenti Studenti { get; set; }

        public int IdCurs { get; set; }
        public Cursuri Cursuri { get; set; }  // fiecare an are o colectie de cursuri
        public int An { get; set; }
    }
}
