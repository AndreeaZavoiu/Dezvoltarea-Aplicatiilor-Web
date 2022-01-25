using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Entities
{
    public class Studenti
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public CamereCamin CamereCamin { get; set; } // sa putem lua date si face mai rapid join intre tabele
                                                     // public Ani An { get; set; }
        public ICollection<Catalog> CursStudent { get; set; }  // fiecare student are o colectie de cursuri la care participa
    }
}
