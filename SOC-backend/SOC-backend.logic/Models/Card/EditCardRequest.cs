using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Models.Card
{
    public class EditCardRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int DMG { get; set; }
        public string Color { get; set; }
        public IFormFile? Image { get; set; }

        public Card ToCardModel()
        {
            return new Card(Id, Name, HP, DMG, Color);
        }
    }
}
