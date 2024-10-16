using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Models.Player
{
    public class PlayerProfileResponse
    {
        public string Username { get; private set; }
        public string Email { get; private set; }

        public PlayerProfileResponse(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}
