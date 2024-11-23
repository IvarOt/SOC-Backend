
using Microsoft.AspNetCore.Http;
using Moq;
using SOC_backend.logic.Interfaces;
using SOC_backend.logic.Interfaces.Data;
using SOC_backend.logic.Models.Cards;
using SOC_backend.logic.Services;
using System.Text;

namespace SOC_backend.test.Services
{
    [TestClass]
    public class CardServiceTests
    {
        private Mock<ICardRepository> _mockCardRepository;
        private Mock<IImageRepository> _mockImageRepository;
        private CardService _cardService;

        [TestInitialize]
        public void Setup()
        {
            _mockCardRepository = new Mock<ICardRepository>();
            _mockImageRepository = new Mock<IImageRepository>();
            _cardService = new CardService(_mockCardRepository.Object, _mockImageRepository.Object);
        }

        [TestMethod]
        public async Task CreateCard_CallsRepositoriesCorrectly()
        {
            //Arrange
            var cardRequest = new CreateCardRequest { Name = "Test", Image =  CreateMockFormFile()};
            _mockImageRepository.Setup(repo => repo.UploadImage(cardRequest.Image)).ReturnsAsync("http://test.com/image.png");

            //Act
            await _cardService.CreateCard(cardRequest);

            //Assert
            _mockImageRepository.Verify(repo => repo.UploadImage(cardRequest.Image), Times.Once);
            _mockCardRepository.Verify(repo => repo.CreateCard(It.Is<Card>(card => card.ImageURL == "http://test.com/image.png")), Times.Once);
        }

        [TestMethod]
        public async Task GetAllCards_ReturnsCardResponses()
        {
            //Arrange
            var cards = new List<Card>
            {
                new Card(),
                new Card()
            };
            _mockCardRepository.Setup(repo => repo.GetAllCards()).ReturnsAsync(cards);

            //Act
            var result = await _cardService.GetAllCards();

            //Assert
            Assert.AreEqual(cards.Count, result.Count);
        }

        [TestMethod]
        public async Task GetCard_ReturnsMappedCard()
        {
            //Arrange
            var card = new Card();
            _mockCardRepository.Setup(repo => repo.GetCard(1)).ReturnsAsync(card); 

            //Act
            var result = await _cardService.GetCard(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CardResponse));
        }

        [TestMethod]
        public async Task EditCard_CallsRepositories()
        {
            //Arrange
            var card = new EditCardRequest { Name = "Test", Image = CreateMockFormFile() };
            _mockImageRepository.Setup(repo => repo.UploadImage(card.Image)).ReturnsAsync("http://test.com/image.png");

            //Act
            await _cardService.EditCard(card);

            //Assert
            _mockImageRepository.Verify(repo => repo.UploadImage(card.Image), Times.Once);
            _mockCardRepository.Verify(repo => repo.EditCard(It.Is<Card>(card => card.ImageURL == "http://test.com/image.png")), Times.Once);
        }

        [TestMethod]
        public async Task DeleteCard_CallsRepositories()
        {
            //Arrange
            int cardId = 1;

            //Act
            await _cardService.DeleteCard(cardId);

            //Assert
            _mockCardRepository.Verify(repo => repo.DeleteCard(cardId), Times.Once);
        }


        private IFormFile CreateMockFormFile(string content = "Mock image", string fileName = "test.jpg", string contentType = "image/jpeg")
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var formFile = new Mock<IFormFile>();

            formFile.Setup(f => f.OpenReadStream()).Returns(stream);
            formFile.Setup(f => f.FileName).Returns(fileName);
            formFile.Setup(f => f.Length).Returns(stream.Length);
            formFile.Setup(f => f.ContentType).Returns(contentType);

            return formFile.Object;
        }

    }
}
