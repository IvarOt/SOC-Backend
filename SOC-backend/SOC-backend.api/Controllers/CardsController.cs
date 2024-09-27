using Microsoft.AspNetCore.Mvc;
using SOC_backend.logic.Interfaces.Logic;
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
		public async Task<ActionResult> GetCard(int id)
		{
			try
			{
				CardResponse card = await _cardService.GetCard(id);
				return Ok(card);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.InnerException.Message);
			}
		}

		[HttpGet("[action]")]
		public async Task<ActionResult<List<CardResponse>>> GetAllCards()
		{
			if (ModelState.IsValid)
			{
				try
				{
					List<CardResponse> cards = await _cardService.GetAllCards();
					return Ok(cards);
				}
				catch (Exception ex)
				{
					if (ex.InnerException != null)
					{
						return BadRequest(ex.InnerException.Message);
					}
					else
					{
						return BadRequest();
					}
				}
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost("[action]")]
		public async Task<ActionResult> CreateCard(CardRequest requestCard)
		{
			try
			{
				await _cardService.CreateCard(requestCard);
				return Ok();
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					return BadRequest(ex.InnerException.Message);
				}
				else
				{
					return BadRequest();
				}
			}
		}

		[HttpPut("[action]")]
		public async Task<ActionResult> EditCard(EditCardRequest card)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _cardService.EditCard(card);
					return Ok();
				}
				catch (Exception ex)
				{
					if (ex.InnerException != null)
					{
						return BadRequest(ex.InnerException.Message);
					}
					else
					{
						return BadRequest();
					}
				}
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpDelete("[action]")]
		public async Task<ActionResult> DeleteCard(int id)
		{
			if (ModelState.IsValid)
			{
				try
				{
					return Ok();
				}
				catch (Exception ex)
				{
					if (ex.InnerException != null)
					{
						return BadRequest(ex.InnerException.Message);
					}
					else
					{
						return BadRequest();
					}
				}
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
