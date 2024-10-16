using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Models.Match
{
    public class FinishedMatch
    {
        public int Id { get; private set; }
        public string Opponent { get; private set; }
        public DateTime Date { get; private set; }
        public bool IsWinner {  get; private set; }

        public FinishedMatch(string opponent, bool isWinner)
        {
            Opponent = opponent;
            IsWinner = isWinner;
            Date = DateTime.Now;
        }
    }
}
