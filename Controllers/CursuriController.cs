using InscrieriStudenti.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursuriController : ControllerBase
    {
        public static List<Cursuri> Cursuri = new List<Cursuri>
        {
            new Cursuri { Id = 11, Nume = "Algebra", CursStudent = null, ZiCurs = null},
            new Cursuri { Id = 12, Nume = "Programare functionala", CursStudent = null, ZiCurs = null},
            new Cursuri { Id = 13, Nume = "Machine Learning", CursStudent = null, ZiCurs = null}

        };

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var curs = Cursuri.FirstOrDefault(x => x.Id == id);
            return Ok(curs);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(Cursuri);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Cursuri curs)
        {
            Cursuri.Add(curs);
            return Ok(Cursuri);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Cursuri curs)
        {
            var cursToUpdate = Cursuri.FirstOrDefault(x => x.Id == curs.Id);
            cursToUpdate.Nume = curs.Nume;
            return Ok(Cursuri);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var cursToRemove = Cursuri.FirstOrDefault(x => x.Id == id);
            Cursuri.Remove(cursToRemove);
            return Ok(Cursuri);
        }
    }
}
