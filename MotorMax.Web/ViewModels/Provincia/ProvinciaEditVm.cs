using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Provincia
{
    public class ProvinciaEditVm
    {
        public int ProvinciaId { get; set; }

        [DisplayName("Provincia")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Nombre { get; set; }
        public byte[] RowVersion { get; set; }
    }
}