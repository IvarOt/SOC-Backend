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
	public class ShopCard
	{
		public int ShopId { get; set; }
		[JsonIgnore]
        public Shop Shop { get; set; }
		public int CardId { get; set; }
		public Card Card { get; set; }
		public bool IsPurchased { get; set; } = false;

		public ShopCard() { }
	}
}
