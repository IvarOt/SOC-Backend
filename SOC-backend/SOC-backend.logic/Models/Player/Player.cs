namespace SOC_backend.logic.Models.Player
{
	public class Player
	{
		public int Id { get; private set; }
		public string Username { get; private set; }
		public string Email { get; private set; }
		public string Password { get; private set; }
		public string Role { get; private set; }
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpiry { get; set; }

		//Entity framework
		public Player() { }

		//Login
		public Player(string username, string password)
		{
			Username = username;
			Password = password;
		}

		//Register
		public Player(string username, string email, string password)
		{
			Username = username;
			Email = email;
			Password = HashPassword(password);
			Role = "player";
		}

		//Testing
		public Player(int id, string username, string password)
		{
			Id = id;
			Username = username;
			Password= password;
		}

		public Player(int id, string username)
		{
			Id = id;
			Username = username;
		}

		public Player(int id, string username, DateTime refreshTokenExpiry)
        {
            Id = id;
            Username = username;
			RefreshTokenExpiry = refreshTokenExpiry;
        }

		public PlayerProfileResponse ToPlayerProfileResponse()
		{
			return new PlayerProfileResponse(Username, Email);
		}



		private string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
		}
	}
}
