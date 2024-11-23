using SOC_backend.logic.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Models.Match
{
	public class OpponentCard
	{
		public int OpponentId { get; set; }
		public Opponent Opponent { get; set; } 
		public int CardId {  get; set; }
		public Card Card { get; set; }
		public bool IsOffence { get; set; } = false;

		public OpponentCard() { }
	}
}
