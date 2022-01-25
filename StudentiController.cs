using InscrieriStudenti.Entities;
using InscrieriStudenti.Managers;
using InscrieriStudenti.Models;
using InscrieriStudenti.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentiController : ControllerBase
    {
        private IStudentiManager manager;   // acum controllerul are instanta la manager care are instanta la repository
        public StudentiController(IStudentiManager studentiManager) // la crearea controllerului, inregistrez serviciul pt manager
        {
            this.manager = studentiManager;
        }
        // acum in controller trb sa am doar request catre manager, fara logica sql


        ///////////////////////////  Actiunile principale de a lua informatii  //////////////////////////////////////
        [HttpGet]
        [Authorize(Policy = "BasicUser")]
        public async Task<IActionResult> GetStudenti()
        {
            var studenti = manager.GetStudenti();   //  => lista de studenti

            return Ok(studenti); // nu vreau sa stau aici cu frica ca mi a murit contextul (din cauza lui using din IQueryable) => de asta il pun ca prop cu 'private readonly StudentiContext db;'  =>  mutat in repository
        }

        // de asta nu mai am nev pt ca am mutat-o in repository
        /*public IQueryable<Studenti> GetStudentiIQueryable()
        {
            // using var db = new StudentiContext(); // using face ca db sa se distruga la incheierea executiei blocului de functiei
            var studenti = db.Studenti;
            return studenti;
        }*/


        // implementate in repository

        [HttpGet("select")]
        // eager loading
        public async Task<IActionResult> GetIduriStudentiEager()
        {
            // dupa implementarile din manager si repository, nu mai am nev de astea
            // var db = new StudentiContext();
            // var idstudenti = db.Studenti.Select(x => x.Id).ToList(); // echivalent cu select id from studenti;
            
            var idstudenti = manager.GetIduriStudenti();
            return Ok(idstudenti);                          // incarc in memorie id-urile
        }

        /* 
            // pe astea nu le mai vreau, le las comentate
        [HttpGet("select-lazy")]
        // lazy loading -> nu e recomandat in multe cazuri
        // am instalat proxies pt asta si toate metodele din tabele trb sa fie virtuale, iar in connection string trb modif MultipleActiveResultSets = True
        public async Task<IActionResult> GetIduriStudentiLazy()
        {
            var db = new StudentiContext();
            var idstudenti = db.Studenti.Select(x => x.Id).AsQueryable(); // doar cand va avea nevoie mai jos in cod, va face query-ul
                                                                          // asta nu incarca in variabila, nu efectueaza selectul (se poate obs in consola acest aspect)
            return Ok(idstudenti); // doar cand mai jos in cod vom avea nev de ce se ret aici, se va executa comanda de select
        }


        [HttpGet("lazy-join")]
        // lazy loading
        // in connection string trb modif MultipleActiveResultSets = True
        public async Task<IActionResult> JoinLazy()
        {
            var db = new StudentiContext();
            var studenti = db.Studenti.AsQueryable(); 
            foreach(var x in studenti)
            {
                var y = x.CursStudent;
            }
            return Ok(studenti); 
        }
        */

        [HttpGet("eager-join")]
        // eager loading = incarcare in memorie
        // nu le putem fol pe ambele in acelasi timp!   => comentez .UseLazyLoadingProxies()
        public async Task<IActionResult> JoinEager()
        {
            /* var db = new StudentiContext();
            var studenti = db.Studenti.Include(x => x.CursStudent).ToList();*/
            var studenti = manager.GetStudentiWithJoin();
            foreach (var x in studenti)
            {
                var y = x.CursStudent;
            }
            return Ok(studenti);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter()
        {
            /* var db = new StudentiContext();
            var studenti = db.Studenti
                .Include(x => x.CursStudent)
                .Include(x => x.CamereCamin)
                .Where(x => x.CursStudent.Count > 0)
                .Select(x => new { Id = x.Id, PrimulCursSauNull = x.CursStudent.FirstOrDefault().IdCurs}) // select TOP(1) IdCurs from CursStudent
                .ToList();                                                          // adica selecteaza prima val din colectia asta din Studenti
            */
            var studenti = manager.GetStudentiFiltered();
            return Ok(studenti);
        }

        [HttpGet("orderby")]
        public async Task<IActionResult> OrderBy()
        {
            /* var db = new StudentiContext();
            var studenti = db.Studenti
                .Include(x => x.CursStudent)
                .Include(x => x.CamereCamin)
                .Where(x => x.CursStudent.Count > 0)
                .Select(x => new { Id = x.Id, PrimulCursSauNull = x.CursStudent.FirstOrDefault().IdCurs }) // select TOP(1) IdCurs from CursStudent
                .ToList()
                .OrderBy(x => x.PrimulCursSauNull) // studentii ordonati dupa primul curs din lista
                .OrderByDescending(x => x.Id) // exista si descresc
                .ToList();                                                          // adica selecteaza prima val din colectia asta din Studenti
            */
            var studenti = manager.GetStudentiFilteredOrdered();
            return Ok(studenti);
        }



        ///////////////////////////  Actiunile principale de a crea informatii  //////////////////////////////////////
        [HttpPost]
        public async Task<IActionResult> Create ([FromBody] string name) // aducem dinspre client informatii pt un nou student, conventie: din body
        {
            // var db = new StudentiContext();
            var newStudent = new Studenti
            {
                //Id = 1, // la mine nu merge cu asta tot din cauza lui set identity on
                Nume = name // am facut 4 post-uri si mi-a adaugat in BD 4 studenti (primii 3 cu numele "Popescu" ca asa am dat, nestiind ca merge sa salveze)
                            //  si ultimul cu numele Andrei iar id-urile lor de la 3 la 6
            };
            /* await db.Studenti.AddAsync(newStudent); // adaugam in cod, nu si in aplicatie 
            await db.SaveChangesAsync(); // si apoi salvam in bd, ca un commit
            */
            await manager.Create(newStudent);
            return Ok();
        }



        [HttpPost("withObj")]
        public async Task<IActionResult> Create([FromBody] StudentModel studentCreationModel)
        {
            // var db = new StudentiContext();
            var newStudent = new Studenti   // in tabela Studenti adaug
            {
                //Id = studentCreationModel.Id, // si aici imi da eroare daca vreau sa bag eu id-ul, dar cu el comentat imi merge adaugarea de nume, cu id pus automat
                Nume = studentCreationModel.Name
            };
            /* await db.Studenti.AddAsync(newStudent); 
            await db.SaveChangesAsync(); */
            await manager.Create(newStudent);
            return Ok();
        }


        [HttpPut("withObj")]   // a mers pt id-urile pe care le am deja salvate
        public async Task<IActionResult> Update([FromBody] StudentModel studentCreationModel)
        {
            /* var db = new StudentiContext();
            var student = db.Studenti.FirstOrDefault(x => x.Id == studentCreationModel.Id); // trimit din FrontEnd studentul cu un anumit Id (primul pe vare-l gaseste)
            student.Nume = studentCreationModel.Name;  // si vreau sa ii schimb numele
            db.Studenti.Update(student); // nu are echivalent asincron
            await db.SaveChangesAsync(); */
            await manager.Update(studentCreationModel);
            return Ok();
        }
    
    }
}
