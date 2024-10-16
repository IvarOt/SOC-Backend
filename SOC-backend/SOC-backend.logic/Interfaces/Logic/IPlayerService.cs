
using SOC_backend.logic.Models.Player;

namespace SOC_backend.logic.Interfaces.Logic
{
    public interface IPlayerService
    {
        Task Register(RegisterPlayerRequest newPlayer);
        Task<PlayerLoginResponse> Login(PlayerLoginRequest loginRequest);
    }
}
