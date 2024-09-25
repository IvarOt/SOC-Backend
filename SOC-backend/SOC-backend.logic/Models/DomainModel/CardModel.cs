using SOC_backend.logic.Models.Response;

namespace SOC_backend.logic.Models.DomainModel
{
    public class CardModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int HP { get; private set; }
        public int DMG { get; private set; }

        public CardModel(int id, string name, int hp, int dmg)
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
    }
}
