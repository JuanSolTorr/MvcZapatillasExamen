using Microsoft.EntityFrameworkCore;
using MvcZapatillasExamen.Data;
using MvcZapatillasExamen.Models;

namespace MvcZapatillasExamen.Repositories
{
    public class RepositoryZapatillas
    {
        private  ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            return await this.context.Zapatillas.ToListAsync();
        }

        public async Task InsertarZapatillaAsync(int id, string nombre, string descripcion, string imagen)
        {
            Zapatilla zapatilla = new Zapatilla
            {
                IdZapatilla = id,
                Nombre = nombre,
                Descripcion = descripcion,
                ImagenUrl = imagen
            };
            this.context.Zapatillas.Add(zapatilla);
            await this.context.SaveChangesAsync();
        }
    }
}