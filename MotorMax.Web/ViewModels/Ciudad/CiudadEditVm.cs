using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorMax.Web.ViewModels.Ciudad
{
    public class CiudadEditVm
    {
        public int CiudadId {get;set;}
        [DisplayName("Ciudad")]
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Nombre { get;set;}
        [DisplayName("Provincia")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una provincia")]
        public int ProvinciaId { get;set;}
        public int ProvinciaAnteriorId { get; set; }
        public byte[] RowVersion { get;set;}
        public List<SelectListItem> Provincias { get;set;}

    }
}