﻿using Moq;
using SOC_backend.logic.Interfaces.Data;
using SOC_backend.logic.Interfaces;
using SOC_backend.logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Models.Player;

namespace SOC_backend.test.Services
{
    [TestClass]
    public class PlayerServiceTests
    {
        private Mock<IPlayerRepository> _mockPlayerRepository;
        private Mock<ITokenService> _mockTokenService;
        private PlayerService _playerService;

        [TestInitialize]
        public void Setup()
        {
            _mockPlayerRepository = new Mock<IPlayerRepository>();
            _mockTokenService = new Mock<ITokenService>();
            _playerService = new PlayerService(_mockPlayerRepository.Object, _mockTokenService.Object);
        }

        [TestMethod]
        public async Task GetProfileInfo_MapsToResponseCorrectly()
        {
            //Arrange
            var player = new Player();
            _mockPlayerRepository.Setup(repo => repo.GetPlayer(1)).ReturnsAsync(player);

            //Act
            var result = await _playerService.GetProfileInfo(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(PlayerProfileResponse));
        }

        [TestMethod]
        public async Task Register_MapsDataCorrectly()
        {
            //Arrange
            var player = new RegisterPlayerRequest { Username = "Test", Password = "Test123!", ConfirmPassword = "Test123!", Email = "test@gmail.com", Role = "player" };

            //Act
            await _playerService.Register(player);

            //Assert
            _mockPlayerRepository.Verify(repo => repo.Register(It.Is<Player>(player => player.Username == "Test")), Times.Once);
        }

        [TestMethod]
        public async Task Login_ReturnsTokens()
        {
            //Arrange
            var loginRequest = new PlayerLoginRequest { Username = "Test", Password = "Test123!"};
            var retreivedPlayer = new Player(1, "Test", BCrypt.Net.BCrypt.EnhancedHashPassword("Test123!"));

            _mockPlayerRepository.Setup(repo => repo.Login(It.IsAny<Player>())).ReturnsAsync(retreivedPlayer);
            _mockTokenService.Setup(service => service.CreateAccesToken(It.IsAny<Player>())).Returns("acces_token");
            _mockTokenService.Setup(service => service.CreateRefreshToken()).Returns("refresh_token");
            _mockPlayerRepository.Setup(repo => repo.StoreRefreshToken(It.IsAny<int>(), It.IsAny<string>()));

            //Act
            var result = await _playerService.Login(loginRequest);

            //Assert
            Assert.AreEqual("acces_token", result.AccesToken);
            Assert.AreEqual("refresh_token", result.RefreshToken);
            _mockPlayerRepository.Verify(repo => repo.StoreRefreshToken(retreivedPlayer.Id, "refresh_token"), Times.Once);
        }

        [TestMethod]
        public async Task RefreshAccesToken_ReturnsAccesToken()
        {
            //Arrange
            var player = new Player(1, "Test", DateTime.Now.AddMinutes(5));
            string refreshToken = "refresh_token";
            string accesToken = "acces_token";
            _mockPlayerRepository.Setup(repo => repo.GetMatchingPlayer(refreshToken)).ReturnsAsync(player);
            _mockTokenService.Setup(service => service.CreateAccesToken(player)).Returns(accesToken);

            //Act
            var result = await _playerService.RefreshAccesToken(refreshToken);

            //Assert
            Assert.AreEqual(accesToken, result);
        }
    }
}
