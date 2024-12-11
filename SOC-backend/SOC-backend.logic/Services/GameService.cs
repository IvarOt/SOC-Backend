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
            GameState gameState = new GameState(cards, playerId);
            await _gameRepository.CreateNewGame(gameState);
            return gameState;
        }

        public async Task<GameState> PurchaseCard(int cardId, int playerId)
        {
            GameState gameState = await _gameRepository.GetGameState(playerId);
            var card = await _cardRepository.GetCard(cardId);
            gameState.BuyCard(card);
            await _gameRepository.UpdateGame(gameState);
            return gameState;
        }

        public async Task<GameState> ResolveFight(int playerId)
        {
            GameState gameState = await _gameRepository.GetGameState(playerId);
            gameState.ResolveTurn();
            await _gameRepository.UpdateGame(gameState);
            return gameState;
        }

        public async Task Surrender(int playerId)
        {
            GameState gameState = await _gameRepository.GetGameState(playerId);
            gameState.Surrender();
            await EndGame(gameState);
        }

        public async Task EndGame(GameState gameState)
        {
            FinishedMatch finishedMatch = new FinishedMatch(gameState.Players[0].IsWin, gameState.Players[1].Name, gameState.PlayerId);
            await _gameRepository.EndGame(gameState, finishedMatch);
        }

        public async Task EndGame(int playerId)
        {
            GameState gameState = await _gameRepository.GetGameState(playerId);
            FinishedMatch finishedMatch = new FinishedMatch(gameState.Players[0].IsWin, gameState.Players[1].Name, gameState.PlayerId);
            await _gameRepository.EndGame(gameState, finishedMatch);
        }

        public async Task<GameState> PassTurn(int playerId)
        {
            GameState gameState = await _gameRepository.GetGameState(playerId);
            gameState.PassTurn();
            await _gameRepository.UpdateGame(gameState);
            return gameState;
        }
    }
}
