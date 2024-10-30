﻿using SOC_backend.logic.ExceptionHandling.Exceptions;
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
            if (RegisterPasswordsMatch(newPlayer.Password, newPlayer.ConfirmPassword))
            {
                Player player = newPlayer.ToPlayer();
                await _playerRepository.Register(player);
            }
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
                throw new PropertyException("Password is incorrect", "password");
            }
        }

        public async Task<string> RefreshAccesToken(string refreshToken)
        {
            Player player = await _playerRepository.GetMatchingPlayer(refreshToken);
            if (player.RefreshTokenExpiry < DateTime.UtcNow)
            {
                throw new PropertyException("Refresh token is expired", "RefreshToken");
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

        public bool RegisterPasswordsMatch(string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                return true;
            }
            else
            {
                throw new PropertyException("Passwords don't match", "password");
            }
        }
    }
}