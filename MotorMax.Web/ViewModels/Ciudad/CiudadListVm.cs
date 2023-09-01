using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Ciudad
{
    public class CiudadListVm
    {
        public int CiudadId { get; set; }
        [DisplayName("Ciudad")]
        public string Nombre { get; set; }
        [DisplayName("Provincia")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una provincia")]
        public string NombreProvincia { get; set; }
    }
}