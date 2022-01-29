using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Models
{
    public class StudentModel      // ca sa stie clientul ce trimite => un id si un nume (ajutatoare ca sa stim modelul care trb trimis de la frontend)
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
