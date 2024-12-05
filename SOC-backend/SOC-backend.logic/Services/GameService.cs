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
        private readonly ICardRepository _cardRepository;

        public GameService(IGameRepository gameRepository, ICardRepository cardRepository)
        {
            _gameRepository = gameRepository;
            _cardRepository = cardRepository;
        }

        public async Task<GameState> StartNewGame(int playerId)
        {
            var cards = await _cardRepository.GetAllCards();
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
