using SOC_backend.logic.Interfaces.Data;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Models.Match;

namespace SOC_backend.logic.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<GameState> StartNewGame(int playerId)
        {
            GameState gameState = new GameState(playerId);
            await _gameRepository.CreateNewGame(gameState);
            return gameState;
        }
    }
}
