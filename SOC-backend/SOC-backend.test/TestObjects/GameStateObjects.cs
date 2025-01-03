using SOC_backend.logic.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.test.TestObjects
{
    public class GameStateObjects
    {
        public GameState testGameState;
        public GameState testGameState_ExpensiveCards;
        public CardObjects _cardObjects;
        public GameStateObjects()
        {
            _cardObjects = new CardObjects();
            testGameState = new GameState(_cardObjects.testCards, 1);
            testGameState_ExpensiveCards = new GameState(_cardObjects.expensiveTestCards, 1);
        }
    }
}
