using Microsoft.EntityFrameworkCore;
using RIAE3._1.Models;

namespace RIAE3._1.Context
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Registros> Registros { get; set; }
        public DbSet<Boletas> Boletas { get; set; }
        //public DbSet<Models.Request.RegistrosRequest> RegistrosRequests { get; set; }
    }
}
