
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Laboratorio1.Models
{
    public class CategoriaProducto
    {


        [Key]
        public Guid CategoriaProductoId { get; set; } = Guid.NewGuid();


        [Required(ErrorMessage = "El nombre de categoria es obligatorio")]
        [StringLength(255, ErrorMessage = "El nombre no puede tener más de 255 caracteres")]
        [DisplayName("Nombre de la Categoría")]
        public string NombreCategoriaProducto { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres")]
        [DisplayName("Descripción de la Categoría")]
        public string DescripcionCategoriaProducto { get; set; }


        [Required(ErrorMessage = "El estado de la categoría es obligatorio")]
        [StringLength(20)]
        [DisplayName("Estado de la Categoria")]
        public string EstadoCategoriaProducto { get; set; }


        [ScaffoldColumn(false)]
        public bool CategoriaProductoEliminada { get; set; }
    }
}






