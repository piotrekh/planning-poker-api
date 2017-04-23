using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Domain.Constants;
using PlanningPoker.Domain.Models.Sessions;
using PlanningPoker.Domain.Providers.Context;
using PlanningPoker.Domain.Services;
using PlanningPoker.Security.Attributes;
using System.Net;

namespace PlanningPoker.Api.Controllers
{
    [Route("api/[controller]")]
    public class SessionsController : Controller
    {
        private readonly ISessionsService _sessionsService;
        private readonly IUserContextProvider _userContextProvider;

        public SessionsController(ISessionsService sessionsService, IUserContextProvider userContextProvider)
        {
            _sessionsService = sessionsService;
            _userContextProvider = userContextProvider;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ClaimAuthorize(Claims.CanManageSessions)]
        public void CreateSession([FromBody]CreateSession data)
        {
            _sessionsService.CreateSession(_userContextProvider.Id.Value, data);
        }
    }
}
