using SOC_backend.logic.Models.DomainModel;

namespace SOC_backend.logic.Models.Request
{
    public class CardRequest
    {
        public string Name { get; private set; }
        public int HP { get; private set; }
        public int DMG { get; private set; }


		public CardRequest(string name, int hp, int dmg)
		{
			Name = name;
			HP = hp;
			DMG = dmg;
		}

		public CardModel ToCardModel()
        {
            return new CardModel(Name, HP, DMG);
        }
    }
}
