using SOC_backend.logic.Interfaces.Data;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Models.Player;

namespace SOC_backend.logic.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITokenService _tokenService;
        public PlayerService(IPlayerRepository playerRepository, ITokenService tokenService)
        {
            _playerRepository = playerRepository;
            _tokenService = tokenService;
        }

        public async Task<string> Register(RegisterPlayerRequest newPlayer)
        {
            Player player = newPlayer.ToPlayer();
            await _playerRepository.Register(player);
            string token = _tokenService.CreateToken(player);
            return token;
        }

        public async Task<string> Login(PlayerLoginRequest loginRequest)
        {
            Player player = new Player();
            string token = _tokenService.CreateToken(player);
            return token;
        }
    }
}
