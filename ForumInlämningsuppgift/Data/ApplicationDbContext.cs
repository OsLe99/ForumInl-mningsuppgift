﻿using ForumInlämningsuppgift.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForumInlämningsuppgift.Data;

public class ApplicationDbContext : IdentityDbContext<ForumUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Models.Category> Category { get; set; }
    public DbSet<Models.SubCategory> SubCategory { get; set; }
    public DbSet<Models.Post> Posts { get; set; }
    public DbSet<Models.Reply> Replies { get; set; }
    public DbSet<Models.Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Reply>()
            .HasOne(r => r.Post)
            .WithMany(p => p.Replies)
            .HasForeignKey(r => r.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Message>()
            .HasOne(m => m.Recipient)
            .WithMany()
            .HasForeignKey(m => m.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
