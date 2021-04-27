using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ProjectImmediateReply.DBModels
{
    public partial class ProjectImmediateReplyDataContext : DbContext
    {
        public ProjectImmediateReplyDataContext()
            : base("name=MainDBDataContext")
        {
        }

        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Work> Works { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .Property(e => e.ClassNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.WorkID)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Works)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LineID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ClassNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.License)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.WorkID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Privilege)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Grades)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Work>()
                .Property(e => e.FilePath)
                .IsUnicode(false);
        }
    }
}
