using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Provincia
{
    public class ProvinciaListVm
    {
        public int ProvinciaId { get; set; }

        [DisplayName("Provincia")]
        public string Nombre { get; set; }
        [DisplayName("Cant. Ciudades")]
        public int CantidadCiudades { get; set; }
    }
}