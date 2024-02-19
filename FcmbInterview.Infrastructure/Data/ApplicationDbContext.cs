
using FcmbInterview.Domain.Users;
using FcmbInterview.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using FcmbInterview.Infrastructure.Data.Configurations;

namespace FcmbInterview.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
      
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
      
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Discount> Discount { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityConfiguration).Assembly);
        }
    }
}
