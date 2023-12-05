﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ShipmentCondition> TransferConditions { get; set; }
        public DbSet<Analytic> Analytics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
