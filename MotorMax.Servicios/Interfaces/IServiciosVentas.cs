using MotorMax.Entidades.Dto.DetallesVenta;
using MotorMax.Entidades.Dto.Venta;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Servicios.Interfaces
{
    public interface IServiciosVentas
    {
        int GetCantidad();
        List<DetalleVentaListDto> GetDetalleVenta(int ventaId);
        List<VentaListDto> GetVentas();
        Venta GetVentasPorId(int value);
        //void Guardar(List<ItemCarrito> Carrito);
        VentaListDto GetVentaListDtoPorId(int id);
        int Guardar(Venta venta, string name);
    }
}
