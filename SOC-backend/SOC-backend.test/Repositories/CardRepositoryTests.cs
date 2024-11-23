using Microsoft.EntityFrameworkCore;
using SOC_backend.data;
using SOC_backend.data.Repositories;
using SOC_backend.logic.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.test.Repositories
{
    [TestClass]
    public class CardRepositoryTests
    {
        private ApplicationDbContext _context;
        private DbContextOptions<ApplicationDbContext> _options;
        private CardRepository _cardRepository;

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new ApplicationDbContext(_options);
            _context.Database.EnsureCreated();
            _cardRepository = new CardRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetCard_ShouldReturnCard()
        {
            // Arrange
            var card = new Card(1, "Test", 5, 5, "#4A90E2", "http://test.com/image.png");
            _context.Card.Add(card);
            _context.SaveChanges();

            // Act
            var result = await _cardRepository.GetCard(1);

            // Assert
            Assert.AreSame(result, card);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task GetCard_ShouldThrowException()
        {
            //Act
            var result = await _cardRepository.GetCard(10);
        }

        [TestMethod]
        public async Task GetAllCards_ReturnsAllCards()
        {
            //Arrange
            var cards = new List<Card>
            {
                new Card(1, "Test1", 5, 5, "#4A90E2", "http://test.com/image.png"),
                new Card(2, "Test2", 5, 5, "#4A90E2", "http://test.com/image.png"),
                new Card(3, "Test3", 5, 5, "#4A90E2", "http://test.com/image.png")
            };
            cards.ForEach(card => _context.Card.Add(card));
            _context.SaveChanges();

            //Act
            var result = await _cardRepository.GetAllCards();

            //Assert
            Assert.AreEqual(cards.Count, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task GetAllCards_ThrowsException()
        {
            //Act
            var result = await _cardRepository.GetAllCards();
        }

        [TestMethod]
        public async Task DeleteCard_DeletesCard()
        {
            //Arrange
            var card = new Card(1, "Test1", 5, 5, "#4A90E2", "http://test.com/image.png");
            _context.Card.Add(card);
            _context.SaveChanges();

            //Act
            await _cardRepository.DeleteCard(1);

            //Assert
            var result = _context.Card.FirstOrDefault();
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task DeleteCard_ThrowsException()
        {
            //Act
            await _cardRepository.DeleteCard(1);
        }

        [TestMethod]
        public async Task EditCard_UpdatesCardCorrectly()
        {
            //Arrange
            var card = new Card(1, "Test", 5, 5, "#4A90E2", "http://test.com/image.png");
            _context.Card.Add(card);
            _context.SaveChanges();

            //Act
            card.Update("Updated", 5, 5, "#4A90E2", "http://test.com/image.png");
            await _cardRepository.EditCard(card);

            //Assert
            var result = _context.Card.Where(card => card.Id == 1).FirstOrDefault();
            Assert.AreEqual(card.Name, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task EditCard_ThrowsException()
        {
            //Arrange
            var card = new Card(1, "Test", 5, 5, "#4A90E2", "http://test.com/image.png");

            //Act
            await _cardRepository.EditCard(card);
        }

        [TestMethod]
        public async Task CreateCard_CreatesCardCorrectly()
        {
            //Arrange
            var card = new Card(1, "Test", 5, 5, "#4A90E2", "http://test.com/image.png");

            //Act
            await _cardRepository.CreateCard(card);

            //Assert
            var result = _context.Card.FirstOrDefault();
            Assert.AreSame(card, result);
        }
	}
}
