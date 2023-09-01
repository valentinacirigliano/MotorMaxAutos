using MotorMax.Datos.Interfaces;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using MotorMax.Entidades.Dto.Venta;
using MotorMax.Entidades.Enums;

namespace MotorMax.Datos.Repositorios
{
    public class RepositorioVentas : IRepositorioVentas
    {
        private readonly AutosDbContext _context;

        public RepositorioVentas(AutosDbContext context)
        {
            _context = context;
        }

        public void Agregar(Venta venta)
        {
            _context.Ventas.Add(venta);
        }

        public List<VentaListDto> Filtrar(Func<Venta, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad()
        {
            return _context.Ventas.Count();
        }

        public Venta GetVentaPorId(int id)
        {
            return _context.Ventas
                .Include(v => v.Cliente)
                .SingleOrDefault(v => v.VentaId == id);

        }
        public VentaListDto GetVentaListDtoPorId(int id)
        {

            return _context.Ventas
                .Include(v => v.Cliente)
                .Select(v => new VentaListDto
                {
                    VentaId = v.VentaId,
                    FechaOperacion = v.FechaOperacion,
                    Cliente = v.Cliente.NombreApellido,
                    Monto = v.Monto,
                    Estado = v.Estado.ToString(),
                })
                .SingleOrDefault(v => v.VentaId == id);

        }

        public List<VentaListDto> GetVentas()
        {
            return _context.Ventas
                .Include(v => v.Cliente)
                .OrderBy(v => v.FechaOperacion)
                .Select(v => new VentaListDto
                {
                    VentaId = v.VentaId,
                    FechaOperacion = v.FechaOperacion,
                    Cliente = v.Cliente.NombreApellido,
                    Monto = v.Monto,
                    Estado = v.Estado.ToString()
                }).ToList();


        }

    }
}
