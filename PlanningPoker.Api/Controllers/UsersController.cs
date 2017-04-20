using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Domain.Models.Users;
using PlanningPoker.Domain.Services;
using System.Net;
using PlanningPoker.Security.Attributes;
using PlanningPoker.Domain.Constants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanningPoker.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserManagerService _userManagerService;

        public UsersController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }
        
        [HttpPost]
        [ClaimAuthorize(Claims.CanManageUsers)]
        public async Task Post([FromBody] CreateUser data)
        {
            await _userManagerService.CreateUser(data);
        }        
    }
}
