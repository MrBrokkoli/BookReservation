using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BookReservation.DataAccess.Models;

namespace BookReservation.DataAccess
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Entity>();

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), (type) => !type.IsAbstract);

            base.OnModelCreating(modelBuilder);
        }
    }
}