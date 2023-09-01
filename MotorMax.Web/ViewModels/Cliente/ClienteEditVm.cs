using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorMax.Web.ViewModels.Cliente
{
    public class ClienteEditVm
    {
        public int ClienteId { get; set; }


        [DisplayName("Cliente")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string NombreApellido { get; set; }


        [DisplayName("Direccion")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Direccion { get; set; }

        [DisplayName("Ciudad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una ciudad")]
        public int CiudadId { get; set; }


        [Required]
        [StringLength(1)]
        public string Sexo { get; set; }


        [DisplayName("Teléfono")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Telefono { get; set; }

        [MaxLength(256, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public byte[] RowVersion { get; set; }

        public List<SelectListItem> Ciudades { get; set; }

    }
}