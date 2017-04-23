using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Domain.Constants;
using PlanningPoker.Domain.Models.Sessions;
using PlanningPoker.Domain.Models.Users;
using PlanningPoker.Domain.Providers.Context;
using PlanningPoker.Domain.Services;
using PlanningPoker.Security.Attributes;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanningPoker.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserManagerService _userManagerService;
        private readonly ISessionsService _sessionsService;
        private readonly IUserContextProvider _userContextProvider;

        public UsersController(IUserManagerService userManagerService, 
                               ISessionsService sessionsService,
                               IUserContextProvider userContextProvider)
        {
            _userManagerService = userManagerService;
            _sessionsService = sessionsService;
            _userContextProvider = userContextProvider;
        }
        
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ClaimAuthorize(Claims.CanManageUsers)]
        public async Task Post([FromBody] CreateUser data)
        {
            await _userManagerService.CreateUser(data);
        }        

        [HttpGet("{userId}/Sessions")]
        [ProducesResponseType(typeof(List<SessionWithGames>), (int)HttpStatusCode.OK)]  
        [Authorize]
        public IActionResult GetUserSessions([FromRoute] int userId)
        {
            //in current version of api, the user calling this method
            //(even administrator) can access only their own sessions
            if (userId != _userContextProvider.Id.Value)
                return Forbid();

            List<SessionWithGames> sessions = _sessionsService.GetUserSessions(userId);
            return Ok(sessions);
        }
    }
}
