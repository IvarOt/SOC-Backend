using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SOC_backend.logic.Models.Cards
{
    public class EditCardRequest : CardRequest
    {
        [Required]
        public int Id { get; set; }

        public IFormFile? Image { get; set; }

        public EditCardRequest() { }

        public Card ToCardModel()
        {
            return new Card(Id, Name, HP, DMG, Color, null, Cost);
        }
    }
}
