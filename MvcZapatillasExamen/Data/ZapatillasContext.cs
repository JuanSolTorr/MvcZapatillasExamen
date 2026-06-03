using Microsoft.EntityFrameworkCore;

namespace MvcZapatillasExamen.Data
{
    public class ZapatillasContext: DbContext
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options) : base(options){}
        public DbSet<Models.Zapatilla> Zapatillas { get; set; }
    }
}
