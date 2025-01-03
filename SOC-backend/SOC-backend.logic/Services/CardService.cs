using SOC_backend.logic.Interfaces;
using SOC_backend.logic.Interfaces.Data;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Models.Cards;

namespace SOC_backend.logic.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IImageRepository _imageRepository;

        public CardService(ICardRepository cardRepository, IImageRepository imageRepository)
        {
            _cardRepository = cardRepository;
            _imageRepository = imageRepository;
        }

        public async Task CreateCard(CreateCardRequest cardRequest)
        {
            Card card = cardRequest.ToCard();
            var imageUrl = await _imageRepository.UploadImage(cardRequest.Image);
            card.ImageURL = imageUrl;
            await _cardRepository.CreateCard(card);
        }

        public async Task<List<CardResponse>> GetAllCards()
        {
            List<Card> cardModelList = await _cardRepository.GetAllCards();
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
            Card cardModel = await _cardRepository.GetCard(id);
            CardResponse card = cardModel.ToCardResponse();
            return card;
        }

        public async Task EditCard(EditCardRequest cardRequest)
        {
            var card = cardRequest.ToCardModel();
            var cardToEdit = await _cardRepository.GetCard(cardRequest.Id);
            if (cardRequest.Image != null)
            {
                card.ImageURL = await _imageRepository.UploadImage(cardRequest.Image);
                if (!string.IsNullOrEmpty(card.ImageURL))
                {
                    await _imageRepository.DeleteImage(cardToEdit.ImageURL);
                }
            }
            await _cardRepository.EditCard(card);
        }

        public async Task DeleteCard(int id)
        {
            var card = await _cardRepository.GetCard(id);
            await _imageRepository.DeleteImage(card.ImageURL);
            await _cardRepository.DeleteCard(id);
        }
    }
}
