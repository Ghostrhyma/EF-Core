using EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Core.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Course> Cources => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=school.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(s => s.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(s => s.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(s => s.Age)
                    .IsRequired();

            });

        }

    }
}
