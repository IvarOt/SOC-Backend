using SOC_backend.logic.Models.Cards;

namespace SOC_backend.logic.Models.Match
{
    public class Opponent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int Coins { get; set; }
        public List<Card> Defence { get; set; } = new List<Card>();
        public List<Card> Offence { get; set; } = new List<Card>();
        public List<Card> Cards { get; set; } = new List<Card>();
        public Shop Shop { get; set; } = new Shop();

        public Opponent() { }
        public Opponent(string name, int hp, int coins)
        {
            Name = name;
            HP = hp;
            Coins = coins;
        }

        public void BuyCard(Card card)
        {
            if (Shop.AvailableCards.Contains(card))
            {
                Shop.SellCard(card);
                Cards.Add(card);
                Coins = Coins - card.Cost;
            }
        }

        public void PlayCard(Card card, bool isOffence)
        {
            if (Cards.Contains(card))
            {
                if (isOffence)
                {
                    Offence.Add(card);
                    Cards.Remove(card);
                }
                else
                {
                    Defence.Add(card);
                    Cards.Remove(card);
                }
            }
        }
    }
}
