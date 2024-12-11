using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json.Linq;
using SOC_backend.logic.Models.Player;
using SOC_backend.logic.Services;
using SOC_backend.test.TestObjects;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.test.Services
{
	[TestClass]
    [TestCategory("Unit")]
    public class TokenServiceTests
	{
		private Mock<IConfiguration> _mockConfiguration;
		private TokenService _tokenService;
		private PlayerObjects _playerObjects;

		[TestInitialize]
		public void Setup()
		{
			_mockConfiguration = new Mock<IConfiguration>();
			_mockConfiguration.Setup(c => c["JwtSettings:Key"]).Returns("H9v6Kk3uT3/z1gV3hwvOl1T+aM1Z2A5V2B7QxM+Bz7B"); 
			_mockConfiguration.Setup(c => c["JwtSettings:Issuer"]).Returns("TestIssuer");
			_mockConfiguration.Setup(c => c["JwtSettings:Audience"]).Returns("TestAudience");

			_tokenService = new TokenService(_mockConfiguration.Object);
			_playerObjects = new PlayerObjects();
		}

		[TestMethod]
		public async Task CreateAccesToken_ReturnsAccessToken()
		{
            //Arrange
            var player = _playerObjects.testPlayer;

            //Act
            var result = _tokenService.CreateAccesToken(player);

			//Assert
			Assert.IsNotNull(result);
			var tokenHandler = new JwtSecurityTokenHandler();
			Assert.IsTrue(tokenHandler.CanReadToken(result));
		}

		[TestMethod]
		public async Task CreateRefreshToken_ReturnsRefreshToken()
		{
			//Act
			var result = _tokenService.CreateRefreshToken();

			//Assert
			Assert.IsNotNull(result);
		}
	}
}
