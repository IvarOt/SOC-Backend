
namespace SOC_backend.logic.Models.Player
{
    public class PlayerLoginRequest
    {
        public string Username {  get; set; }
        public string Password { get; set; }

        public PlayerLoginRequest() { }

        public Player ToPlayer()
        {
            return new Player(Username, Password);
        }
    }
}
