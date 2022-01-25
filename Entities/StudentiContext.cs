using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscrieriStudenti.Entities
{
    public class StudentiContext : IdentityDbContext  // extind cu asta (lab5)
    {
        public DbSet<Studenti> Studenti { get; set; }

        public StudentiContext(DbContextOptions<StudentiContext> options) : base(options) { } // constructor pt context, va folosi optiunile din serviciul AddDbContext din StartUp 
        // pt ca am fol new (in controller) pt a initializa contextul pt studenti, am suprascris aceasta metoda OnConfiguring pt a o fol si am comentat-o pe cea de deasupra (StudentiContext(DbContextOptions<StudentiContext> options)) + services.Add
        
        // lab 4: nu mai am nev de OnConfiguring dupa injectarea contextului in controller => comentez dar dau UseLoggerFactory si UseSqlServer in 
        // dar am nev inapoi de controllerul StudentiContext(DbContextOptions<StudentiContext> options) => il decomentez
        
        /*protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) // asta a fost configuratia inainte de dependency injection (lab4)
        {
            optionsBuilder
                //.UseLazyLoadingProxies()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .UseSqlServer("Server=(localdb)\\ProjectsV13;Initial Catalog=InscrieriStudenti;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }*/

        public DbSet<CamereCamin> CamereCamin { get; set; } // il am si aici chiar daca e referentiat de Studenti
        public DbSet<Cursuri> Cursuri { get; set; }
        public DbSet<ZiCurs> ZiCurs { get; set; }
        public DbSet<Ani> Ani { get; set; }

   
        
        // pt anumite cazuri cand va trebui sa configuram optiunile contextului
        
        protected override void OnModelCreating(ModelBuilder builder) // suprascriem metoda
        {
            base.OnModelCreating(builder);

            builder.Entity<Studenti>()      // entitatea are o anume configuratie ca sa-si dea seama de foreign key-urile din tabele
                .HasOne(a => a.CamereCamin)     // ONE - TO - ONE
                .WithOne(aa => aa.Studenti)
                .HasForeignKey<CamereCamin>(c => c.IdStudent);

            builder.Entity<ZiCurs>()
                .HasMany(a => a.Cursuri)     // ONE - TO - MANY
                .WithOne(b => b.ZiCurs);

            builder.Entity<Catalog>()
                .HasKey(c => new { c.IdStudent, c.IdCurs });

            builder.Entity<Catalog>()
                .HasOne<Studenti>(a => a.Studenti)  // catalogul are un student           (la hover: CTRL + click pt a merge in clasa Studenti)
                .WithMany(b => b.CursStudent)       // care are mai multe cursuri 
                .HasForeignKey(c => c.IdStudent);

            builder.Entity<Catalog>()
                .HasOne<Cursuri>(a => a.Cursuri)  // catalogul are un curs
                .WithMany(b => b.CursStudent)       // care are mai multi studenti
                .HasForeignKey(c => c.IdCurs);
        }
    }
}
