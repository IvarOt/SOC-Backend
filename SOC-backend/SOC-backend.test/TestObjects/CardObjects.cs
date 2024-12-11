using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC_backend.logic.Models.Cards;
using SOC_backend.logic.Models.Player;

namespace SOC_backend.test.TestObjects
{
    public class CardObjects
    {
        public Card testCard {  get; set; }
        public List<Card> testCards { get; set; }
        public List<Card> expensiveTestCards { get; set; }

        public CardObjects()
        {
            testCard = new Card(1, "test", 1, 1, "#TestColor", null, 1);
            testCards = new List<Card>
            {
                testCard, 
                new Card(2, "test", 1, 1, "#TestColor", null, 1)
            };
            expensiveTestCards = new List<Card>
            {
                new Card(3, "test", 1, 1, "#TestColor", null, 100),
                new Card(4, "test", 1, 1, "#TestColor", null, 100)
            };
        }
    }
}
