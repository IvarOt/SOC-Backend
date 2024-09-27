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
            await _context.Card.AddAsync(card);
            await _context.SaveChangesAsync();
        }

        public async Task EditCard(CardModel cardModel)
        {
            CardModel card = await _context.Card.FindAsync(cardModel.Id);
            if (card != null)
            {
                card.Update(cardModel.Id, cardModel.Name, cardModel.HP, cardModel.DMG);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCard(int id)
        {
            CardModel card = await _context.Card.FindAsync(id);
            if (card != null)
            {
				_context.Card.Remove(card);
				await _context.SaveChangesAsync();
			}
        }

        public async Task<CardModel> GetCard(int id)
        {
            var card = await _context.Card.FindAsync(id);
            return card;
        }

        public async Task<List<CardModel>> GetAllCards()
        {
            var cards = await _context.Card.ToListAsync();
            return cards;
        }
    }
}
