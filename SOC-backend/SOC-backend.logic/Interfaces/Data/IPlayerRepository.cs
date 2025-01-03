
using SOC_backend.logic.Models.Match;
using SOC_backend.logic.Models.Player;

namespace SOC_backend.logic.Interfaces.Data
{
    public interface IPlayerRepository
    {
        Task Register(Player player);
        Task<Player> Login(Player player);
        Task<Player> GetPlayer(int id);
        Task StoreRefreshToken(int playerId, string refreshToken);
        Task<List<FinishedMatch>> GetMatchHistory(int playerId);
        Task<Player> GetMatchingPlayer(string refreshToken);
        Task ChangeAvatar(int playerId, string imageURL);
    }
}
