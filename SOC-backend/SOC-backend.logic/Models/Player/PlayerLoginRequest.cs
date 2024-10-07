
namespace SOC_backend.logic.Models.Player
{
    public class PlayerLoginRequest
    {
        public string Username {  get; private set; }
        public string Password { get; private set; }

        public PlayerLoginRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
