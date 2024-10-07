
namespace SOC_backend.logic.Models.Player
{
    public class Player
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        public Player() { }

        public Player(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
            Role = "player";
        }
    }
}
