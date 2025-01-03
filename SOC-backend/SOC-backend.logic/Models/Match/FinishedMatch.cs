using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Models.Match
{
    public class FinishedMatch
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public bool DidWin {  get; set; }
        public string OpponentName { get; set; }
        public DateTime DateTime { get; set; }

        public FinishedMatch(bool didWin, string opponentName, int playerId)
        {
            DidWin = didWin;
            OpponentName = opponentName;
            DateTime = DateTime.Now;
            PlayerId = playerId;
        }
    }
}
