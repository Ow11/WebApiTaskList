using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using asp_rest.Data;
using System;
using System.Linq;
using NMTask.Models;

namespace asp_rest.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LocalDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<LocalDbContext>>()
            ))
            {
                // Looks for any tasks
                if (context.Task.Any())
                {
                    return; // Will not seed any films, whether DB's already seeded
                }

                context.Task.AddRange(
                    new Task
                    {
                        Id = 1,
                        Name = "The Godfather",
                        Description = "Watch this great movie again with my friend!",
                        CreatedAt = "2021-07-29T21:18:59.447Z"
                    },
                    new Task
                    {
                        Id = 2,
                        Name = "12 Angry Men",
                        Description = "A reccomendation from my friend",
                        CreatedAt = "2021-07-29T21:18:59.447Z"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}