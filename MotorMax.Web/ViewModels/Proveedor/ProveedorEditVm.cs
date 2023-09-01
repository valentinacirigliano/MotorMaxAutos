using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMax.Entidades.Entidades;

namespace MotorMax.Web.ViewModels.Proveedor
{
    public class ProveedorEditVm
    {
        public int ProveedorId { get; set; }
        [DisplayName("Proveedor")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string NombreProveedor { get; set; }
        [DisplayName("Dirección")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]

        public string Direccion { get; set; }
        [DisplayName("Ciudad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un país")]

        public int CiudadId { get; set; }

        [DisplayName("Teléfono")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Telefono { get; set; }
        public byte[] RowVersion { get; set; }

        public List<SelectListItem> Ciudades { get; set; }
    }
}