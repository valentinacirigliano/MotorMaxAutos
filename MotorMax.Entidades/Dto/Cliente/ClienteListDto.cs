using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Entidades.Dto.Cliente
{
    public class ClienteListDto
    {
        public int ClienteId { get; set; }
        public string NombreApellido { get; set; }
        public string Ciudad { get; set; }
    }
}
