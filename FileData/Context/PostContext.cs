﻿using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FileData.Context;

public class PostContext:DbContext
{
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder.UseSqlite("Data Source = ../FileData/Post.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Post>().HasKey(post => post.Id);
                modelBuilder.Entity<User>().HasKey(user => user.Id);
                modelBuilder.Entity<User>().HasAlternateKey(user => user.UserName);

                modelBuilder.Entity<Post>(entity =>
                {
                        entity.HasOne(post => post.Owner).WithMany(user => user.posts);
                });
                modelBuilder.Entity<User>(entity =>
                {
                        entity.HasMany(user => user.posts).WithOne(post => post.Owner);
                });
        }

}