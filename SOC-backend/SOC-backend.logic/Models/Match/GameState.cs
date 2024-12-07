
using SOC_backend.logic.Models.Cards;

namespace SOC_backend.logic.Models.Match
{
    public class GameState
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public List<Opponent> Players { get; set; }
        public bool PlayersTurn { get; set; }

        public GameState(List<Card> deck)
        {
            Players = new List<Opponent> 
            {
                new Opponent("Me", 30, 1, deck),
                new Opponent("Bob", 30, 1, deck),
            };
            PlayersTurn = true;
            PlayerId = 1;
        }

        public GameState() { }
        public void ResolveFightingStage()
        {
            var test = new List<Card>();
            var test1 = new List<Card>();

            ResolveFight(test, test1);   
            Players.ForEach(player => player.Shop.ClearPurchasedCards());
        }

        private void ResolveFight(List<Card> attackingPlayerCards, List<Card> attackingOpponentCards)
        {
            var maxCardAttacks = Math.Max(attackingPlayerCards.Count, attackingOpponentCards.Count);
            var minCardAttacks = Math.Min(attackingPlayerCards.Count, attackingOpponentCards.Count);
            var player = Players[0];
            var opponent = Players[1];

            foreach (var card in attackingOpponentCards)
            {
                player.AddCard(card);
            }
            foreach (var card in attackingPlayerCards)
            {
                opponent.AddCard(card);
            }

            for (int turnIndex = 0; turnIndex < minCardAttacks; turnIndex++)
            {
                var playerCard = attackingPlayerCards[turnIndex];
                var opponentCard = attackingOpponentCards[turnIndex];

                playerCard.TakeDamage(opponentCard.DMG);
                opponentCard.TakeDamage(playerCard.DMG);

                if (playerCard.HP <= 0)
                {
                    player.Cards.RemoveAt(turnIndex);
                }
                if (opponentCard.HP <= 0)
                {
                    opponent.Cards.RemoveAt(turnIndex);
                }
            }
            if (maxCardAttacks > minCardAttacks)
            {
                var differential = maxCardAttacks - minCardAttacks;

                if (attackingPlayerCards.Count > attackingOpponentCards.Count)
                {
                    for (int turnIndex = 0; turnIndex < differential; turnIndex++)
                    {
                        var playerCard = attackingPlayerCards[turnIndex];
                        opponent.TakeDamage(playerCard);
                    }
                }
                else
                {
                    for (int turnIndex = 0; turnIndex < differential; turnIndex++)
                    {
                        var opponentCard = attackingOpponentCards[turnIndex];
                        player.TakeDamage(opponentCard);
                    }
                }
            }
        }

        public void BuyCard(Card card)
        {
            Players[0].PurchaseCard(card);
        }

        public void StartNewRound()
        {
        }
    }
}
