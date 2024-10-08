﻿using Microsoft.AspNetCore.Mvc;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Models.Request;
using SOC_backend.logic.Models.Response;

namespace SOC_backend.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardResponse>> GetCard(int id)
        {
            CardResponse card = await _cardService.GetCard(id);
            return Ok(card);
        }

        [HttpGet]
        public async Task<ActionResult<List<CardResponse>>> GetAllCards()
        {
            List<CardResponse> cards = await _cardService.GetAllCards();
            return Ok(cards);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCard(CreateCardRequest requestCard)
        {
            await _cardService.CreateCard(requestCard);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> EditCard(EditCardRequest card)
        {
            await _cardService.EditCard(card);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCard(int id)
        {
            await _cardService.DeleteCard(id);
            return Ok();
        }
    }
}
