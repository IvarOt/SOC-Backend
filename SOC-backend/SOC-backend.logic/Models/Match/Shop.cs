using SOC_backend.logic.Models.Cards;

namespace SOC_backend.logic.Models.Match
{
    public class Shop
    {
        public int Id { get; set; }
        public List<ShopCard> AvailableCards { get; set; } = new List<ShopCard>();

        public Shop() { }
    }
}
