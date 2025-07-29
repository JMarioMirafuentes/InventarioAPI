using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Data
{
    public class IventarioDbContext :DbContext
    {
        public IventarioDbContext(DbContextOptions<IventarioDbContext> options)
            : base(options)
        {
        }

        public DbSet<AlimentoBebida> AlimentosBebidas { get; set; } = null!;
    }
}
