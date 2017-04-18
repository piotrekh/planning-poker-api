using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace PlanningPoker.DataAccess.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }


        #region Navigation properties

        public ICollection<SessionPlayer> Sessions { get; set; }

        public ICollection<GameEstimate> Estimates { get; set; }

        #endregion
    }
}
