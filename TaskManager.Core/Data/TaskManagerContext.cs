using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Models;

namespace TaskManager.Core.Data;

public partial class TaskManagerContext : DbContext
{
    public TaskManagerContext()
    {
    }

    public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<MTask> Tasks { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress;Initial Catalog=TaskManager;Integrated Security=True;trustservercertificate=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Comment_pk");

            entity.ToTable("Comment");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.PreviousCommentId).HasColumnName("Previous_Comment_Id");
            entity.Property(e => e.TextContent)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("Text_Content");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.PreviousComment).WithMany(p => p.InversePreviousComment)
                .HasForeignKey(d => d.PreviousCommentId)
                .HasConstraintName("Comment_Comment_Id_fk");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Comment_User_Id_fk");
        });

        modelBuilder.Entity<MTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Task_pk");

            entity.ToTable("Task");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.TextContent)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Text_Content");
            entity.Property(e => e.Title)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.TopicId).HasColumnName("Topic_Id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Topic).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TopicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_Topic_Id_fk");

            entity.HasOne(d => d.User).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_User_Id_fk");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Topic_pk");

            entity.ToTable("Topic");

            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pk");

            entity.ToTable("User");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
