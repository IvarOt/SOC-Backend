using Microsoft.AspNetCore.Authorization;
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

        public Task JoinGroup(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var gameState = await _gameService.GetGameState(8);
                var groupName = $"game-{gameState.Id}";
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                await Clients.Caller.SendAsync("gameState", gameState);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnConnectedAsync: {ex.Message}");
            }
            finally
            {
                await base.OnConnectedAsync();
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task GetGameState()
        {
            var gameState = await _gameService.GetGameState(8);
            await Clients.Caller.SendAsync("gameState", gameState);
        }

        public async Task StartGame()
        {
            var gameState = await _gameService.StartNewGame(8);
            var groupName = $"game-{gameState.Id}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("gameState", gameState);
        }

        public async Task ResolveFight()
        {
            var gameState = await _gameService.ResolveFight(8);
            var groupName = $"game-{gameState.Id}";
            await Clients.Group(groupName).SendAsync("gameState", gameState);
        }

        public async Task PurchaseCard(int cardId)
        {
            var gameState = await _gameService.PurchaseCard(cardId, 8);
            var groupName = $"game-{gameState.Id}";
            await Clients.Group(groupName).SendAsync("gameState", gameState);
        }

        public async Task PassTurn()
        {
            var gameState = await _gameService.PassTurn(8);
            var groupName = $"game-{gameState.Id}";
            await Clients.Group(groupName).SendAsync("gameState", gameState);
        }

        public async Task EndGame()
        {
            await _gameService.EndGame(8);
            var groupName = $"game-{8}";
            await Clients.Group(groupName).SendAsync("GameEnded");
        }
    }
}
