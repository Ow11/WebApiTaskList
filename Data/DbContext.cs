using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NMTask.Models;
using NMList.Models;

namespace asp_rest.Data
{
    public class LocalDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public LocalDbContext (DbContextOptions<LocalDbContext> options)
            : base(options)
        {
        }

        public DbSet<NMTask.Models.Task> Task { get; set; }
        public DbSet<NMList.Models.ListDetailed> ListDetailed { get; set; }
    }
}