using MotorMax.Entidades.Dto.DetallesVenta;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Servicios.Interfaces
{
    public interface IServiciosDetallesVenta
    {
        void Agregar(DetalleVenta detalle);
        void Borrar(int id);
        void Editar(DetalleVenta detalle);
        bool EstaRelacionado(DetalleVenta detalle);
        bool Existe(DetalleVenta detalle);
        DetalleVenta GetDetalleVentaPorId(int id);
        List<DetalleVentaListDto> GetDetalleVentas(int ventaId);
    }
}
