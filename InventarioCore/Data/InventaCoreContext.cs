using Microsoft.EntityFrameworkCore;
using InventaCore.Models;

namespace InventaCore.Data
{
    public class InventaCoreContext : DbContext
    {
        public InventaCoreContext(DbContextOptions<InventaCoreContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}