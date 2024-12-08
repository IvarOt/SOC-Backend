using Microsoft.EntityFrameworkCore;
using SOC_backend.data;
using SOC_backend.data.Repositories;
using SOC_backend.logic.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.test.Repositories
{
	[TestClass]
    [TestCategory("Integration")]
    public class PlayerRepositoryTests
	{
		private ApplicationDbContext _context;
		private DbContextOptions<ApplicationDbContext> _options;
		private PlayerRepository _playerRepository;


		[TestInitialize]
		public void Setup()
		{
			_options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase("TestDatabase")
			.Options;

			_context = new ApplicationDbContext(_options);
			_context.Database.EnsureCreated();

			_playerRepository = new PlayerRepository(_context);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
		}

		[TestMethod]
		public async Task GetPlayer_ReturnsPlayer()
		{
			//Arrange
			var player = new Player("Test", "Test@gmail.com", "Test123!");
			_context.Player.Add(player);
			_context.SaveChanges();


			//Act
			var result = await _playerRepository.GetPlayer(1);

			//Assert
			Assert.AreSame(player, result);
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public async Task GetPlayer_ThrowsException()
		{
			//Act
			var result = await _playerRepository.GetPlayer(1);
		}

		[TestMethod]
		public async Task StoreRefreshToken_StoresRefreshTokenCorrectly()
		{
			//Arrange
			var player = new Player("Test", "Test@gmail.com", "Test123!");
			_context.Player.Add(player);
			_context.SaveChanges();
			string refreshToken = "refresh_token";
			int playerId = 1;

			//Act
			await _playerRepository.StoreRefreshToken(playerId, refreshToken);

			//Assert
			var result = _context.Player.FirstOrDefault();
			Assert.AreSame(refreshToken, result.RefreshToken);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public async Task StoreRefreshToken_ThrowsException()
		{
			//Arrange
			string refreshToken = "refresh_token";
			int playerId = 1;

			//Act
			await _playerRepository.StoreRefreshToken(playerId, refreshToken);
		}

		[TestMethod]
		public async Task GetMatchingPlayer_ReturnsMatchingPlayer()
		{
			//Arrange
			var player = new Player("Test", "Test@gmail.com", "Test123!");
			string refreshToken = "refresh_token";
			player.RefreshToken = refreshToken;
			_context.Player.Add(player);
			_context.SaveChanges();

			//Act
			var result = await _playerRepository.GetMatchingPlayer(refreshToken);

			//Assert
			Assert.AreSame(player, result);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public async Task GetMatchingPlayer_ThrowsException()
		{
			//Arrange
			string refreshToken = "refresh_token";

			//Act
			var result = await _playerRepository.GetMatchingPlayer(refreshToken);
		}

		[TestMethod]
		public async Task Register_AddsPlayer()
		{
			//Arrange
			var player = new Player("Test", "Test@gmail.com", "Test123!");

			//Act
			await _playerRepository.Register(player);

			//Assert
			var result = _context.Player.FirstOrDefault();
			Assert.AreSame(player, result);
		}

		[TestMethod]
		public async Task Login_GetsExistingPlayer()
		{
			//Arrange
			var player = new Player("Test", "Test@gmail.com", "Test123!");
			_context.Player.Add(player);
			_context.SaveChanges();

			//Act
			var result = await _playerRepository.Login(player);

			//Assert
			Assert.AreSame(player, result);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public async Task Login_ThrowsException()
		{
			//Arrange
			var player = new Player("Test", "Test@gmail.com", "Test123!");

			//Act
			var result = await _playerRepository.Login(player);
		}
	}
}
