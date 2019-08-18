

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TankMonitor.SiteDb
{
    public class SiteContext : DbContext
    {
        public SiteContext(DbContextOptions<SiteContext> options)
            : base(options)
        { }

        //public SiteContext() { }

        public DbSet<Site> Sites { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Tank> Tanks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Site>()
                .HasIndex(s => s.SiteNumber)
                .IsUnique();

        }
    }

    public class Site
    {
        public int SiteId { get; set; }
        public string SiteNumber { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public string IpAddress { get; set; }
        public string PortNumber { get; set; }
        
        public List<Tank> Tanks { get; set; }

        public List<Inventory> Inventories { get; set; }
    }

    public class Inventory
    {
        public int InventoryId { get; set; }
        public string SiteNumber { get; set; }
        public string Date { get; set; }
        public string Product { get; set; }

        public int TankNumber { get; set; }
        public int TankVolume { get; set; }

        public int SiteId { get; set; }
        public Site Site { get; set; }
    }

    public class Tank
    {
        public int TankId { get; set; }
        public int TankNumber { get; set; }
        public int TankSize { get; set; }
        public string TankProduct { get; set; }
        public int SiteId { get; set; }
        public Site Site { get; set; }
    }
}