using SOC_backend.logic.Models.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Models.Request
{
    public class EditCardRequest
    {
		public int Id { get; private set; }
		public string Name { get; private set; }
		public int HP { get; private set; }
		public int DMG { get; private set; }


		public EditCardRequest(int id, string name, int hp, int dmg)
		{
			Id = id;
			Name = name;
			HP = hp;
			DMG = dmg;
		}

		public CardModel ToCardModel()
		{
			return new CardModel(Id, Name, HP, DMG);
		}
	}
}
