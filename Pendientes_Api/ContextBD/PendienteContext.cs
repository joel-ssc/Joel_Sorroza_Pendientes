using Microsoft.EntityFrameworkCore;
using Pendientes_Api.Models;

namespace Pendientes_Api.ContextBD
{
    public class PendienteContext : DbContext
    {
        public PendienteContext(DbContextOptions<PendienteContext> options) : base(options)
        {

        }

        public DbSet<LoginModel> LoginModels { get; set; }

        public DbSet<Pendientes> Pendientes { get; set; }
    }
}
