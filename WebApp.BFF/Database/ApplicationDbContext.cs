using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebApp.BFF.Core.Models;

namespace WebApp.BFF.Database
{
    public class ChatiusContext : IdentityDbContext<ApplicationUser>
    {
        public ChatiusContext(DbContextOptions<ChatiusContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplcationUsers { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Connection>()
                .Property(c => c.Id)
                .ValueGeneratedNever();

            builder.Entity<Connection>()
                .HasKey(c => new { c.Id, c.ApplicationUserId });

            builder.Entity<Connection>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(u => u.Connections)
                .HasForeignKey(c => c.ApplicationUserId);

            builder.Entity<Message>()
                .HasOne(c => c.Group)
                .WithMany(g => g.Messages)
                .HasForeignKey(m => m.GroupId);

            builder.Entity<Message>()
                .HasOne(c => c.Author)
                .WithMany(g => g.Messages)
                .HasForeignKey(m => m.AuthorId);            
        }
    }
}
