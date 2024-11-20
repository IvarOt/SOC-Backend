using Microsoft.EntityFrameworkCore;
using SOC_backend.data;
using SOC_backend.data.Repositories;
using SOC_backend.logic.Exceptions;
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
            Assert.AreEqual("Test", result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task GetCard_ShouldThrowException()
        {
            //Act
            var result = await _cardRepository.GetCard(10);
        }
    }
}
