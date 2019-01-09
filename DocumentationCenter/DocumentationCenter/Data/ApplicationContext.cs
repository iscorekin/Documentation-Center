using DocumentationCenter.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentationCenter.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
