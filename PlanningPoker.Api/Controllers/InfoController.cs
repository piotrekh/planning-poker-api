using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanningPoker.Api.Controllers
{
    [Route("api/[controller]")]
    public class InfoController : Controller
    {
        [HttpGet("status")]
        public string Get()
        {
            return "The application is up and running";
        }        
    }
}
