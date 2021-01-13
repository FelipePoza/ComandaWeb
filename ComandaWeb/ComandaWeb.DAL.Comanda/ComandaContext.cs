using ComandaWeb.Model;
using Microsoft.EntityFrameworkCore;

namespace ComandaWeb.DAL.Comanda
{
    public class ComandaContext : DbContext
    {
        public ComandaContext(DbContextOptions<ComandaContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<ComandaWeb.Model.Comanda> Comanda { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ComandaItem> ComandaItem { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
