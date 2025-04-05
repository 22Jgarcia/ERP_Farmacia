using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorio1.Models
{
    public class Lote
    {
        [Key]
        public Guid LoteId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public virtual required Producto Producto { get; set; }

        [Required(ErrorMessage = "El número de lote es obligatorio")]
        [StringLength(100, ErrorMessage = "El número de lote no puede tener más de 100 caracteres")]
        [DisplayName("Número de Lote")]
        public string NumeroLote { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria")]
        [DisplayName("Fecha de Vencimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaVencimientoLote { get; set; }

        [Required(ErrorMessage = "El estado del lote es obligatorio")]
        [StringLength(50)]
        [DisplayName("Estado del Lote")]
        public string EstadoLote { get; set; }

        [ScaffoldColumn(false)]
        public bool LoteEliminado { get; set; }
    }
}