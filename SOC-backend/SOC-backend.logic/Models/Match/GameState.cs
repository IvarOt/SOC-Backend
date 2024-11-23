
namespace SOC_backend.logic.Models.Match
{
    public class GameState
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public List<Opponent> Players { get; set; }
        public bool PlayersTurn { get; set; }

        public GameState() { }
        public GameState(int playerId)
        {
            Players = new List<Opponent>();
            Opponent Player = new Opponent("Me", 30, 1);
            Opponent Enemy = new Opponent("Bob", 30, 1);
            Players.Add(Player);
            Players.Add(Enemy);
            PlayersTurn = true;
            PlayerId = playerId;
        }
    }
}
