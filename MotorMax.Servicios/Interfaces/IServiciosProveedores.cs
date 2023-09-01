using MotorMax.Entidades.Dto.Proveedor;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorMax.Servicios.Interfaces
{
    public interface IServiciosProveedores
    {
        void Guardar(Proveedor proveedor);
        void Borrar(int id);
        bool EstaRelacionado(Proveedor proveedor);
        bool Existe(Proveedor proveedor);
        Proveedor GetProveedorPorId(int id);
        List<ProveedorListDto> GetProveedores();
        List<ProveedorListDto> Filtrar(Func<Proveedor, bool> predicado);
        List<SelectListItem> GetProveedoresDropDownList();
    }
}
