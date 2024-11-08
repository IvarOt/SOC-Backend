using System.ComponentModel.DataAnnotations;

namespace SOC_backend.logic.Models.Player
{
	public class PlayerRequest
	{
		[Required]
		[StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
		public string Password { get; set; }

		[Required]
		public string Role { get; set; }

		public PlayerRequest() { }
	}
}
