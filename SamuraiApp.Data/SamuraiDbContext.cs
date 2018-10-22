using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiDbContext: DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        public static readonly LoggerFactory LoggerFactotydb
                = new LoggerFactory( new[]{
                new ConsoleLoggerProvider((category, level)
                => category  == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information, true)
            });


        public SamuraiDbContext(DbContextOptions<SamuraiDbContext> options) : base(options)
        {
        }

        public SamuraiDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.SamuariId, s.BattleId });
        }
     
       
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLoggerFactory(LoggerFactotydb);
            builder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb; Database= SamuraiAppCoreDb; Trusted_Connection=True; ");
        }
       
    }
}
