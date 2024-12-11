using Moq;
using SOC_backend.logic.Interfaces;
using SOC_backend.logic.Interfaces.Data;
using SOC_backend.logic.Models.Cards;
using SOC_backend.logic.Models.Match;
using SOC_backend.logic.Services;
using SOC_backend.test.TestObjects;

namespace SOC_backend.test.Services
{
    [TestClass]
    [TestCategory("Unit")]
    public class GameServiceTests
    {
        private Mock<IGameRepository> _mockGameRepository;
        private Mock<ICardRepository> _mockCardRepository;
        private GameService _gameService;
        private CardObjects _cardObjects;
        private GameStateObjects _gameStateObjects;

        [TestInitialize]
        public void Setup()
        {
            _mockGameRepository = new Mock<IGameRepository>();
            _mockCardRepository = new Mock<ICardRepository>();
            _gameService = new GameService(_mockGameRepository.Object, _mockCardRepository.Object);
            _cardObjects = new CardObjects();
            _gameStateObjects = new GameStateObjects();
        }

        [TestMethod]
        public async Task StartNewGame_SuccesfullyCreatesGame()
        {
            //Arrange
            _mockCardRepository.Setup(x => x.GetAllCards()).ReturnsAsync(_cardObjects.testCards);

            //Act
            var result = await _gameService.StartNewGame(1);

            //Assert
            Assert.AreEqual(2, result.Players.Count);
            Assert.AreEqual(1, result.PlayerId);

            foreach (var player in result.Players)
            {
                Assert.AreEqual(30, player.HP);
                Assert.IsNotNull(player.Shop);
                Assert.AreEqual(1, player.Coins);
            }
            _mockCardRepository.Verify(x => x.GetAllCards(), Times.Once);
            _mockGameRepository.Verify(x => x.CreateNewGame(It.IsAny<GameState>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task StartNewGame_PlayerAlreadyHasGame()
        {
            //Arrange
            _mockGameRepository.Setup(x => x.GetGameState(1)).ReturnsAsync(_gameStateObjects.testGameState);

            //Act
            var result = await _gameService.StartNewGame(1);

            //Assert
            Assert.IsNull(result);
            _mockCardRepository.Verify(x => x.GetAllCards(), Times.Never);
            _mockGameRepository.Verify(x => x.CreateNewGame(It.IsAny<GameState>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task StartNewGame_NoCardsAvailable()
        {
            //Arrange
            _mockCardRepository.Setup(x => x.GetAllCards()).ReturnsAsync(new List<Card>());

            //Act
            var result = await _gameService.StartNewGame(1);
            
            //Assert
            Assert.IsNull(result);
            _mockCardRepository.Verify(x => x.GetAllCards(), Times.Once);
            _mockGameRepository.Verify(x => x.CreateNewGame(It.IsAny<GameState>()), Times.Never);
        }

        [TestMethod]
        public async Task PurchaseCard_SuccesfullyPurchasesCard()
        {
            //Arrange
            var gameState = _gameStateObjects.testGameState;
            _mockGameRepository.Setup(x => x.GetGameState(1)).ReturnsAsync(gameState);
            //Act
            var result = await _gameService.PurchaseCard(1, 1);

            //Assert player
            Assert.AreEqual(1, result.Players[0].Cards.Count);
            Assert.AreEqual(30, result.Players[0].HP);
            Assert.AreEqual(0, result.Players[0].Coins);
            _mockGameRepository.Verify(x => x.GetGameState(1), Times.Once);
            _mockGameRepository.Verify(x => x.UpdateGame(It.IsAny<GameState>()), Times.Once);
            //Assert opponent
            Assert.AreEqual(1, result.Players[1].Cards.Count);
            Assert.AreEqual(0, result.Players[1].Coins);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task PurchaseCard_NotEnoughCoins()
        {
            //Arrange
            var gameState = _gameStateObjects.testGameState_ExpensiveCards;
            var cardId = gameState.Players[0].Shop.CardsForSale.FirstOrDefault().Card.Id;
            _mockGameRepository.Setup(x => x.GetGameState(1)).ReturnsAsync(gameState);

            //Act
            var result = await _gameService.PurchaseCard(cardId, 1);

            //Assert player
            Assert.AreEqual(0, result.Players[0].Cards.Count);
            Assert.AreEqual(1, result.Players[0].Coins);
            _mockGameRepository.Verify(x => x.GetGameState(1), Times.Once);
            _mockGameRepository.Verify(x => x.UpdateGame(It.IsAny<GameState>()), Times.Never);

            //Assert opponent
            Assert.AreEqual(0, result.Players[1].Cards.Count);
            Assert.AreEqual(1, result.Players[1].Coins);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task PurchaseCard_CardNotFound()
        {
            //Arrange
            var gameState = _gameStateObjects.testGameState;
            _mockGameRepository.Setup(x => x.GetGameState(1)).ReturnsAsync(gameState);
            //Act
            var result = await _gameService.PurchaseCard(100, 1);
            //Assert player
            Assert.AreEqual(0, result.Players[0].Cards.Count);
            Assert.AreEqual(1, result.Players[0].Coins);
            _mockGameRepository.Verify(x => x.GetGameState(1), Times.Once);
            _mockGameRepository.Verify(x => x.UpdateGame(It.IsAny<GameState>()), Times.Never);

            //Assert opponent
            Assert.AreEqual(0, result.Players[1].Cards.Count);
            Assert.AreEqual(1, result.Players[1].Coins);
        }

        [TestMethod]
        public async Task Surrender_SuccesfullySurrenders()
        {
            //Arrange
            var gameState = _gameStateObjects.testGameState;
            _mockGameRepository.Setup(x => x.GetGameState(1)).ReturnsAsync(gameState);

            //Act
            await _gameService.Surrender(1);

            //Assert
            Assert.IsTrue(gameState.Players[1].IsWin);
            _mockGameRepository.Verify(x => x.GetGameState(1), Times.Once);
            _mockGameRepository.Verify(x => x.EndGame(It.IsAny<GameState>(), It.IsAny<FinishedMatch>()), Times.Once);
        }

        [TestMethod]
        public async Task EndGame_SuccesfullyEndsGame()
        {
            //Arrange
            var gameState = _gameStateObjects.testGameState;
            _mockGameRepository.Setup(x => x.GetGameState(1)).ReturnsAsync(gameState);

            //Act
            await _gameService.EndGame(1);

            //Assert
            _mockGameRepository.Verify(x => x.GetGameState(1), Times.Once);
            _mockGameRepository.Verify(x => x.EndGame(It.IsAny<GameState>(), It.IsAny<FinishedMatch>()), Times.Once);
        }

        [TestMethod]
        public async Task PassTurn_SuccesfullyPassesTurn()
        {
            //Arrange
            var gameState = _gameStateObjects.testGameState;
            _mockGameRepository.Setup(x => x.GetGameState(1)).ReturnsAsync(gameState);

            //Act
            var result = await _gameService.PassTurn(1);

            //Assert
            Assert.AreEqual(2, result.TurnNumber);
            _mockGameRepository.Verify(x => x.GetGameState(1), Times.Once);
            _mockGameRepository.Verify(x => x.UpdateGame(It.IsAny<GameState>()), Times.Once);
        }

        [TestMethod]
        public async Task ResolveFight_SuccesfullyResolvesFight()
        {
            //Arrange
            var gameState = _gameStateObjects.testGameState;
            gameState.BuyCard(1);
            _mockGameRepository.Setup(x => x.GetGameState(1)).ReturnsAsync(gameState);

            //Act
            var result = await _gameService.ResolveFight(1);

            //Assert
            Assert.AreEqual(1, result.Fights.Count);
            _mockGameRepository.Verify(x => x.GetGameState(1), Times.Once);
            _mockGameRepository.Verify(x => x.UpdateGame(It.IsAny<GameState>()), Times.Once);
        }

        [TestMethod]
        public async Task ResolveFight_NoCardsToFight()
        {
            //Arrange
            var gameState = _gameStateObjects.testGameState;
            _mockGameRepository.Setup(x => x.GetGameState(1)).ReturnsAsync(gameState);

            //Act
            var result = await _gameService.ResolveFight(1);

            //Assert
            Assert.AreEqual(0, result.Fights.Count);
            _mockGameRepository.Verify(x => x.GetGameState(1), Times.Once);
            _mockGameRepository.Verify(x => x.UpdateGame(It.IsAny<GameState>()), Times.Once);
        }

        [TestMethod]
        public async Task ResolveFight_NoOpponentCardsToFight()
        {
            //Arrange
            var gameState = _gameStateObjects.testGameState;
            int HP = 30;
            gameState.BuyCard(1);
            gameState.Players[1].Cards.Clear();
            _mockGameRepository.Setup(x => x.GetGameState(1)).ReturnsAsync(gameState);

            //Act
            var result = await _gameService.ResolveFight(1);

            //Assert
            Assert.AreEqual(0, result.Fights.Count);
            Assert.AreEqual(gameState.Players[1].HP, HP - gameState.Players[0].Cards.First().DMG);
            _mockGameRepository.Verify(x => x.GetGameState(1), Times.Once);
            _mockGameRepository.Verify(x => x.UpdateGame(It.IsAny<GameState>()), Times.Once);
        }

        [TestMethod]
        public async Task ResolveFight_NoPlayerCardsToFight()
        {
            //Arrange
            var gameState = _gameStateObjects.testGameState;
            int HP = 30;
            gameState.BuyCard(1);
            gameState.Players[0].Cards.Clear();
            _mockGameRepository.Setup(x => x.GetGameState(1)).ReturnsAsync(gameState);

            //Act
            var result = await _gameService.ResolveFight(1);

            //Assert
            Assert.AreEqual(0, result.Fights.Count);
            Assert.AreEqual(gameState.Players[0].HP, HP - gameState.Players[1].Cards.First().DMG);
            _mockGameRepository.Verify(x => x.GetGameState(1), Times.Once);
            _mockGameRepository.Verify(x => x.UpdateGame(It.IsAny<GameState>()), Times.Once);
        }
    }
}
