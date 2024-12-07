using SOC_backend.logic.Models.Cards;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOC_backend.logic.Models.Match
{
    public class Opponent
    {
        public int Id { get; set; }

        [ForeignKey("GameStateId")]
        public int GameStateId { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int Coins { get; set; }
        public List<OpponentCard> Cards { get; set; } = new List<OpponentCard>();
        public Shop Shop { get; set; }

        public Opponent() { }
        public Opponent(string name, int hp, int coins, List<Card> deck)
        {
            Name = name;
            HP = hp;
            Coins = coins;
            Shop = new Shop(deck);
        }

        public void TakeDamage(Card card)
        {
            HP = HP - card.DMG;
        }

        public void AddCard(Card card, bool isOffence = false)
        {
            Cards.Add(new OpponentCard
            {
                OpponentId = Id,
                CardId = card.Id,
                Card = card,
                IsOffence = isOffence,
            });
        }

        public void RemoveCard(Card card)
        {
            var cardToDelete = Cards.Where(x => x.Card == card).FirstOrDefault();
            if (cardToDelete != null)
            {
                Cards.Remove(cardToDelete);
            }
        }

        public void PurchaseCard(Card card)
        {
            if (Coins >= card.Cost)
            {
                Coins -= card.Cost;
                AddCard(card);
                Shop.SetCardAsPurchased(card);
            }
        }
    }
}
