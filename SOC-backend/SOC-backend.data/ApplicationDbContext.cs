using Microsoft.EntityFrameworkCore;
using SOC_backend.logic.Models.Card;
using SOC_backend.logic.Models.Player;

namespace SOC_backend.data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Card> Card { get; set; } = null!;
        public DbSet<Player> Player {  get; set; } = null!; 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
