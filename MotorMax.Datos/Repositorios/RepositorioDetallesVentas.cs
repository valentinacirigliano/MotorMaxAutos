using MotorMax.Datos.Interfaces;
using MotorMax.Entidades.Dto.DetallesVenta;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace MotorMax.Datos.Repositorios
{
    public class RepositorioDetalleVentas : IRepositorioDetalleVentas
    {
        private readonly AutosDbContext _context;
        public RepositorioDetalleVentas(AutosDbContext context)
        {
            _context = context;
        }
        public void Agregar(DetalleVenta detalle)
        {
            _context.DetalleVenta.Add(detalle);
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

        public DetalleVenta GetDetalleVentaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<DetalleVentaListDto> GetDetalleVentas(int ventaId)
        {
            return _context.DetalleVenta
                .Include(d => d.Repuesto)
                .Where(d => d.VentaId == ventaId)
                .Select(d => new DetalleVentaListDto()
                {
                    DetalleVentaId = d.DetalleVentaId,
                    Repuesto = d.Repuesto.Descripcion,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,

                }).ToList();
        }

    }

}
