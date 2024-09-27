using SOC_backend.logic.Models.DomainModel;

namespace SOC_backend.logic.Interfaces
{
    public interface ICardRepository
    {
        Task CreateCard(CardModel card);
        Task<List<CardModel>> GetAllCards();
        Task<CardModel> GetCard(int id);
        Task EditCard(CardModel card);
        Task DeleteCard(int id);

	}
}
