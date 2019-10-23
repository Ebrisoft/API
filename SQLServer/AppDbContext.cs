using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
using System;

namespace SQLServer
{
    public class AppDbContext : IdentityDbContext<ApplicationUserDbo>
    {
        //  Properties
        //  ==========

        public DbSet<IssueDbo> Issues { get; set; } = null!;
        public DbSet<HouseDbo> Houses { get; set; } = null!;

        //  Constructors
        //  ============

        public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
        {

        }

        //  Methods
        //  =======

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            SetUpPrimarykeys(modelBuilder);
            SetUpOneToManyRelationships(modelBuilder);
            SeedRoles(modelBuilder);            
        }

        private void SetUpPrimarykeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HouseDbo>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<IssueDbo>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<ApplicationUserDbo>()
                .HasKey(h => h.Id);
        }

        private void SetUpOneToManyRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HouseDbo>()
                .HasMany(h => h.Issues)
                .WithOne(i => i.House);
            
            modelBuilder.Entity<ApplicationUserDbo>()
                .HasMany(l => l.Houses)
                .WithOne(h => h.Landlord);
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                   new IdentityRole
                   {
                       Name = Abstractions.Roles.Tenant,
                       NormalizedName = Abstractions.Roles.Tenant.ToUpperInvariant()
                   },
                   new IdentityRole
                   {
                       Name = Abstractions.Roles.Landlord,
                       NormalizedName = Abstractions.Roles.Landlord.ToUpperInvariant()
                   });
        }
    }
}