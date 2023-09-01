using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace MotorMax.Web.ViewModels.Repuesto
{
    public class RepuestoEditVm
    {
        public int RepuestoId { get; set; }

        [DisplayName("Repuesto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(250, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Descripcion { get; set; }

        [DisplayName("Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría")]

        public int CategoriaId { get; set; }


        [DisplayName("Precio unitario")]
        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.10, 500000, ErrorMessage = "Favor de ingresar un {0} entre {1} y{2}")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal PrecioUnitario { get; set; }


        public int Stock { get; set; }
        public bool Suspendido { get; set; } = false;

        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
        [DisplayName("Imagen")]
        public HttpPostedFileBase imagenFile { get; set; }
        public byte[] RowVersion { get; set; }
        public List<SelectListItem> Categorias { get; set; }
    }
}