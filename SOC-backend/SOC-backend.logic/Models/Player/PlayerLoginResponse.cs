using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Models.Player
{
    public class PlayerLoginResponse
    {
        public string Token { get; private set; }
        public string Username { get; private set; }

        public PlayerLoginResponse(string token, string username)
        {
            Token = token;
            Username = username;
        }
    }
}
