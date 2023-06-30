using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Model;
#pragma warning disable
public partial class CrowdfyContext : DbContext
{
    public CrowdfyContext()
    {
    }

    public CrowdfyContext(DbContextOptions<CrowdfyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Forum> Forums { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserXlike> UserXlikes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CT-C-0013K\\SQLEXPRESS01;Initial Catalog=crowdfy;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__forums__3214EC0767ECD5DB");

            entity.ToTable("forums");

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

            entity.HasOne(d => d.CreatorNavigation).WithMany(p => p.Forums)
                .HasForeignKey(d => d.Creator)
                .HasConstraintName("FK__forums__Creator__398D8EEE");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__posts__3214EC0708F440B9");

            entity.ToTable("posts");

            entity.Property(e => e.Anex).IsUnicode(false);
            entity.Property(e => e.Content)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(90)
                .IsUnicode(false);

            entity.HasOne(d => d.IdForumNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdForum)
                .HasConstraintName("FK__posts__IdForum__4BAC3F29");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.InverseIdPostNavigation)
                .HasForeignKey(d => d.IdPost)
                .HasConstraintName("FK__posts__IdPost__4316F928");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3214EC0742784FE9");

            entity.ToTable("roles");

            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.HasOne(d => d.IdForumNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.IdForum)
                .HasConstraintName("FK__roles__IdForum__46E78A0C");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__roles__IdUser__45F365D3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3214EC0747AA57DD");

            entity.ToTable("users");

            entity.Property(e => e.BornDate).HasColumnType("datetime");
            entity.Property(e => e.Completename)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HashCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsAuth)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isAuth");
            entity.Property(e => e.Mail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Photo).IsUnicode(false);
            entity.Property(e => e.Salt)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.IdForums).WithMany(p => p.IdUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserXforum",
                    r => r.HasOne<Forum>().WithMany()
                        .HasForeignKey("IdForum")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__userXforu__IdFor__3E52440B"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__userXforu__IdUse__3D5E1FD2"),
                    j =>
                    {
                        j.HasKey("IdUser", "IdForum").HasName("PK__userXfor__A8B315B36902D80C");
                        j.ToTable("userXforum");
                    });
        });

        modelBuilder.Entity<UserXlike>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__userXlik__3214EC07335BF379");

            entity.ToTable("userXlikes");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.UserXlikes)
                .HasForeignKey(d => d.IdPost)
                .HasConstraintName("FK__userXlike__IdPos__4AB81AF0");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserXlikes)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__userXlike__IdUse__49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
