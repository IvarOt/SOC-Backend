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

        //Every property
        public Card(int id, string name, int hp, int dmg, string color)
        {
            Id = id;
            Name = ValidateString(name, "name", 3, 30);
            HP = ValidateInt(hp, "hp", 1, 30);
            DMG = ValidateInt(dmg, "dmg", 0, 30);
            Color = color;
        }

        //CardRequest
        public Card(string name, int hp, int dmg, string color)
        {
            Name = ValidateString(name, "name", 3, 30);
            HP = ValidateInt(hp, "hp", 1, 30);
            DMG = ValidateInt(dmg, "dmg", 0, 30);
            Color = color;
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

        private string ValidateString(string value, string property, int minLength, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new PropertyException($"{property} cannot be empty..", nameof(property));
            }
            if (value.Length < minLength || value.Length > maxLength)
            {
                throw new PropertyException($"{property} has to be between {minLength} and {maxLength} long..", nameof(property));
            }
            return value;
        }

        private int ValidateInt(int value, string property, int minValue, int maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                throw new PropertyException($"{property} must be between {minValue} and {maxValue}..", property);
            }
            return value;
        }
    }
}
