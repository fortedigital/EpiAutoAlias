﻿using Forte.EpiserverRedirects.EntityFramework.Model;
using Microsoft.EntityFrameworkCore;


namespace Forte.EpiserverRedirects.EntityFramework
{
    public abstract class RedirectRulesDbContext : DbContext, IRedirectRulesDbContext
    {
        protected RedirectRulesDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<RedirectRuleEntity> RedirectRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RedirectRuleEntity>()
                .HasKey(rule => rule.RuleId);

            modelBuilder.Entity<RedirectRuleEntity>()
                .Property(rule => rule.RuleId)
                .IsRequired();

            modelBuilder.Entity<RedirectRuleEntity>()
                .HasIndex(rule => rule.RuleId)
                .IsUnique();
        }
    }
}
