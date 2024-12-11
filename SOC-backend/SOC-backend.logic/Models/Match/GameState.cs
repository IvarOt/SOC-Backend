
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
        public bool GameEnded { get; set; } = false;

        public GameState(List<Card> deck, int playerId)
        {
            if (deck.Count == 0)
            {
                throw new InvalidOperationException("Deck cannot be empty.");
            }
            else
            {
                var deck1 = new List<Card>(deck);
                Players = new List<Opponent>
            {
                new Opponent("Me", deck),
                new Opponent("Bob", deck1),
            };
                PlayerId = playerId;
            }
        }

        public GameState() { }

        public void ResolveTurn()
        {
            ResolveFight();
            StartNewRound();
        }

        public void StartNewRound()
        {
            if (Players[0].HP <= 0 || Players[1].HP <= 0)
            {
                DeclareWinner();
            }
            TurnNumber++;
            foreach (var player in Players)
            {
                player.GiveCoins(TurnNumber);
                player.Shop.ClearPurchasedCards();
                List<Card> cards = new List<Card>();
                player.Shop.CardsForSale.ForEach(card => cards.Add(card.Card));
                player.Shop.FillWithCards();
            }
        }

        public void PassTurn()
        {
            Players[1].AutoPurchaseCard();
            ResolveFight();
            StartNewRound();
        }

        private void ResolveFight()
        {
            var attackingPlayerCards = Players[0].Cards;
            var attackingOpponentCards = Players[1].Cards;

            var maxCardAttacks = Math.Max(attackingPlayerCards.Count, attackingOpponentCards.Count);
            var minCardAttacks = Math.Min(attackingPlayerCards.Count, attackingOpponentCards.Count);
            var player = Players[0];
            var opponent = Players[1];
            int opponenCardsIndex = 0;
            int playerCardsIndex = 0;

            OpponentCard opponentCard = new OpponentCard();
            OpponentCard playerCard = new OpponentCard();

            for (int turnIndex = 0; turnIndex < minCardAttacks; turnIndex++)
            {
                if (attackingPlayerCards.Count == 0 && attackingOpponentCards.Count == 0)
                {
                    return;
                }
                if (attackingPlayerCards.Count > 0 && attackingOpponentCards.Count > 0)
                {
                    if (attackingPlayerCards.Count >= playerCardsIndex)
                    {
                        playerCard = attackingPlayerCards[playerCardsIndex];
                    }
                    if (attackingOpponentCards.Count >= opponenCardsIndex)
                    {
                        opponentCard = attackingOpponentCards[opponenCardsIndex];
                    }
                    playerCard.TakeDamage(opponentCard.DMG);
                    opponentCard.TakeDamage(playerCard.DMG);
                    if (playerCard.HP <= 0)
                    {
                        player.Cards.Remove(playerCard);
                        playerCardsIndex--;
                    }
                    if (opponentCard.HP <= 0)
                    {
                        opponent.Cards.Remove(opponentCard);
                        opponenCardsIndex--;
                    }
                    opponenCardsIndex++;
                    playerCardsIndex++;
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
                        if (attackingPlayerCards[turnIndex] != null)
                        {
                            playerCard = attackingPlayerCards[turnIndex];
                            opponent.TakeDamage(playerCard);
                        }
                    }
                }
                else
                {
                    for (int turnIndex = 0; turnIndex < differential; turnIndex++)
                    {
                        if (attackingOpponentCards[turnIndex] != null)
                        {
                            opponentCard = attackingOpponentCards[turnIndex];
                            player.TakeDamage(opponentCard);
                        }
                    }
                }
            }
        }

        public void BuyCard(int cardId)
        {
            Players[0].PurchaseCard(cardId);
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

        public void DeclareWinner()
        {
            if (Players[0].HP <= 0)
            {
                Players[1].IsWin = true;
                GameEnded = true;
            }
            else if (Players[1].HP <= 0)
            {
                Players[0].IsWin = true;
                GameEnded = true;
            }
        }

        public void Surrender()
        {
            Players[1].IsWin = true;
            GameEnded = true;
        }
    }
}
