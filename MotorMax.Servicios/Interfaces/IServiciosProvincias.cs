using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorMax.Servicios.Interfaces
{
    public interface IServiciosProvincias
    {
        List<Provincia> GetProvincias();
        void Guardar(Provincia provincia);
        void Borrar(int id);
        bool Existe(Provincia provincia);
        Provincia GetProvinciaPorId(int provinciaId);
        bool EstaRelacionado(Provincia provincia);
        List<Provincia> GetProvinciasPorPagina(int cantidad, int pagina);
        int GetCantidad();
        List<SelectListItem> GetProvinciasDropDownList();
    }


}
