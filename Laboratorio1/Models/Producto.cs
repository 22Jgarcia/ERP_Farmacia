using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Laboratorio1.Models
{
    public class Producto
    {
        [Key]
        public Guid ProductoId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(250, ErrorMessage = "El nombre no puede tener más de 250 caracteres")]
        [DisplayName("Nombre del Producto")]
        public string NombreProducto { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres")]
        [DisplayName("Descripción del Producto")]
        public string? DescripcionProducto { get; set; }

        [Required(ErrorMessage = "La unidad de medida es obligatoria")]
        [StringLength(50, ErrorMessage = "La unidad de medida no puede tener más de 50 caracteres")]
        [DisplayName("Unidad de Medida")]
        public string UnidadMedidaProducto { get; set; }

        // Relación con tabla CategoriaProducto
        [Required]
        public Guid CategoriaProductoId { get; set; }
        [ForeignKey("CategoriaProductoId")]
        public virtual required CategoriaProducto CategoriaProducto { get; set; }

        // Relación con tabla Proveedor
        [Required]
        public Guid ProveedorId { get; set; }
        [ForeignKey("ProveedorId")]
        public virtual required Proveedor Proveedor { get; set; }

        [Required(ErrorMessage = "El precio de compra es obligatorio")]
        [Precision(10, 2)]
        [DisplayName("Precio de Compra")]
        public decimal PrecioCompra { get; set; }

        [Required(ErrorMessage = "El precio de venta es obligatorio")]
        [Precision(10, 2)]
        [DisplayName("Precio de Venta")]
        public decimal PrecioVenta { get; set; }

        [Required(ErrorMessage = "El stock mínimo es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo debe ser un número positivo")]
        [DisplayName("Stock Mínimo")]
        public int StockMinimoProducto { get; set; }

        [Required(ErrorMessage = "El stock máximo es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock máximo debe ser un número positivo")]
        [DisplayName("Stock Máximo")]
        public int StockMaximoProducto { get; set; }

        [Required(ErrorMessage = "El estado del producto es obligatorio")]
        [StringLength(50)]
        [DisplayName("Estado del Producto")]
        public string EstadoProducto { get; set; }

        [ScaffoldColumn(false)]
        public bool ProductoEliminado { get; set; }
    }
}
