﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SOC_backend.logic.Interfaces.Logic;
using SOC_backend.logic.Models.Cards;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SOC_backend.api.Controllers
{
    public class GameHub : Hub
    {
        private readonly IGameService _gameService;

        public GameHub(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task StartGame()
        {
            var gameState = await _gameService.StartNewGame(1);
            await Clients.Caller.SendAsync("gameState", gameState);
        }

        public async Task ResolveFight()
        {
            var gameState = await _gameService.ResolveFight(1);
            await Clients.Caller.SendAsync("gameState", gameState);
        }

        public async Task PurchaseCard(int cardId)
        {
            var gameState = await _gameService.PurchaseCard(cardId);
            await Clients.Caller.SendAsync("gameState", gameState);
        }

        public async Task EndGame()
        {
            await _gameService.EndGame();
            await Clients.Caller.SendAsync("GameEnded");
        }
    }
}