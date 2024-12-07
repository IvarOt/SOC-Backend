using SOC_backend.logic.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Interfaces.Data
{
    public interface IGameRepository
    {
        Task CreateNewGame(GameState gameState);
        Task<GameState> GetGameState(int playerId);
        Task DeleteGame(GameState gameState);
    }
}
