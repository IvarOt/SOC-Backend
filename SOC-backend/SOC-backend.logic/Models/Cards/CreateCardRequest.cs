using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SOC_backend.logic.Models.Cards
{
    public class CreateCardRequest : CardRequest
    {
        [Required]
        public IFormFile Image { get; set; }

        public CreateCardRequest() { }

        public Card ToCard()
        {
            return new Card(0, Name, HP, DMG, Color, null);
        }
    }
}
