using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTaskList.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApiTaskList.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListModel>().HasMany(m => m.TaskModels).WithOne(n => n.ListModel).OnDelete(DeleteBehavior.Cascade).IsRequired();
            // modelBuilder.Entity<TaskModel>().HasOne(m => m.ListModel).WithMany(n => n.TaskModels).HasForeignKey(k => k.ListModelId);

            // modelBuilder.Entity<ListModel>().HasMany(m => m.TaskModels).WithOne();
            // modelBuilder.Entity<ListModel>().Navigation(m => m.TaskModels).UsePropertyAccessMode(PropertyAccessMode.Property);
            // modelBuilder.Entity<TaskModel>().HasOne(m => m.ListModel).WithMany(n => n.TaskModels).HasForeignKey(k => k.ListModelId);
        }

        public DbSet<TaskModel> Tasks { get; set; }

        public DbSet<ListModel> Lists { get; set; }
    }
}
