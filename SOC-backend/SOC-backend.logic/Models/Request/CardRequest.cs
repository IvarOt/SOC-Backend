using SOC_backend.logic.Models.DomainModel;

namespace SOC_backend.logic.Models.Request
{
    public class CardRequest
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int HP { get; private set; }
        public int DMG { get; private set; }

        public CardModel ToCardModel()
        {
            return new CardModel(Id, Name, HP, DMG);
        }
    }
}
