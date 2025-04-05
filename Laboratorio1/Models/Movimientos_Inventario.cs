using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorio1.Models
{
    public class Movimientos_Inventario
    {
        [Key]
        public Guid MovimientoId { get; set; }

      
        [DisplayName("Tipo de Movimiento")]
        public string TipoMovimiento { get; set; }

        [DisplayName("Cantidad")]
        public double Cantidad { get; set; }

        [DisplayName("Estado Movimiento")]
        public string EstadoMovimiento { get; set; }


        [DisplayName("Fecha de Movimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaMovimiento { get; set; }

        //llave foranea
        [Required]
        public Guid ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public virtual required Producto Producto { get; set; }
        //--

        //llave foranea
        [Required]
        public Guid LoteId { get; set; }
        [ForeignKey("LoteId")]
        public virtual required Lote Lote { get; set; }
        //--

        [ScaffoldColumn(false)]
        public bool MovimientosInvetarioEliminados { get; set; }
    }
}
