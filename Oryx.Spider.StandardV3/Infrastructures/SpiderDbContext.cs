using Microsoft.EntityFrameworkCore;
using System;

namespace Oryx.Spider.StandardV3.Infrastructures
{
    public class SpiderDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=spiderLogs.db");
        }

        public DbSet<SpiderLog> SpiderLogs { get; set; }
    }
}
