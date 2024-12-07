using Microsoft.EntityFrameworkCore;
using SOC_backend.logic.Models.Cards;
using SOC_backend.logic.Models.Match;
using SOC_backend.logic.Models.Player;

namespace SOC_backend.data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Card> Card { get; set; } = null!;
		public DbSet<Player> Player { get; set; } = null!;
		public DbSet<GameState> GameState { get; set; } = null!;
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<OpponentCard>()
				.HasKey(oc => new { oc.OpponentId, oc.CardId });

            modelBuilder.Entity<OpponentCard>()
				.HasOne(oc => oc.Opponent)
				.WithMany(o => o.Cards)
				.HasForeignKey(oc => oc.OpponentId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<OpponentCard>()
				.HasOne(oc => oc.Card)
				.WithMany()
				.HasForeignKey(oc => oc.CardId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ShopCard>()
				.HasKey(oc => new { oc.ShopId, oc.CardId });

			modelBuilder.Entity<Opponent>()
				.HasOne(o => o.Shop)
				.WithOne()
				.HasForeignKey<Shop>()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShopCard>()
				.HasOne(oc => oc.Shop)
				.WithMany(o => o.AvailableCards)
				.HasForeignKey(oc => oc.ShopId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ShopCard>()
				.HasOne(oc => oc.Card)
				.WithMany()
				.HasForeignKey(oc => oc.CardId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
