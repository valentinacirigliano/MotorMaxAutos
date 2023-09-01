using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Entidades.Dto.Proveedor
{
    public class ProveedorListDto
    {
        public int ProveedorId { get; set; }
        public string NombreProveedor { get; set; }
        public string Ciudad { get; set; }
        public int CantidadRepuestos { get; set; }
    }
}
