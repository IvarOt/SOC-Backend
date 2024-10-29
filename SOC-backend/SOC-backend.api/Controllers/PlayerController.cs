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
            await _playerService.Register(newPlayer);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(PlayerLoginRequest loginRequest)
        {
            var user = await _playerService.Login(loginRequest);
            return Ok(new { token = "Bearer " + user.Token, username = user.Username });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Profile(int id)
        {
            var playerInfo = await _playerService.GetProfileInfo(id);
            return Ok(playerInfo);
        }
    }
}
