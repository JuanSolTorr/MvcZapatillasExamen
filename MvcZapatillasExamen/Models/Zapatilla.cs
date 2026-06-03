using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcZapatillasExamen.Models
{
    [Table("ZAPATILLAS")]
    public class Zapatilla
    {
        [Key]
        [Column("IDPRODUCTO")]
        public int IdZapatilla { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
        [Column("IMAGEN")]
        public string ImagenUrl { get; set; }
    }
}
