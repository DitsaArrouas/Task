using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks;
using Tasks.Interfaces;
using Tasks.Services;

namespace Tasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private IUserHttp UserHttp;
        public UserController(IUserHttp UserHttp)
        {
            this.UserHttp = UserHttp;
        }

        
        // public ActionResult<String> Login([FromBody] User User)
        // {
        //     var dt = DateTime.Now;

        //     if (User.Name != "Wray"
        //     || User.Password != $"W{dt.Year}#{dt.Day}!")
        //     {
        //         return Unauthorized();
        //     }

        //     var claims = new List<Claim>
        //     {
        //         new Claim("type", "Admin"),
        //     };
        //     var token = TokenService.GetToken(claims);

        //     return new OkObjectResult(TokenService.WriteToken(token));
        // }
        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] User user)
        {
            var dt = DateTime.Now;
            user.Id = UserHttp.GetAll().First(u => u.Password.CompareTo(user.Password)==0).Id;
            if (user.Name== "Ilan"
            && user.Password == $"I{dt.Year}@{dt.Day}!")
            {
                var claims = new List<Claim> {
                    new Claim("type", "Admin"),
                    new Claim("id", user.Id),
                };
                var token = TokenService.GetToken(claims);
                return new OkObjectResult(TokenService.WriteToken(token));
            }
            else if (UserHttp.GetAll().First(u => u.Password.CompareTo(user.Password)==0) != null 
                && UserHttp.GetAll().First(u => u.Password.CompareTo(user.Password)==0).Name == user.Name)
            {
                var claims = new List<Claim> {
                    new Claim("type", "Agent"),
                    new Claim("id", user.Id),
                };
                var token = TokenService.GetToken(claims);

                return new OkObjectResult(TokenService.WriteToken(token));
            }
            else return Unauthorized();
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IEnumerable<User> Get()
        {
            return UserHttp.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult<User> Get(string id)
        {
            var userById = UserHttp.Get(id);
            if (userById == null)
                return NotFound();
             return userById;
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]

        public ActionResult Post(User user)
        {
            UserHttp.Add(user);

            return CreatedAtAction(nameof(Post), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult Put(string id, User user)
        {
            if (! UserHttp.Update(id, user))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete (string id)
        {
            if (! UserHttp.Delete(id))
                return NotFound();
            return NoContent();            
        }

    }
}


