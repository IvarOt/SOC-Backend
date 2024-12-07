
using SOC_backend.logic.Models.Cards;

namespace SOC_backend.logic.Models.Match
{
    public class GameState
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public List<Opponent> Players { get; set; }
        public int TurnNumber { get; set; } = 1;
        public List<CardFight> Fights { get; set; } = new List<CardFight>();

        public GameState(List<Card> deck)
        {
            Players = new List<Opponent>
            {
                new Opponent("Me", deck),
                new Opponent("Bob", deck),
            };
            PlayerId = 1;
        }

        public GameState() { }

        public void ResolveTurn()
        {
            ResolveFight();
            StartNewRound();
        }

        public void StartNewRound()
        {
            TurnNumber++;
            foreach (var player in Players)
            {
                player.GiveCoins(TurnNumber);
                player.Shop.ClearPurchasedCards();
            }
        }

        private void ResolveFight()
        {
            var attackingPlayerCards = Players[0].Cards;
            var attackingOpponentCards = Players[1].Cards;

            var maxCardAttacks = Math.Max(attackingPlayerCards.Count, attackingOpponentCards.Count);
            var minCardAttacks = Math.Min(attackingPlayerCards.Count, attackingOpponentCards.Count);
            var player = Players[0];
            var opponent = Players[1];

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
                List<FightCard> cards = new List<FightCard>
                {
                    new FightCard
                {
                    HP = playerCard.HP,
                    DMG = playerCard.DMG,
                },
                new FightCard
                {
                    HP = opponentCard.HP,
                    DMG = opponentCard.DMG,
                },
                };
                CardFight fight = new CardFight();
                fight.AddCards(cards);
                Fights.Add(fight);
            }
            if (maxCardAttacks > minCardAttacks)
            {
                var differential = maxCardAttacks - minCardAttacks;

                if (attackingPlayerCards.Count > attackingOpponentCards.Count)
                {
                    for (int turnIndex = 0; turnIndex < differential; turnIndex++)
                    {
                        var playerCard = attackingPlayerCards[turnIndex].Card;
                        opponent.TakeDamage(playerCard);
                    }
                }
                else
                {
                    for (int turnIndex = 0; turnIndex < differential; turnIndex++)
                    {
                        var opponentCard = attackingOpponentCards[turnIndex].Card;
                        player.TakeDamage(opponentCard);
                    }
                }
            }
        }

        public void BuyCard(Card card)
        {
            Players[0].PurchaseCard(card);
            Players[1].AutoPurchaseCard();
        }

        public void GiveCardsTheirPositions()
        {
            foreach (var player in Players)
            {
                player.Cards = player.Cards.OrderBy(x => x.IsOffence).ThenBy(x => x.PositionIndex).ToList();
            }
        }

        public void Update(GameState newState)
        {
            PlayerId = newState.PlayerId;
            for (int i = 0; i < Players.Count - 1; i++)
            {
                Players[i].Update(newState.Players[i]);
            }
            TurnNumber = newState.TurnNumber;
            Fights = newState.Fights;
        }
    }
}
