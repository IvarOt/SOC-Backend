using Microsoft.EntityFrameworkCore;
using SOC_backend.logic.Interfaces.Data;
using SOC_backend.logic.Models.Match;
using SOC_backend.logic.Models.Player;

namespace SOC_backend.data.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;
        public PlayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Player> GetPlayer(int id)
        {
            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                throw new KeyNotFoundException("Player not found");
            }
            else
            {
                return player;
            }
        }

        public async Task ChangeAvatar(int playerId, string imageURL)
        {
            var player = await _context.Player.FirstOrDefaultAsync(x => x.Id == playerId);
            if (player == null)
            {
                throw new KeyNotFoundException("Player not found");
            }
            else
            {
                player.ProfileAvatar = imageURL;
                await _context.SaveChangesAsync();
            }
        }

        public async Task StoreRefreshToken(int playerId, string refreshToken)
        {
            var player = await _context.Player.FirstOrDefaultAsync(x => x.Id == playerId);
            if (player == null)
            {
                throw new InvalidOperationException();
            }
            player.RefreshToken = refreshToken;
            player.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();
        }

        public async Task<Player> GetMatchingPlayer(string refreshToken)
        {
            var player = await _context.Player.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if (player == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                return player;
            }
        }

        public async Task Register(Player player)
        {
            await _context.Player.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public async Task<Player> Login(Player player)
        {
            var existingPlayer = await _context.Player.FirstOrDefaultAsync(x => x.Username == player.Username);
			if (existingPlayer == null)
            {
                throw new InvalidOperationException("No player found with those credentials");
			}
            else
            {
                return existingPlayer;
            }
		}

        public async Task<List<FinishedMatch>> GetMatchHistory(int playerId)
        {
            var matches = await _context.finishedMatch.Where(x => x.PlayerId == playerId).OrderByDescending(x => x.DateTime).ToListAsync();
            if (matches == null)
            {
                throw new KeyNotFoundException("No matches found");
            }
            return matches;
        }
    }
}
