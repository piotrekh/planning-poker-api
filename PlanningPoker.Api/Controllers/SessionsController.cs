using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Domain.Models.Sessions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanningPoker.Api.Controllers
{
    [Route("api/[controller]")]
    public class SessionsController : Controller
    {

        // POST api/values
        [HttpPost]
        public void Post([FromBody]CreateSession data)
        {
        }
    }
}
