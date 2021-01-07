using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace SubWatchApi.Models
{
    public class SubWatchContext : DbContext
    {
        public SubWatchContext(DbContextOptions<SubWatchContext> options) : base(options)
        {
        }

        public DbSet<ServiceInfo> Services { get; set; }

        public DbSet<TimeData> TimeDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeData>().HasNoKey();
            modelBuilder.Entity<ServiceInfo>().Property(model => model.Id).ValueGeneratedOnAdd();
        }
    }
}
