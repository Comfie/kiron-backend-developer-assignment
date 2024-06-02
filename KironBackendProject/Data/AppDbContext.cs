using KironBackendProject.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KironBackendProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define your DbSets (tables) here, if needed.
        public DbSet<User> Users { get; set; }
        public DbSet<BankHoliday> BankHolidays { get; set; }
        public DbSet<RegionBankHoliday> RegionBankHolidays { get; set; }
        public DbSet<Region> RegionHolidays { get; set; }
        public DbSet<Navigation> Navigation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RegionBankHoliday>()
                 .HasKey(rb => new { rb.RegionId, rb.BankHolidayId });

            modelBuilder.Entity<RegionBankHoliday>()
                .HasOne(rb => rb.Region)
                .WithMany(r => r.RegionBankHolidays)
                .HasForeignKey(rb => rb.RegionId);

            modelBuilder.Entity<RegionBankHoliday>()
                .HasOne(rb => rb.BankHoliday)
                .WithMany(b => b.RegionBankHolidays)
                .HasForeignKey(rb => rb.BankHolidayId);
        }
    }
}
