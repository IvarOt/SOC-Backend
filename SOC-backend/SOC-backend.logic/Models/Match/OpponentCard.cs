using SOC_backend.logic.Models.Cards;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SOC_backend.logic.Models.Match
{
	public class OpponentCard
	{
		public int OpponentId { get; set; }
		[JsonIgnore]
		public Opponent Opponent { get; set; }
        public int CardId {  get; set; }
		public Card Card { get; set; }
		public bool IsOffence { get; set; } = false;
		public int PositionIndex { get; set; }
		public int HP { get; set; }
        public int DMG { get; set; }

        public OpponentCard() { }

        public void TakeDamage(int DMG)
        {
            HP = HP - DMG;
        }

    }
}
