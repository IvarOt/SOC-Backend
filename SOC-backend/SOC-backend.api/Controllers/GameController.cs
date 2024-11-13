using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOC_backend.logic.Interfaces.Logic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SOC_backend.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("StartGame")]
        public async Task<ActionResult> StartGame()
        {
            var userId = GetUserId();
            var gameState = await _gameService.StartNewGame(userId);
            return Ok(gameState);
        }

        private int GetUserId()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = HttpContext.Request.Headers.Authorization.ToString();
            var token = tokenHandler.ReadJwtToken(jwtToken);
            var userId = int.Parse(token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            return userId;
        }
    }
}
