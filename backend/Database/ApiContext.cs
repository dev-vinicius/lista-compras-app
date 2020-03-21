using System.Collections.Generic;
using APIListaCompras.Models;
using Microsoft.EntityFrameworkCore;

namespace APIListaCompras.Database
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options): base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
        
    }
}