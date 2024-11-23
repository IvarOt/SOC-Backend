using SOC_backend.logic.Models.Cards;

namespace SOC_backend.logic.Interfaces.Logic
{
    public interface ICardService
    {
        Task CreateCard(CreateCardRequest cardRequest);
        Task<List<CardResponse>> GetAllCards();
        Task<CardResponse> GetCard(int id);
        Task EditCard(EditCardRequest cardRequest);
        Task DeleteCard(int id);
	}
}
