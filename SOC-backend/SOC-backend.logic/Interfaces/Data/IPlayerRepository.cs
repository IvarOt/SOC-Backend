
using SOC_backend.logic.Models.Player;

namespace SOC_backend.logic.Interfaces.Data
{
    public interface IPlayerRepository
    {
        Task Register(Player player);
        Task<Player> Login(Player player);
        Task<Player> GetProfileInfo(int id);


    }
}
