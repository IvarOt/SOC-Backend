using Microsoft.EntityFrameworkCore;
using SOC_backend.logic.Models.DomainModel;

namespace SOC_backend.data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CardModel> Card { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
