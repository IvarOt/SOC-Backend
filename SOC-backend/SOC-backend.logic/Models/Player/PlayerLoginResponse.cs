using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Models.Player
{
    public class PlayerLoginResponse
    {
        public string AccesToken { get; private set; }
        public string RefreshToken { get; private set; }

        public PlayerLoginResponse(string token, string refreshToken)
        {
            AccesToken = token;
            RefreshToken = refreshToken;
        }
    }
}
