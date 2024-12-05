using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Models.Cards;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SOC_backend.api.Controllers
{
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
            var gameState = await _gameService.StartNewGame(1);
            return Ok(gameState);
        }

        [HttpPost("ResolveFight")]
        public async Task<ActionResult> ResolveFight()
        {
            var cards = new List<Card>
            {
                new Card(1, "Bob", 5, 3, "GHUESWHGUIES", null),
                new Card(2, "Bobbie", 3, 2, "GHUESWHGUFA", null),
                new Card(2, "Bobbie", 3, 2, "GHUESWHGUFA", null),
            };

            var otherCards = new List<Card>
            {
                new Card(2, "Bobbie", 3, 2, "GHUESWHGUFA", null),
                new Card(1, "Bob", 5, 3, "GHUESWHGUIES", null),
            };
            var gameState = await _gameService.ResolveFight(1);
            return Ok(gameState);
        }
    }
}
