namespace PlanningPoker.DataAccess.Entities
{
    public class SessionPlayer
    {
        public int SessionId { get; set; }

        public int UserId { get; set; }


        #region Navigation properties

        public Session Session { get; set; }

        public User User { get; set; }

        #endregion
    }
}
