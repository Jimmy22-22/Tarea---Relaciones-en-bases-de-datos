using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UspgPOS.Models
{
    public class Producto
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("nombre")]
        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Column("marca_id")]
        public long? MarcaId { get; set; }
        public Marca? Marca { get; set; }

        [Column("clasificacion_id")]
        public long? ClasificacionId { get; set; }        
        public Clasificacion Clasificacion { get; set; }

        [Column("precio")]
        [Range(0, 1000000)]
        public decimal Precio { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }
    }
}
