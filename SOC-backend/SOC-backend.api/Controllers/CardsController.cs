using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOC_backend.logic.Interfaces;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Models;
using SOC_backend.logic.Models.Request;
using SOC_backend.logic.Models.Response;

namespace SOC_backend.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetCard()
        {
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<CardResponse>>> GetAllCards()
        {
            try
            {
                List<CardResponse> cards = await _cardService.GetAllCards();
                return Ok(cards);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> CreateCard(CardRequest requestCard)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _cardService.CreateCard(requestCard);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> EditCard()
        {
            return Ok();
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteCard()
        {
            return Ok();
        }
    }
}
