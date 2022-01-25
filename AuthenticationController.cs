using InscrieriStudenti.Entities;
using InscrieriStudenti.Managers;
using InscrieriStudenti.Models;
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
    public class AuthenticationController : ControllerBase
    {
        public static List<User> User = new List<User>
        {
            new User { UserName = null}
        };

        private readonly IAuthenticationManager authenticationManager;

        public AuthenticationController(IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] RegisterModel registerModel)
        {
            await authenticationManager.SignUp(registerModel);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var tokens = await authenticationManager.Login(loginModel);
            if (tokens != null) return Ok(tokens);
            else return BadRequest("Failed to login.");
        }



        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(User);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] User user)
        {
            User.Add(user);
            return Ok(User);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var userToUpdate = User.FirstOrDefault(x => x.UserName == user.UserName);
            userToUpdate.UserName = user.UserName;
            return Ok(User);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var userToUpdate = User.FirstOrDefault(x => x.Id == id);
            User.Remove(userToUpdate);
            return Ok(User);
        }





        /*
        [HttpGet]
        public async Task<IActionResult> GetUseri()
        {
            return Ok(User);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            User.Create(user);
            return Ok(User);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var cursToUpdate = User(x => x.Id == user.Id);
            cursToUpdate.Username = user.UserName;
            return Ok(User);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var cursToRemove = Cursuri.FirstOrDefault(x => x.Id == id);
            Cursuri.Remove(cursToRemove);
            return Ok(Cursuri);
        }*/
    }
}
