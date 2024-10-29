using SOC_backend.logic.ExceptionHandling.Exceptions;
using SOC_backend.logic.Interfaces.Data;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Models.Player;
using System.Numerics;

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

        public async Task<PlayerProfileResponse> GetProfileInfo(int id)
        {
            var player = await _playerRepository.GetProfileInfo(id);
            var profileInfo = player.ToPlayerProfileResponse();
            return profileInfo;
        }

        public async Task Register(RegisterPlayerRequest newPlayer)
        {
            if (PasswordsMatch(newPlayer))
            {
                Player player = newPlayer.ToPlayer();
                await _playerRepository.Register(player);
            }
        }

        public async Task<PlayerLoginResponse> Login(PlayerLoginRequest loginRequest)
        {
            Player player = loginRequest.ToPlayer();
            var retreivedPlayer = await _playerRepository.Login(player);
            if (BCrypt.Net.BCrypt.EnhancedVerify(loginRequest.Password, retreivedPlayer.Password))
            {
				string token = _tokenService.CreateToken(retreivedPlayer);
                PlayerLoginResponse response = new PlayerLoginResponse(token, retreivedPlayer.Username);
				return response;
			}
            else
            {
                throw new PropertyException("Passwords don't match", nameof(player.Password));
            }
		}

        private bool PasswordsMatch(RegisterPlayerRequest player)
        {
            if (player.Password == player.ConfirmPassword)
            {
                return true;
            }
            else
            {
                throw new PropertyException("Passwords don't match", nameof(player.Password));
            }
        }
    }
}
