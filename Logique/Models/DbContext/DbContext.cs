using Microsoft.EntityFrameworkCore;
using Logique.Models.Entities;

namespace TeleCentre.Web.Portal.Models
{
    public class LogiqueDBContext : DbContext
    {
        public LogiqueDBContext(DbContextOptions<LogiqueDBContext> options)
            : base(options)
        {
        }

        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });

        }
    }

}
