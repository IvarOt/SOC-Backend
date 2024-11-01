using System.Text.RegularExpressions;
using SOC_backend.logic.Models.Match;
using System.Diagnostics.CodeAnalysis;
using SOC_backend.logic.Exceptions;

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

        //entity framework
        public Player() { }

		//Profile function?
		public Player(int id, string username, string email)
		{
			Id = id;
			Username = username;
			Email = email;
			Role = "player";
		}

		//Register request
		public Player(string username, string email, string password)
        {
            Username = ValidateUsername(username);
            Email = ValidateEmail(email);
            Password = HashPassword(password);
            Role = "player";
        }

		//Login request
		public Player(string username, string password)
		{
			Username = username;
			Password = password;
		}

        public PlayerProfileResponse ToPlayerProfileResponse()
        {
            return new PlayerProfileResponse(Username, Email);
        }

        private string ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 20)
            {
                throw new PropertyException($"{nameof(Username)} must be between 3 and 20 characters.", nameof(Username));
            }
            return username;
        }

        private string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new PropertyException($"{nameof(Email)} must be a valid email address.", nameof(Email));
            }
            return email;
        }

        private string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                throw new PropertyException($"{nameof(Password)} must be at least 6 characters long.", nameof(Password));
            }
            else
            {
                return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            }
        }
    }
}
