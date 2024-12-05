using SOC_backend.logic.Models.Cards;

namespace SOC_backend.logic.Models.Match
{
    public class Shop
    {
        public int Id { get; set; }
        public List<ShopCard> AvailableCards { get; set; } = new List<ShopCard>();

        public Shop(List<Card> deck)
        {
            FillWithCards(deck);
        }

        public Shop() { }

        public void AddCard(Card card)
        {
            AvailableCards.Add(new ShopCard
            {
                ShopId = Id,
                Card = card,
                CardId = card.Id,
                IsPurchased = false
            });
        }

        public void SetCardAsPurchased(Card card)
        {
            var cardToChange = AvailableCards.Where(c => c.Card == card).FirstOrDefault();
            if (cardToChange != null)
            {
                cardToChange.IsPurchased = true;
            }
        }

        public void ClearShop()
        {
            AvailableCards.Clear();
        }

        public void FillWithCards(List<Card> deck)
        {
            List<Card> cardsForShop = new List<Card>();
            Random random = new Random();
            for (int i = 0; i < deck.Count; i++)
            {
                var randomCardIndex = random.Next(0, deck.Count);
                cardsForShop.Add(deck[randomCardIndex]);
            }
            cardsForShop.ForEach(card => AddCard(card));
        }
    }
}
