using Backend_Develin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Task = Backend_Develin.Models.Task;

namespace Backend_Develin.Data
{
    public class ProjectContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
        public DbSet<Comment> Comments { get; set; }


        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Project>().Property(r => r.Name).IsRequired().HasMaxLength(50);

            builder.Entity<ProjectEmployee>()
            .HasKey(t => new { t.ProjectId, t.EmployeeId });

            builder.Entity<ProjectEmployee>()
                .HasOne(pt => pt.Project)
                .WithMany(p => p.Employees)
                .HasForeignKey(pt => pt.ProjectId);

            builder.Entity<ProjectEmployee>()
                .HasOne(pt => pt.Employee)
                .WithMany(t => t.Projects)
                .HasForeignKey(pt => pt.EmployeeId);

            builder.Entity<Task>().HasMany(t => t.Comments);

        }


    }
}
