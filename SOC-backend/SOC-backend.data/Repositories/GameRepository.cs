﻿using Microsoft.EntityFrameworkCore;
using SOC_backend.logic.Interfaces.Data;
using SOC_backend.logic.Models.Match;

namespace SOC_backend.data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateNewGame(GameState gameState)
        {
            await _context.GameState.AddAsync(gameState);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGame(GameState gameState)
        {
            var previousGameState = await GetGameState(gameState.PlayerId);

            previousGameState.Update(gameState);

            await _context.SaveChangesAsync();
        }

        public async Task<GameState> GetGameState(int playerId)
        {
            var gameState = await _context.GameState
                .Include(x => x.Players)
                .ThenInclude(x => x.Cards)
                .ThenInclude(x => x.Card)
                .Include(x => x.Players)
                .ThenInclude(x => x.Shop)
                .ThenInclude(x => x.CardsForSale)
                .ThenInclude(x => x.Card)
                .Include(x => x.Players)
                .ThenInclude(x => x.Shop)
                .ThenInclude(x => x.AvailableCards)
                .Where(x => x.PlayerId == playerId)
                .FirstOrDefaultAsync();
            if (gameState == null)
            {
                throw new KeyNotFoundException("No active game found");
            }
            return gameState;
        }

        public async Task EndGame(GameState gameState, FinishedMatch finishedMatch)
        {
            _context.GameState.Remove(gameState);
            await _context.finishedMatch.AddAsync(finishedMatch);
            await _context.SaveChangesAsync();
        }
    }
}

