using Microsoft.EntityFrameworkCore;
using SOC_backend.logic.Exceptions;
using SOC_backend.logic.Interfaces.Data;
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

        public async Task<Player> GetProfileInfo(int id)
        {
            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                throw new NotFoundException("Player", id);
            }
            else
            {
                return player;
            }
        }

        public async Task StoreRefreshToken(int playerId, string refreshToken)
        {
            var player = await _context.Player.FirstOrDefaultAsync(x => x.Id == playerId);
            if (player == null)
            {
                throw new NotFoundException("Player", player.Id);
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
                throw new NotFoundException("Refresh token does not exist");
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
				throw new NotFoundException("Player", player.Id);
			}
            else
            {
                return existingPlayer;
            }
		}
    }
}
