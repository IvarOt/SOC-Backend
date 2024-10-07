using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Models.Card
{
    public class EditCardRequest
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int HP { get; private set; }
        public int DMG { get; private set; }
        public string Color { get; private set; }


        public EditCardRequest(int id, string name, int hp, int dmg, string color)
        {
            Id = id;
            Name = name;
            HP = hp;
            DMG = dmg;
            Color = color;
        }

        public Card ToCardModel()
        {
            return new Card(Id, Name, HP, DMG, Color);
        }
    }
}
