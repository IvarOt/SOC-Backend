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
            var token = await _playerService.Register(newPlayer);
            return Ok(token);
        }

        [HttpGet]
        public async Task<ActionResult> Login(PlayerLoginRequest loginRequest)
        {
            var token = _playerService.Login(loginRequest);
            return Ok(token);
        }
    }
}
