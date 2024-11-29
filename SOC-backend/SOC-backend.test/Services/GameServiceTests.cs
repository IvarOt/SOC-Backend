using Moq;
using SOC_backend.logic.Interfaces.Data;
using SOC_backend.logic.Models.Match;
using SOC_backend.logic.Services;

namespace SOC_backend.test.Services
{
    [TestClass]
    public class GameServiceTests
    {
        private Mock<IGameRepository> _mockGameRepository;
        private GameService _gameService;

        [TestInitialize]
        public void Setup()
        {
            _mockGameRepository = new Mock<IGameRepository>();
            _gameService = new GameService(_mockGameRepository.Object);
        }

        [TestMethod]
        public async Task StartNewGame_SuccesfullyCreatesGame()
        {
            //Arrange

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
        }
    }
}
