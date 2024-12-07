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
        public int HP { get; set; } = 30;
        public int Coins { get; set; } = 1;
        public List<OpponentCard> Cards { get; set; } = new List<OpponentCard>();
        public Shop Shop { get; set; }

        public Opponent() { }
        public Opponent(string name, List<Card> deck)
        {
            Name = name;
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

        public void AutoPurchaseCard()
        {
            var purchaseableCards = Shop.AvailableCards.Where(c => c.Card.Cost <= Coins).ToList();
            if (purchaseableCards.Count == 0)
            {
                return;
            }
            var cardToPurchase = purchaseableCards.OrderBy(c => c.Card.DMG).ThenBy(c => c.Card.HP).Last();
            if (cardToPurchase != null)
            {
                Coins -= cardToPurchase.Card.Cost;
                AddCard(cardToPurchase.Card);
                Shop.SetCardAsPurchased(cardToPurchase.Card);
            }
        }

        public void GiveCoins(int amountOfCoins)
        {
            if (amountOfCoins > 0)
            {
                Coins += amountOfCoins;
            }
        }

        public void Update(Opponent updatedOpponent)
        {
            Name = updatedOpponent.Name;
            HP = updatedOpponent.HP;
            Coins = updatedOpponent.Coins;
            Cards = updatedOpponent.Cards;
            Shop.Update(updatedOpponent.Shop.AvailableCards);
        }
    }
}
