using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC_backend.logic.Models.Cards;

namespace SOC_backend.logic.Models.Match
{
    public class CardFight
    {
        public int Id { get; set; }
        private readonly List<FightCard> _Cards = new List<FightCard>();
        public List<FightCard> Cards { get { return _Cards; } }


        public CardFight() { }

        public void AddCards(List<FightCard> cards)
        {
            foreach (var card in cards)
            {
                if (_Cards.Count < 2)
                {
                    _Cards.Add(new FightCard
                    {
                        HP = card.HP,
                        DMG = card.DMG,
                    });
                }
                else
                {
                    throw new InvalidOperationException("Only two cards can fight at the same time.");
                }
            }
        }
    }
}
