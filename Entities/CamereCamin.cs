using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Entities
{
    public class CamereCamin
    {
        public int Id { get; set; }
        public int IdStudent { get; set; }
        public int Etaj { get; set; }
        public int Suprafata { get; set; }

        public Studenti Studenti { get; set; } // ca sa putem referentia studentul prin IdStudent in cod

    }
}
