namespace SOC_backend.logic.Models.Cards
{
    public class Card
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int HP { get; private set; }
        public int DMG { get; private set; }
        public string Color { get; private set; }
        public string? ImageURL { get; set; }
        public int Cost { get; private set; }

        //For entity framework
        public Card() { }
        public Card(int id, string name, int hp, int dmg, string color, string? imageUrl, int cost)
        {
            Id = id;
            Name = name;
            HP = hp;
            DMG = dmg;
            Color = color;
            ImageURL = imageUrl;
            Cost = cost;
        }

        public Card(string name, int hp, int dmg, string color, string? imageUrl, int cost)
        {
            Name = name;
            HP = hp;
            DMG = dmg;
            Color = color;
            ImageURL = imageUrl;
            Cost = cost;
        }

        public void Update(string name, int hp, int dmg, string color, int cost, string? imageUrl)
        {
            Name = name;
            HP = hp;
            DMG = dmg;
            Color = color;
            Cost = cost;
            if (ImageURL != null)
            {
                ImageURL = imageUrl;
            }
        }
        

        public CardResponse ToCardResponse()
        {
            return new CardResponse(Id, Name, HP, DMG, Color, ImageURL, Cost);
        }
    }
}
