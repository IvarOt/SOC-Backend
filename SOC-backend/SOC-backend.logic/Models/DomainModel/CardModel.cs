using SOC_backend.logic.ExceptionHandling.Exceptions;
using SOC_backend.logic.Models.Response;

namespace SOC_backend.logic.Models.DomainModel
{
    public class CardModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int HP { get; private set; }
        public int DMG { get; private set; }

        //For entity framework
        public CardModel() { }

        //Every property
        public CardModel(int id, string name, int hp, int dmg)
        {
            Id = id;
            Name = ValidateName(name, 3, 30);
			HP = ValidateInt(hp, "hp", 1, 30);
			DMG = ValidateInt(dmg, "dmg", 0, 30);
		}

        //CardRequest
        public CardModel(string name, int hp, int dmg)
        {
            Name = ValidateName(name, 3, 30);
            HP = ValidateInt(hp, "hp", 1, 30);
            DMG = ValidateInt(dmg, "dmg", 0, 30);
        }

        public void Update(int id, string name, int hp, int dmg)
        {
            Id = id;
            Name = name;
            HP = hp; 
            DMG = dmg;
        }

        public CardResponse ToCardResponse()
        {
            return new CardResponse(Id, Name, HP, DMG);
        }

        private string ValidateName(string name, int minLength, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
				throw new PropertyException("Name cannot be empty..", nameof(name));
			}
			if (name.Length < minLength || name.Length > maxLength)
            {
                throw new PropertyException($"Name has to be between {minLength} and {maxLength} long..", nameof(name));
			}
			return name;
        }

        private int ValidateInt(int value, string property, int minValue, int maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                throw new PropertyException($"Number must be between {minValue} and {maxValue}..", property);
            }
            return value;
        }
    }
}
