using MotorMax.Web.ViewModels.Ciudad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Provincia
{
    public class ProvinciaDetailVm
    {
        public ProvinciaListVm Provincia { get; set; }
        public List<CiudadListVm> Ciudades { get; set; }
    }
}