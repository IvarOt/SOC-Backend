using System.Drawing;

namespace SOC_backend.logic.Models.Card
{
    public class CreateCardRequest
    {
        public string Name { get; private set; }
        public int HP { get; private set; }
        public int DMG { get; private set; }
        public string Color { get; private set; }


        public CreateCardRequest(string name, int hp, int dmg, string color)
        {
            Name = name;
            HP = hp;
            DMG = dmg;
            Color = color;
        }

        public Card ToCardModel()
        {
            return new Card(Name, HP, DMG, Color);
        }
    }
}
