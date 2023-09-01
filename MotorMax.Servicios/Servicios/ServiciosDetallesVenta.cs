using MotorMax.Datos.Interfaces;
using MotorMax.Entidades.Dto.DetallesVenta;
using MotorMax.Entidades.Entidades;
using MotorMax.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Servicios.Servicios
{
    public class ServiciosDetallesVenta : IServiciosDetallesVenta
    {
        private readonly IRepositorioDetalleVentas _repositorio;
        public ServiciosDetallesVenta(IRepositorioDetalleVentas repositorio)
        {
            _repositorio = repositorio;
        }

        public void Agregar(DetalleVenta detalle)
        {
            throw new NotImplementedException();
        }

        public void Borrar(int id)
        {
            throw new NotImplementedException();
        }

        public void Editar(DetalleVenta detalle)
        {
            throw new NotImplementedException();
        }

        public bool EstaRelacionado(DetalleVenta detalle)
        {
            throw new NotImplementedException();
        }

        public bool Existe(DetalleVenta detalle)
        {
            throw new NotImplementedException();
        }

        public List<DetalleVentaListDto> GetDetallesPorVenta(int id)
        {
            return _repositorio.GetDetalleVentas(id);
        }

        public DetalleVenta GetDetalleVentaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<DetalleVentaListDto> GetDetalleVentas(int ventaId)
        {
            throw new NotImplementedException();
        }
    }
}
