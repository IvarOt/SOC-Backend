using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using SOC_backend.logic.Models.Cards;

namespace SOC_backend.logic.Models.Match
{
    public class Shop
    {
        public int Id { get; set; }
        public List<ShopCard> CardsForSale { get; set; } = new List<ShopCard>();
        public List<Card> AvailableCards { get; set; } = new List<Card>();

        public Shop(List<Card> deck)
        {
            AvailableCards = deck;
            FillWithCards();
        }

        public Shop() { }

        public void AddCard(Card card)
        {
            CardsForSale.Add(new ShopCard
            {
                ShopId = Id,
                Card = card,
                CardId = card.Id,
                IsPurchased = false
            });
        }

        public void SetCardAsPurchased(Card card)
        {
            var cardToChange = CardsForSale.Where(c => c.Card == card).FirstOrDefault();
            if (cardToChange != null)
            {
                cardToChange.IsPurchased = true;
            }
        }

        public void ClearPurchasedCards()
        {
            CardsForSale.RemoveAll(c => c.IsPurchased);
        }

        public void FillWithCards()
        {
            Random random = new Random();
            var amountOfCards = AvailableCards.Count > 4 ? 4 : AvailableCards.Count;
            for (int i = CardsForSale.Count; i < amountOfCards; i++)
            {
                var randomCardIndex = random.Next(0, AvailableCards.Count);
                AddCard(AvailableCards[randomCardIndex]);
                AvailableCards.RemoveAt(randomCardIndex);
            }
        }

        public void Update(List<ShopCard> availableCards)
        {
            CardsForSale = availableCards;
        }
    }
}
