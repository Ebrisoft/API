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
        public DbSet<ContactDbo> Contacts { get; set; } = null!;
        public DbSet<CommentDbo> Comments { get; set; } = null!;

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
                .HasKey(i => i.Id);

            modelBuilder.Entity<ApplicationUserDbo>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<ContactDbo>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CommentDbo>()
                .HasKey(c => c.Id);
        }

        private void SetUpOneToManyRelationships(ModelBuilder modelBuilder)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.

            modelBuilder.Entity<HouseDbo>()
                .HasMany(h => h.Issues)
                .WithOne(i => i.House);

            modelBuilder.Entity<ApplicationUserDbo>()
                .HasMany(a => a.Houses)
                .WithOne(h => h.Landlord);

            modelBuilder.Entity<ApplicationUserDbo>()
                .HasOne(a => a.House)
                .WithMany(h => h.Tenants)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ApplicationUserDbo>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.Author)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<IssueDbo>()
                .HasOne(i => i.Author)
                .WithMany(a => a.Issues)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<IssueDbo>()
                .HasMany(i => i.Comments)
                .WithOne(c => c.Issue);

            modelBuilder.Entity<ContactDbo>()
                .HasOne(c => c.House)
                .WithMany(h => h.Contacts);

#pragma warning restore CS8602 // Dereference of a possibly null reference.
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