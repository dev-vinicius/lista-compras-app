using ListaCompras.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaCompras.Server.Database
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Item> Itens { get; set; }
    }
}
