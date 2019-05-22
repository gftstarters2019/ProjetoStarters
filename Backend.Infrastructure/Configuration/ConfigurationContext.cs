using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Configuration
{
    public sealed class ConfigurationContext : DbContext
    {

        public ConfigurationContext(DbContextOptions<ConfigurationContext> options) : base(options)
        {

        }

        public DbSet<Individual> Individuals { get; set; }
        public DbSet<Realty> Reaties { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<MobileDevice> MobileDevices { get; set; }
        public DbSet<Contract> Contracts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Individual>();
            builder.Entity<Realty>();
            builder.Entity<Pet>();
            builder.Entity<Vehicle>();
            builder.Entity<MobileDevice>();
            builder.Entity<Contract>();
        }
    }

    

   
}
