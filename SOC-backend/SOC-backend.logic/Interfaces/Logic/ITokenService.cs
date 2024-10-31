using SOC_backend.logic.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Interfaces.Logic
{
    public interface ITokenService
    {
        string CreateAccesToken(Player player);
        string CreateRefreshToken();
    }
}
