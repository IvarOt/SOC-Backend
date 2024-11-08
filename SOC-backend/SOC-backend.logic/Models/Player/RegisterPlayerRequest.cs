using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Models.Player
{
    public class RegisterPlayerRequest : PlayerRequest
    {
        [Required]
		[Compare("Password", ErrorMessage = "Passwords dont match")]
		public string ConfirmPassword { get; set; }

        public RegisterPlayerRequest() { }

        public Player ToPlayer()
        {
            var player = new Player(Username, Email, Password);
            return player;
        }
    }
}
