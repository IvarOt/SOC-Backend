using SOC_backend.logic.Interfaces;
using SOC_backend.logic.Interfaces.Data;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Models.Cards;
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
            List<Card> cards = new List<Card>();
            GameState gameState = new GameState(cards);
            await _gameRepository.CreateNewGame(gameState);
            return gameState;
        }

        public async Task<GameState> ResolveFight(int playerId)
        {
            GameState gameState = await _gameRepository.GetGameState(playerId);
            gameState.ResolveFightingStage();
            return gameState;
        }

        public async Task<GameState> StartNewRound(int playerId)
        {
            GameState gamestate = await _gameRepository.GetGameState(playerId);

            return gamestate;
        }
    }
}
