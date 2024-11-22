using Microsoft.EntityFrameworkCore;
using SOC_backend.logic.Interfaces;
using SOC_backend.logic.Models.Cards;

namespace SOC_backend.data.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;

        public CardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateCard(Card card)
        {
            await _context.Card.AddAsync(card);
            await _context.SaveChangesAsync();
        }

        public async Task EditCard(Card cardModel)
        {
            Card? card = await _context.Card.FindAsync(cardModel.Id);
            if (card == null)
            {
                throw new InvalidOperationException("Card not found");
            }
            card.Update(cardModel.Name, cardModel.HP, cardModel.DMG, cardModel.Color, string.IsNullOrEmpty(cardModel.ImageURL) ? card.ImageURL : cardModel.ImageURL);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCard(int id)
        {
            Card? card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                throw new InvalidOperationException("Card not found");
			}
            _context.Card.Remove(card);
            await _context.SaveChangesAsync();
        }

        public async Task<Card> GetCard(int id)
        {
            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                throw new KeyNotFoundException("Card not found");
            }
            return card;
        }

        public async Task<List<Card>> GetAllCards()
        {
            var cards = await _context.Card.ToListAsync();
            if (cards.Count == 0)
            {
				throw new KeyNotFoundException("Cards not found");
			}
			return cards;
        }
    }
}
