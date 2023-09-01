using MotorMax.Entidades.Dto.Ciudad;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorMax.Servicios.Interfaces
{
    public interface IServiciosCiudades
    {
        List<CiudadListDto> GetCiudades();
        bool Existe(Ciudad ciudad);
        void Guardar(Ciudad ciudad);
        bool EstaRelacionada(Ciudad ciudad);
        Ciudad GetCiudadPorId(int ciudadId);
        void Borrar(int ciudadId);
        List<CiudadListDto> GetCiudades(int provinciaId);
        List<CiudadListDto> Filtrar(Func<Ciudad, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<CiudadListDto> GetCiudadesPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Ciudad, bool> predicado);
        List<SelectListItem> GetCiudadesDropDownList(int provinciaId);
        List<SelectListItem> GetCiudadesDropDownList();


    }
}
