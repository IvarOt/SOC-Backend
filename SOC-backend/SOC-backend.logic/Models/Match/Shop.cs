using SOC_backend.logic.Models.Cards;

namespace SOC_backend.logic.Models.Match
{
    public class Shop
    {
        public int Id { get; set; }
        public List<Card> AvailableCards { get; set; } = new List<Card>();

        public Shop() { }
        
        public void SellCard(Card card)
        {
            if (AvailableCards.Contains(card))
            {
                AvailableCards.Remove(card);
            }
        }
    }
}
