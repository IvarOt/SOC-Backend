using SOC_backend.logic.Models.DomainModel;
using SOC_backend.logic.Models.Request;
using SOC_backend.logic.Models.Response;

namespace SOC_backend.logic.Interfaces.Logic
{
    public interface ICardService
    {
        Task CreateCard(CardRequest cardRequest);
        Task<List<CardResponse>> GetAllCards();
        Task<CardResponse> GetCard(int id);
        Task EditCard(EditCardRequest cardRequest);
        Task DeleteCard(int id);
	}
}
