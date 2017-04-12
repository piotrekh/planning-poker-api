using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace PlanningPoker.DataAccess.Entities
{
    public class User : IdentityUser<int>
    {
        #region Navigation properties

        public ICollection<SessionPlayer> Sessions { get; set; }

        public ICollection<GameEstimate> Estimates { get; set; }

        #endregion
    }
}
