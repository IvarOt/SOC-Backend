using SOC_backend.logic.Models.Cards;
using SOC_backend.logic.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Interfaces.Logic
{
    public interface IGameService
    {
        Task<GameState> StartNewGame(int playerId);
        Task<GameState> ResolveFight(int playerId, List<Card> attackingPlayerCards, List<Card> attackingOpponentCards);
    }
}
