using SOC_backend.logic.ExceptionHandling.Exceptions;
using System.Drawing;

namespace SOC_backend.logic.Models.Card
{
    public class Card
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int HP { get; private set; }
        public int DMG { get; private set; }
        public string Color { get; private set; }

        //For entity framework
        public Card() { }

        //Database / edit card
        public Card(int id, string name, int hp, int dmg, string color)
        {
            Id = id;
            Name = ValidateString(name, 3, 30);
            HP = ValidateInt(hp, 1, 30);
            DMG = ValidateInt(dmg,  0, 30);
            Color = ValidateString(color, 1, 20); 
        }

        //CardRequest
        public Card(string name, int hp, int dmg, string color)
        {
            Name = ValidateString(name, 3, 30);
            HP = ValidateInt(hp, 1, 30);
            DMG = ValidateInt(dmg, 0, 30);
            Color = ValidateString(color, 7, 7);
        }

        public void Update(int id, string name, int hp, int dmg, string color)
        {
            Id = id;
            Name = name;
            HP = hp;
            DMG = dmg;
            Color = color;
        }

        public CardResponse ToCardResponse()
        {
            return new CardResponse(Id, Name, HP, DMG, Color);
        }

        private string ValidateString(string value, int minLength, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new PropertyException($"{nameof(value)} cannot be empty..", nameof(value));
            }
            if (value.Length < minLength || value.Length > maxLength)
            {
                throw new PropertyException($"{nameof(value)} has to be between {minLength} and {maxLength} long..", nameof(value));
            }
            return value;
        }

        private int ValidateInt(int value, int minValue, int maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                throw new PropertyException($"{nameof(value)} must be between {minValue} and {maxValue}..",  nameof(value));
            }
            return value;
        }
    }
}
