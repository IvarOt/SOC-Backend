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
		[NotMapped]
		public Opponent Opponent { get; set; }
        public int CardId {  get; set; }
		[NotMapped]
		public Card Card { get; set; }
		public bool IsOffence { get; set; } = false;

		public OpponentCard() { }
    }
}
