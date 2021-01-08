using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeData>().Property(model => model.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ServiceInfo>().Property(model => model.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Subscription>().HasKey(new string[] { "Username", "ServiceId" });
        }

        public IQueryable<ServiceInfo> GetSubscribedServices(string username)
        {
            return Subscriptions.Where(sub => sub.Username == username).Select(sub => sub.Service);
        }
    }
}
