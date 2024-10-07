
using SOC_backend.logic.Models.Player;

namespace SOC_backend.logic.Interfaces.Logic
{
    public interface IPlayerService
    {
        Task<string> Register(RegisterPlayerRequest newPlayer);
        Task<string> Login(PlayerLoginRequest loginRequest);
    }
}
