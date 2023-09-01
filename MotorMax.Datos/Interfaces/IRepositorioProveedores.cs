using MotorMax.Entidades.Dto.Proveedor;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorMax.Datos.Interfaces
{
    public interface IRepositorioProveedores
    {
        void Agregar(Proveedor proveedor);
        void Borrar(int id);
        void Editar(Proveedor proveedor);
        bool EstaRelacionado(Proveedor proveedor);
        bool Existe(Proveedor proveedor);
        Proveedor GetProveedorPorId(int id);
        List<ProveedorListDto> GetProveedores();
        List<ProveedorListDto> GetProveedores(int ciudadId);
        List<ProveedorListDto> Filtrar(Func<Proveedor, bool> predicado);
        List<SelectListItem> GetProveedoresDropDownList();
    }
}
