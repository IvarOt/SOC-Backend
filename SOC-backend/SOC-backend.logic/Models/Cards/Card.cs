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
        public Card(int id, string name, int hp, int dmg, string color, string? imageUrl)
        {
            Id = id;
            Name = name;
            HP = hp;
            DMG = dmg;
            Color = color;
            ImageURL = imageUrl;
        }

        public Card(string name, int hp, int dmg, string color, string? imageUrl)
        {
            Name = name;
            HP = hp;
            DMG = dmg;
            Color = color;
            ImageURL = imageUrl;
        }

        public void Update(string name, int hp, int dmg, string color, string? imageUrl)
        {
            Name = name;
            HP = hp;
            DMG = dmg;
            Color = color;
            if (ImageURL != null)
            {
                ImageURL = imageUrl;
            }
        }
        
        public void TakeDamage(int DMG)
        {
            HP = HP - DMG;
        }

        public CardResponse ToCardResponse()
        {
            return new CardResponse(Id, Name, HP, DMG, Color, ImageURL);
        }
    }
}
