
using SOC_backend.logic.Models.Match;
using SOC_backend.logic.Models.Player;

namespace SOC_backend.logic.Interfaces.Logic
{
    public interface IPlayerService
    {
        Task Register(RegisterPlayerRequest newPlayer);
        Task<PlayerLoginResponse> Login(PlayerLoginRequest loginRequest);
        Task<PlayerProfileResponse> GetProfileInfo(int id);
        Task<string> RefreshAccesToken(string refreshToken);
        Task<List<FinishedMatch>> GetMatchHistory(int playerId);
        Task ChangeAvatar(int playerId, string imageURL);
    }
}
