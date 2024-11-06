using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SOC_backend.logic.Models.Card
{
    public class CardRequest
    {
        [Required]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 30)]
        public int HP { get; set; }

        [Required]
        [Range(0, 20)]
        public int DMG { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Color { get; set; } = string.Empty;
    }
}
