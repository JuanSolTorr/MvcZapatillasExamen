using MvcZapatillasExamen.Models;
using MvcZapatillasExamen.Repositories;


namespace MvcZapatillasExamen.Services
{
    public class ZapatillasService
    {
        private  RepositoryZapatillas repository;
        private  ServiceStorageS3 serviceStorage;
        private  IConfiguration configuration;

        public ZapatillasService(RepositoryZapatillas repository, ServiceStorageS3 serviceStorage, IConfiguration configuration)
        {
            this.repository = repository;
            this.serviceStorage = serviceStorage;
            this.configuration = configuration;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            return await this.repository.GetZapatillasAsync();
        }

        public async Task CrearZapatillaAsync(Zapatilla zapatilla, IFormFile imagenZapatilla)
        {
            if (imagenZapatilla != null && imagenZapatilla.Length > 0)
            {
                var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagenZapatilla.FileName);

                using (var stream = imagenZapatilla.OpenReadStream())
                {
                    await this.serviceStorage.UploadFileAsync(nombreArchivo, stream);
                }

                string bucketName = this.configuration.GetValue<string>("AWS:BucketName");
                zapatilla.ImagenUrl = $"https://{bucketName}.s3.us-east-2.amazonaws.com/{nombreArchivo}";
            }

            await this.repository.InsertarZapatillaAsync(zapatilla.IdZapatilla, zapatilla.Nombre, zapatilla.Descripcion, zapatilla.ImagenUrl);
        }
    }
}