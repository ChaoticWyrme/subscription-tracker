using System;
using Microsoft.EntityFrameworkCore;

namespace SubWatchApi.Models
{
    public class SubWatchContext : DbContext
    {
        public SubWatchContext(DbContextOptions<SubWatchContext> options) : base(options)
        {
        }

        public DbSet<ServiceInfo> Services { get; set; }

        public DbSet<TimeData> TimeDatas { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TimeData>().Property<>
        //}
    }
}
