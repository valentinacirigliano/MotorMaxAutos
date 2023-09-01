using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace MotorMax.Web.ViewModels.Auto
{
    public class AutoEditVm
    {
        public int AutoId { get; set; }

        [DisplayName("Patente")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(7, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 6)]
        public string Patente { get; set; }

        [DisplayName("Modelo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Modelo { get; set; }
        [DisplayName("Marca")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una marca")]
        public int MarcaId { get; set; }
        [DisplayName("Precio Unit.")]
        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.10, 10000, ErrorMessage = "Favor de ingresar un {0} entre {1} y{2}")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal PrecioFinal { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Stock mal ingresado")]
        public int Stock { get; set; }

        public bool Suspendido { get; set; }

        public byte[] RowVersion { get; set; }

        public List<SelectListItem> Marcas { get; set; }

    }
}