using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class TestRwaContext : DbContext
{
    public TestRwaContext()
    {
    }

    public TestRwaContext(DbContextOptions<TestRwaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Performer> Performers { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<SongGenre> SongGenres { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserReview> UserReviews { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:AppConnStr");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<Performer>(entity =>
        {
            entity.ToTable("Performer");

            entity.Property(e => e.FirstName).HasMaxLength(256);
            entity.Property(e => e.LastName).HasMaxLength(256);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Review");

            entity.Property(e => e.Comment).HasMaxLength(256);
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.ToTable("Song");

            entity.Property(e => e.Language).HasMaxLength(256);
            entity.Property(e => e.Melody).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Tempo).HasMaxLength(256);

            entity.HasOne(d => d.Performer).WithMany(p => p.Songs)
                .HasForeignKey(d => d.PerformerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Song_Performer");
        });

        modelBuilder.Entity<SongGenre>(entity =>
        {
            entity.ToTable("SongGenre");

            entity.HasOne(d => d.Genre).WithMany(p => p.SongGenres)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SongGenre_Genre");

            entity.HasOne(d => d.Song).WithMany(p => p.SongGenres)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SongGenre_Song");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(256);
            entity.Property(e => e.LastName).HasMaxLength(256);
            entity.Property(e => e.Phone).HasMaxLength(256);
            entity.Property(e => e.PwdHash).HasMaxLength(256);
            entity.Property(e => e.PwdSalt).HasMaxLength(256);
            entity.Property(e => e.SecurityToken).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserReview>(entity =>
        {
            entity.ToTable("UserReview");

            entity.HasOne(d => d.Review).WithMany(p => p.UserReviews)
                .HasForeignKey(d => d.ReviewId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserReview_Review");

            entity.HasOne(d => d.User).WithMany(p => p.UserReviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserReview_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
