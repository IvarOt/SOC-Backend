using Microsoft.AspNetCore.Http;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace SOC_backend.logic.Models.Card
{
    public class CreateCardRequest
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int DMG { get; set; }
        public string Color { get; set; }
        public IFormFile Image { get; set; }

        public CreateCardRequest() { }

        public Card ToCardModel()
        {
            return new Card(Name, HP, DMG, Color);
        }
    }
}
