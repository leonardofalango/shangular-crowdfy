using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Model;

public partial class CrowdfyContext : DbContext
{
    #pragma warning disable
    public CrowdfyContext()
    {
    }

    public CrowdfyContext(DbContextOptions<CrowdfyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Forum> Forums { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CT-C-0013K\\SQLEXPRESS01;Initial Catalog=crowdfy;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__forum__3214EC079D1A06B7");

            entity.ToTable("forum");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Photo).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__posts__3214EC071E5EFAAF");

            entity.ToTable("posts");

            entity.Property(e => e.Comments).HasDefaultValueSql("((0))");
            entity.Property(e => e.Content)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Crowds).HasDefaultValueSql("((0))");
            entity.Property(e => e.Title)
                .HasMaxLength(90)
                .IsUnicode(false);

            entity.HasOne(d => d.IdForumNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdForum)
                .HasConstraintName("FK__posts__IdForum__47DBAE45");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.InverseIdPostNavigation)
                .HasForeignKey(d => d.IdPost)
                .HasConstraintName("FK__posts__IdPost__37A5467C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3214EC074B96BD14");

            entity.ToTable("users");

            entity.Property(e => e.BornDate).HasColumnType("datetime");
            entity.Property(e => e.Completename)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsAuth)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isAuth");
            entity.Property(e => e.Mail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Photo).IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.IdForums).WithMany(p => p.IdUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserXforum",
                    r => r.HasOne<Forum>().WithMany()
                        .HasForeignKey("IdForum")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__userXforu__IdFor__2A4B4B5E"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__userXforu__IdUse__29572725"),
                    j =>
                    {
                        j.HasKey("IdUser", "IdForum").HasName("PK__userXfor__A8B315B394F0E6DB");
                        j.ToTable("userXforum");
                    });

            entity.HasMany(d => d.IdPosts).WithMany(p => p.IdUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserXlike",
                    r => r.HasOne<Post>().WithMany()
                        .HasForeignKey("IdPost")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__userXlike__IdPos__5DCAEF64"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__userXlike__IdUse__5CD6CB2B"),
                    j =>
                    {
                        j.HasKey("IdUser", "IdPost").HasName("PK__userXlik__7844EDEC4E70EE72");
                        j.ToTable("userXlike");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
