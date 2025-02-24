using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementAPI.Models;

public partial class TaskManagementDbContext : DbContext
{
    public TaskManagementDbContext()
    {
    }

    public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskLabel> TaskLabels { get; set; }

    public virtual DbSet<TaskComment> TaskComments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PC1;Database=TaskManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskLabel>()
        .HasKey(tl => new { tl.TaskId, tl.LabelId });

        modelBuilder.Entity<TaskLabel>()
            .HasOne(tl => tl.Task)
            .WithMany(t => t.TaskLabels)
            .HasForeignKey(tl => tl.TaskId);

        modelBuilder.Entity<TaskLabel>()
            .HasOne(tl => tl.Label)
            .WithMany(l => l.TaskLabels)
            .HasForeignKey(tl => tl.LabelId);
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07EBA720C5");

            entity.HasIndex(e => e.Name, "UQ__Categori__737584F6E9FBFD16").IsUnique();

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Label>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Labels__3214EC07FFA84EC8");

            entity.HasIndex(e => e.Name, "UQ__Labels__737584F6A734B241").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tasks__3214EC0779C705B7");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsCompleted).HasDefaultValue(false);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Category).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Tasks__CategoryI__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Tasks__UserId__403A8C7D");

            //entity.HasMany(d => d.Labels).WithMany(p => p.Tasks)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "TaskLabel",
            //        r => r.HasOne<Label>().WithMany()
            //            .HasForeignKey("LabelId")
            //            .HasConstraintName("FK__TaskLabel__Label__4CA06362"),
            //        l => l.HasOne<Task>().WithMany()
            //            .HasForeignKey("TaskId")
            //            .HasConstraintName("FK__TaskLabel__TaskI__4BAC3F29"),
            //        j =>
            //        {
            //            j.HasKey("TaskId", "LabelId").HasName("PK__TaskLabe__5FFEAB0DB3CEC2F8");
            //            j.ToTable("TaskLabels");
            //        });
        });

        modelBuilder.Entity<TaskComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaskComm__3214EC078F41DC33");

            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskComments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TaskComme__TaskI__44FF419A");

            entity.HasOne(d => d.User).WithMany(p => p.TaskComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TaskComme__UserI__45F365D3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0759683B82");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E43BBB30CE").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053492A3ABD7").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
