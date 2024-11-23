using SOC_backend.logic.Models.Cards;

namespace SOC_backend.logic.Interfaces
{
    public interface ICardRepository
    {
        Task CreateCard(Card card);
        Task<List<Card>> GetAllCards();
        Task<Card> GetCard(int id);
        Task EditCard(Card card);
        Task DeleteCard(int id);

	}
}
