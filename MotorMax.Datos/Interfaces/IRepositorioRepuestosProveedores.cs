using MotorMax.Entidades.Dto.RepuestoProveedor;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.Interfaces
{
    public interface IRepositorioRepuestosProveedores
    {
        void Borrar(int repuestoId, int proveedorId);
        bool Existe(RepuestoProveedor repuestoProveedor);
        List<RepuestoProveedorListDto> GetRepuestosProveedores();
        void Guardar(RepuestoProveedor repuestoProveedor);
        List<RepuestoProveedorListDto> Filtrar(Func<RepuestoProveedor, bool> predicado);
        int GetCantidad();
        List<RepuestoProveedor> GetListaPorProveedor(int proveedorId);
        List<RepuestoProveedor> GetListaPorRepuesto(int repuestoId);
    }
}
