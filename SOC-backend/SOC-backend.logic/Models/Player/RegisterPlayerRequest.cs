using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Models.Player
{
    public class RegisterPlayerRequest
    {
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string ConfirmPassword { get; private set; }

        public RegisterPlayerRequest(string username, string email, string password, string confirmpassword)
        {
            Username = username;
            Email = email;
            Password = password;
            ConfirmPassword = confirmpassword;
        }

        public Player ToPlayer()
        {
            var player = new Player(Username, Email, Password);
            return player;
        }
    }
}
