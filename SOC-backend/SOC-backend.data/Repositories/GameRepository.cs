using Microsoft.EntityFrameworkCore;
using SOC_backend.logic.Interfaces.Data;
using SOC_backend.logic.Models.Match;

namespace SOC_backend.data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateNewGame(GameState gameState)
        {
            await _context.GameState.AddAsync(gameState);
            await _context.SaveChangesAsync();
        }

        public async Task<GameState> GetGameState(int playerId)
        {
            GameState? gameState = await _context.GameState.Where(x => x.PlayerId == playerId).FirstOrDefaultAsync();
            if (gameState == null)
            {
                throw new Exception();
            }
            return gameState;
        }
    }
}
