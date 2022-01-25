using InscrieriStudenti.Entities;
using InscrieriStudenti.Managers;
using InscrieriStudenti.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InscrieriStudenti
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // practic asa se initializeaza contextul (pt situatii mai complicare -> lab4) => ca un serviciu => acum il pot injecta in controller = a adauga ca proprietate intr-o clasa un obiect care e de fapt un serviciu
            services.AddDbContext<StudentiContext>(options => options
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .UseSqlServer("Server=(localdb)\\ProjectsV13;Initial Catalog=InscrieriStudenti;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")); // ii specific contextului sa foloseasca SqlServer
                                                                                                                                                                                                                                                // => e bagat asa si UseSqlServer cu ce ne trb pt bd

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("StudScheme", options =>  // setari pt a valida token-ul in login-ul meu:
                {
                    options.SaveToken = true;
                    var secret = Configuration.GetSection("Jwt").GetSection("SecretKey").Get<String>();
                    // cum validez token-ul pe care il primesc request-urile:
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,    // verific ca e ok fol aceeasi cheie
                        ValidateLifetime = true,    // verific daca mai e valid token-ul, daca nu i s-a scurs timpul dat
                        RequireExpirationTime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("BasicUser", policy => policy.RequireRole("BasicUser").RequireAuthenticatedUser().AddAuthenticationSchemes("StudeScheme").Build());
            });

            services.AddTransient<IStudentiRepository, StudentiRepository>(); // inregistrez repository-ul (serviciul) sa fie folosit si oriunde e injectat se reinit separat pt fiecare clasa
            /* 
            services.AddScoped<IStudentiRepository, StudentiRepository>(); // daca dau un request, va fi aceeasi instanta pe toata durata sa
            services.AddSingleton<IStudentiRepository, StudentiRepository>(); // o singura instanta pt toata aplicatia (indiferent de request si instanta)
            */
            services.AddTransient<IStudentiManager, StudentiManager>(); // inregistrez managerul de studenti
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            services.AddTransient<ITokenManager, TokenManager>();

            services.AddIdentity<User, Role>().AddEntityFrameworkStores<StudentiContext>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InscrieriStudenti", Version = "v1" }); // swagger ca serviciu pt a ne ajuta sa facem requesturi
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InscrieriStudenti v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
