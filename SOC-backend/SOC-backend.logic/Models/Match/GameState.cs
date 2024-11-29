
using SOC_backend.logic.Models.Cards;

namespace SOC_backend.logic.Models.Match
{
    public class GameState
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public List<Opponent> Players { get; set; }
        public bool PlayersTurn { get; set; }

        public GameState()
        {
            Players = new List<Opponent> 
            {
                new Opponent("Me", 30, 1),
                new Opponent("Bob", 30, 1),
            };
            PlayersTurn = true;
            PlayerId = 1;
        }

        public void AssignCardToOpponent(Opponent opponent, Card card, bool isOffense = true)
        {
            var opponentCard = new OpponentCard
            {
                OpponentId = opponent.Id,
                CardId = card.Id,
                IsOffence = isOffense,
                Card = card,
            };
            opponent.Cards.Add(opponentCard);
        }

        public void ResolveFight(List<Card> attackingPlayerCards, List<Card> attackingOpponentCards)
        {
            var maxCardAttacks = Math.Max(attackingPlayerCards.Count, attackingOpponentCards.Count);
            var minCardAttacks = Math.Min(attackingPlayerCards.Count, attackingOpponentCards.Count);
            var player = Players[0];
            var opponent = Players[1];

            foreach (var item in attackingOpponentCards)
            {
                AssignCardToOpponent(Players[1], item);
            }
            foreach (var item in attackingPlayerCards)
            {
                AssignCardToOpponent(Players[0], item);
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
    }
}
