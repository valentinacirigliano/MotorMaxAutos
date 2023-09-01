using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Entidades.Entidades
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
