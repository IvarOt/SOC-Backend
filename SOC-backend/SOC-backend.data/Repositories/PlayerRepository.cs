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
    }
}
