
namespace SOC_backend.logic.Models.Match
{
    public class GameState
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Opponent Player { get; set; }
        public Opponent Opponent { get; set; }
        public bool PlayersTurn { get; set; }

        public GameState() { }
        public GameState(int playerId)
        {
            Player = new Opponent("Me", 30, 1);
            Opponent = new Opponent("Bob", 30, 1);
            PlayersTurn = true;
            PlayerId = playerId;
        }
    }
}
