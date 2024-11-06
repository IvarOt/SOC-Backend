namespace SOC_backend.logic.Models.Card
{
    public class CardResponse
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int HP { get; private set; }
        public int DMG { get; private set; }
        public string Color { get; private set; }
        public string ImageURL { get; private set; }

        public CardResponse(int id, string name, int hp, int dmg, string color, string imageURL)
        {
            Id = id;
            Name = name;
            HP = hp;
            DMG = dmg;
            Color = color;
            ImageURL = imageURL;
        }
    }
}
