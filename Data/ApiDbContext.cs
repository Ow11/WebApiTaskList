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
        }

        public DbSet<TaskModel> Tasks { get; set; }

        public DbSet<ListModel> Lists { get; set; }
    }
}
