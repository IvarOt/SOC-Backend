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
        public Opponent(string name, int hp, int coins)
        {
            Name = name;
            HP = hp;
            Coins = coins;
            Shop = new Shop();
        }

        public void TakeDamage(Card card)
        {
            HP = HP - card.DMG;
        }
    }
}
