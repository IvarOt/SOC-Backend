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
            var gameState = await _gameService.ResolveFight(1);
            return Ok(gameState);
        }

        [HttpPost("PurchaseCard")]
        public async Task<ActionResult> PurchaseCard(int cardId)
        {
            var gameState = await _gameService.PurchaseCard(cardId);
            return Ok(gameState);
        }

        [HttpPost("EndGame")]
        public async Task<ActionResult> EndGame()
        {
            await _gameService.EndGame();
            return Ok();
        }
    }
}
