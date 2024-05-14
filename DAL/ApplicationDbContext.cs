using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<TransferCondition> TransferConditions { get; set; }
        public DbSet<Analysis> Analysises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.StartingLocation)
                .WithMany(l => l.StartedTransfers)
                .HasForeignKey(t => t.StartingLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.EndingLocation)
                .WithMany(l => l.FinishedTransfers)
                .HasForeignKey(t => t.EndingLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransferCondition>()
                .HasOne(tc => tc.Transfer)
                .WithOne(t => t.TransferCondition)
                .HasForeignKey<TransferCondition>(tc => tc.TransferId);

        }
    }
}
