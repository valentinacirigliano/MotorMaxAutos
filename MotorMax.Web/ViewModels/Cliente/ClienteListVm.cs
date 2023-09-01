using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Cliente
{
    public class ClienteListVm
    {
        public int ClienteId { get; set; }
        [DisplayName("Cliente")]
        public string NombreApellido { get; set; }
        public string Ciudad { get; set; }
    }
}