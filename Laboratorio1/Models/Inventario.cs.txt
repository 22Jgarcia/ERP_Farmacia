using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorio1.Models
{
    public class Inventario
    {
        [Key]
        public Guid InventarioId { get; set; } = Guid.NewGuid();

        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "La cantidad es Obligatoria")]

        public Guid ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public virtual required Producto Producto { get; set; }

        public Guid LoteId { get; set; }
        [ForeignKey("LoteId")]
        public virtual required Lote Lote { get; set; }

        [ScaffoldColumn(false)]
        public bool InventarioEliminado { get; set; }
    }
}
