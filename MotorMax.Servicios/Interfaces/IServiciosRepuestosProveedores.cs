using MotorMax.Entidades.Dto.RepuestoProveedor;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Servicios.Interfaces
{
    public interface IServiciosRepuestosProveedores
    {
        void Borrar(int repuestoId, int proveedorId);
        bool Existe(RepuestoProveedor repuestoProveedor);
        List<RepuestoProveedorListDto> GetRepuestosProveedores();
        void Guardar(RepuestoProveedor repuestoProveedor);
        int GetCantidad();
        List<RepuestoProveedor> GetListaPorProveedor(int proveedorId);
        List<Repuesto> GetRepuestosPorProveedor(int proveedorId);
        List<Proveedor> GetProveedoresPorRepuesto(int repuestoId);
    }
}
