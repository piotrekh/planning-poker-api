namespace PlanningPoker.DataAccess.Entities
{
    public class GameEstimate
    {
        public int GameId { get; set; }

        public int UserId { get; set; }

        public int Estimate { get; set; }


        #region Navigation properties

        public Game Game { get; set; }

        public User User { get; set; }

        #endregion
    }
}
