using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShallowLibApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
                
        }

        public virtual DbSet<Library> Librarys { get; set; }
        public virtual DbSet<AuthorsItem> Authors { get; set; }

    }
}
