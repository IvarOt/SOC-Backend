using SOC_backend.logic.Interfaces.Data;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Models.Match;
using SOC_backend.logic.Models.Player;
using System.ComponentModel.DataAnnotations;
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

		public async Task<List<FinishedMatch>> GetMatchHistory(int playerId)
		{
            var matchHistory = await _playerRepository.GetMatchHistory(playerId);
            return matchHistory;
        }

		public async Task ChangeAvatar(int playerId, string imageURL)
		{
			await _playerRepository.ChangeAvatar(playerId, imageURL);
        }

		public async Task<PlayerProfileResponse> GetProfileInfo(int id)
		{
			var player = await _playerRepository.GetPlayer(id);
			var profileInfo = player.ToPlayerProfileResponse();
			return profileInfo;
		}

		public async Task Register(RegisterPlayerRequest newPlayer)
		{
			Player player = newPlayer.ToPlayer();
			await _playerRepository.Register(player);
		}

		public async Task<PlayerLoginResponse> Login(PlayerLoginRequest loginRequest)
		{
			Player player = loginRequest.ToPlayer();
			var retreivedPlayer = await _playerRepository.Login(player);
			if (LoginPasswordsMatch(player.Password, retreivedPlayer.Password))
			{
				string accesToken = _tokenService.CreateAccesToken(retreivedPlayer);
				string refreshToken = _tokenService.CreateRefreshToken();
				await _playerRepository.StoreRefreshToken(retreivedPlayer.Id, refreshToken);
				PlayerLoginResponse response = new PlayerLoginResponse(accesToken, refreshToken);
				return response;
			}
			else
			{
				throw new ValidationException("Incorrect credentials");
			}
		}

		public async Task<string> RefreshAccesToken(string refreshToken)
		{
			Player player = await _playerRepository.GetMatchingPlayer(refreshToken);
			if (player.RefreshTokenExpiry < DateTime.UtcNow)
			{
				throw new ValidationException();
			}
			else
			{
				string accesToken = _tokenService.CreateAccesToken(player);
				return accesToken;
			}
		}

		private bool LoginPasswordsMatch(string password, string passwordHash)
		{
			if (BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
