using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Data
{
    public class SamuraiDbContext: DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        public SamuraiDbContext(DbContextOptions<SamuraiDbContext> options) : base(options)
        {

        }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb; Database= SamuraiAppCoreDb; Trusted_Connection=True; ");
        }
        */
    }
}
