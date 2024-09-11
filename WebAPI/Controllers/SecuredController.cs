using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SecuredController : ControllerBase
    {
        // GET: api/<SecuredController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SecuredController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public string Get(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return identity.FindFirst(ClaimTypes.Name).Value;
        }

        // POST api/<SecuredController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SecuredController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SecuredController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
