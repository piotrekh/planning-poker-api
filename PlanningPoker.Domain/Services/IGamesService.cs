namespace PlanningPoker.Domain.Services
{
    public interface IGamesService
    {
        /// <summary>
        /// Begins a new game if there is no unfinished game
        /// and returns its id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>Id of the new game</returns>
        int BeginGame(int sessionId);
    }
}
