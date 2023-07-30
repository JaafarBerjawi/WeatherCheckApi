using Microsoft.EntityFrameworkCore;
using Security.Entities;
using WeatherCheck.Entities;

namespace Data.EFCore
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Username is unique in Users Table
            builder.Entity<User>().HasIndex(user => user.Username).IsUnique();
        }

        public DbSet<CurrentWeatherCondition> WeatherConditions { get; set; }
        public DbSet<Security.Entities.User> Users { get; set; }
        public DbSet<Security.Entities.UserToken> UserTokens { get; set; }
    }
}
