﻿using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Models.Player;

namespace SOC_backend.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterPlayerRequest newPlayer)
        {
            if (ModelState.IsValid)
            {
				await _playerService.Register(newPlayer);
				return Ok();
			}
            else
            {
                return BadRequest();
            }
		}

        [HttpPost("Login")]
        public async Task<ActionResult> Login(PlayerLoginRequest loginRequest)
        {
            var player = await _playerService.Login(loginRequest);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("RefreshToken", player.RefreshToken, cookieOptions);
            return Ok(player.AccesToken);
        }

        [HttpPut("ChangeAvatar")]
        [Authorize]
        public async Task<ActionResult> ChangeAvatar(int playerId, string imageURL)
        {
            await _playerService.ChangeAvatar(playerId, imageURL);
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Profile(int id)
        {
            var playerInfo = await _playerService.GetProfileInfo(id);
            return Ok(playerInfo);
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<ActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["RefreshToken"];
            if (refreshToken == null)
            {
                return Unauthorized("No refreshToken");
            }
            var accesToken = await _playerService.RefreshAccesToken(refreshToken);
            
            return Ok(accesToken);
        }

        [HttpGet("MatchHistory")]
        [Authorize]
        public async Task<ActionResult> GetMatchHistory(int playerId)
        {
            var matchHistory = await _playerService.GetMatchHistory(playerId); 
            return Ok(matchHistory);
        }
    }
}
