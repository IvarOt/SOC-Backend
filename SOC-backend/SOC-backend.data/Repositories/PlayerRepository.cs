using Microsoft.EntityFrameworkCore;
using SOC_backend.logic.ExceptionHandling.Exceptions;
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

        public async Task Register(Player player)
        {
            await _context.Player.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Login(Player player)
        {
            var result = await _context.Player.FirstOrDefaultAsync(x => x.Username == player.Username);
			if (result == null)
            {
				throw new NotFoundException("Player", player.Id);
			}
            else
            {
                return true;
            }
		}
    }
}
