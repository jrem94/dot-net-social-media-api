using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaAssessment.Models
{
    public class ApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Context> Contexts { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<HashTag> HashTags { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Repost> Reposts { get; set; }
        public DbSet<SimpleTweet> SimpleTweets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=127.0.0.1;port=5432;database=socialmedia;userid=postgres;password=bondstone");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.Joined)
                .HasDefaultValueSql("current_timestamp");

            modelBuilder.Entity<SimpleTweet>()
                .Property(b => b.Posted)
                .HasDefaultValueSql("current_timestamp");

            modelBuilder.Entity<Repost>()
                .Property(b => b.Posted)
                .HasDefaultValueSql("current_timestamp");

            modelBuilder.Entity<Reply>()
                .Property(b => b.Posted)
                .HasDefaultValueSql("current_timestamp");
        }
    }
}