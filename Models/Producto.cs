using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnicaAlerium.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        [Required]
        public int IdProveedor { get; set; }
        [Required]
        [MaxLength(20)]
        public string Codigo { get; set; }
        [Required]
        [MaxLength(150)]
        public string Descripcion { get; set; }
        [Required]
        [MaxLength(3)]
        public string Unidad { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Costo { get; set; }
        [ForeignKey("IdProveedor")]
        public Proveedor? Proveedor { get; set; }
    }

}
