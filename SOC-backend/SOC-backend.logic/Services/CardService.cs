using SOC_backend.logic.Interfaces;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Models.DomainModel;
using SOC_backend.logic.Models.Request;
using SOC_backend.logic.Models.Response;

namespace SOC_backend.logic.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task CreateCard(CreateCardRequest cardRequest)
        {
            CardModel card = cardRequest.ToCardModel();
            await _cardRepository.CreateCard(card);
        }

        public async Task<List<CardResponse>> GetAllCards()
        {
            List<CardModel> cardModelList = await _cardRepository.GetAllCards();
            List<CardResponse> cards = new List<CardResponse>();
            foreach (var cardModel in cardModelList)
            {
                var card = cardModel.ToCardResponse();
                cards.Add(card);
            }
            return cards;
        }

        public async Task<CardResponse> GetCard(int id)
        {
            CardModel cardModel = await _cardRepository.GetCard(id);
            CardResponse card = cardModel.ToCardResponse();
            return card;
        }

        public async Task EditCard(EditCardRequest cardRequest)
        {
            var card = cardRequest.ToCardModel();
            await _cardRepository.EditCard(card);
        }

        public async Task DeleteCard(int id)
        {
            await _cardRepository.DeleteCard(id);
        }
    }
}
