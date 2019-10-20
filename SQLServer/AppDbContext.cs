using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer
{
    public class AppDbContext : IdentityDbContext
    {
        //  Properties
        //  ==========

        public DbSet<Issue> Issues { get; set; } = null!;

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

            modelBuilder.Entity<Issue>().HasData(
                new Issue
                {
                    Id = 1,
                    Content = "I am #1"
                },
                new Issue
                {
                    Id = 2,
                    Content = "I am #2"
                });
        }
    }
}