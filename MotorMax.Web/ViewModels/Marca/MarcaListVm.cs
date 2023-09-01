using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace MotorMax.Web.ViewModels.Marca
{
    public class MarcaListVm
    {
        public int MarcaId { get; set; }
        [DisplayName("Marca")]
        public string NombreMarca { get; set; }
    }
}