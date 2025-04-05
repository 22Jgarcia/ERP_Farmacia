
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Laboratorio1.Models
{
    public class Proveedor


    {

        [Key]
        public Guid ProveedorId { get; set; } = Guid.NewGuid();


        [Required(ErrorMessage = "El nombre de proveedor es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 200 caracteres")]
        [DisplayName("Nombre del Proveedor")]
        public string NombreProveedor { get; set; }


        [Required(ErrorMessage = "El nit de proveedor es obligatorio")]
        [StringLength(50, ErrorMessage = "El nit no puede tener mas de 50 caracteres")]
        [DisplayName("NIT del Proveedor")]
        public string NitProveedor { get; set; }


        [Required(ErrorMessage = "La dirección del proveedor es obligatoria")]
        [StringLength(200, ErrorMessage = "La dirección no puede tener mas de 200 caracteres")]
        [DisplayName("Dirección del Proveedor")]
        public string DireccionProveedor { get; set; }

        [Required(ErrorMessage = "El telefono del proveedor es obligatorio")]
        [StringLength(20, ErrorMessage = "El número de telefono no puede tener más de 20 digitos")]
        [DisplayName("Telefono del Proveedor")]
        public string TelefonoProveedor { get; set; }

        [Required(ErrorMessage = "El estado del proveedor es obligatorio")]
        [StringLength(20)]
        [DisplayName("Estado del Proveedor")]
        public string EstadoProveedor { get; set; }


        [ScaffoldColumn(false)]
        public bool ProveedorEliminado { get; set; }


    }
}
