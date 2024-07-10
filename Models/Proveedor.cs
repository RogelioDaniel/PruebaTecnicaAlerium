using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaAlerium.Models
{
    public class Proveedor
    {
        [Key]
        public int idProveedor { get; set; }

        [Required]
        [MaxLength(20)]
        public string Codigo { get; set; }

        [Required]
        [MaxLength(150)]
        public string RazonSocial { get; set; }

        [Required]
        [MaxLength(13)]
        public string RFC { get; set; }

        public ICollection<Producto>? Productos { get; set; }
    }

}
