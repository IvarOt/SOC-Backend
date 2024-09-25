using Microsoft.EntityFrameworkCore;
using SOC_backend.logic.Interfaces;
using SOC_backend.logic.Models.DomainModel;

namespace SOC_backend.data.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;

        public CardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateCard(CardModel card)
        {
            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CardModel>> GetAllCards()
        {
            var cards = await _context.Cards.ToListAsync();
            return cards;
        }
    }
}
