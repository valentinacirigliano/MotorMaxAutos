using MotorMax.Datos.Interfaces;
using MotorMax.Entidades.Dto.Repuesto;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorMax.Datos.Repositorios
{
    public class RepositorioRepuestos :IRepositorioRepuestos
    {
        private readonly AutosDbContext _context;
        public RepositorioRepuestos(AutosDbContext context)
        {
            _context = context;
        }

        public void ActualizarStock(int repuestoId, int cantidad)
        {
            var repuestoInDb = _context.Repuestos.SingleOrDefault(p => p.RepuestoId == repuestoId);
            repuestoInDb.UnidadesEnPedido -= cantidad;
            repuestoInDb.Stock -= cantidad;
            _context.Entry(repuestoInDb).State = EntityState.Modified;

        }
        public void Agregar(Repuesto repuesto)
        {
            _context.Repuestos.Add(repuesto);
        }

        public void ActualizarUnidadesEnPedido(int repuestoId, int cantidad, bool suma)
        {
            var repuestoInDb = _context.Repuestos.SingleOrDefault(p => p.RepuestoId == repuestoId);
            if (suma == true)
            {
                repuestoInDb.UnidadesEnPedido += cantidad;
            }
            else
            {
                repuestoInDb.UnidadesEnPedido = 0;
            }
            
            _context.Entry(repuestoInDb).State = EntityState.Modified;
        }


        public void Borrar(int id)
        {
            var repuestoInDb = _context.Repuestos.SingleOrDefault(p => p.RepuestoId == id);
            if (repuestoInDb == null)
            {
                throw new Exception("Repuesto borrado por otro usuario");
            }
            _context.Entry(repuestoInDb).State = EntityState.Deleted;
        }

        public void Editar(Repuesto repuesto)
        {
            var repuestoInDb = _context.Repuestos.SingleOrDefault(p => p.RepuestoId == repuesto.RepuestoId);
            if (repuestoInDb == null)
            {
                throw new Exception("Repuesto borrado por otro usuario");
            }
            repuestoInDb.Descripcion = repuesto.Descripcion;
            repuestoInDb.CategoriaId= repuesto.CategoriaId;
            repuestoInDb.Stock = repuesto.Stock;
            repuestoInDb.PrecioUnitario = repuesto.PrecioUnitario;
            repuestoInDb.Imagen = repuesto.Imagen;
            repuestoInDb.Suspendido = repuesto.Suspendido;
        }


        public bool EstaRelacionado(Repuesto repuesto)
        {
            try
            {
                return _context.DetalleVenta.Any(v => v.RepuestoId == repuesto.RepuestoId);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool Existe(Repuesto repuesto)
        {
            if (repuesto.RepuestoId == 0)
            {
                return _context.Repuestos.Include(c => c.Categoria).Any(p => p.Descripcion == repuesto.Descripcion);
            }
            return _context.Repuestos.Include(c=>c.Categoria).
                Any(p=>p.RepuestoId==repuesto.RepuestoId);

        }


        public List<RepuestoListDto> Filtrar(Func<Repuesto, bool> predicado)
        {
            return _context.Repuestos
                .Include(c => c.Categoria)
                .OrderBy(c=>c.Descripcion)
                .Where(predicado)
                .Select(p => new RepuestoListDto
            {
                RepuestoId = p.RepuestoId,
                Descripcion = p.Descripcion,
                Categoria = p.Categoria.NombreCategoria,
                PrecioUnitario = p.PrecioUnitario,
                Imagen = p.Imagen,
                Suspendido = p.Suspendido,
                UnidadesDisponibles = p.UnidadesDisponibles()
            })
                .ToList();
        }


        public Repuesto GetRepuestoPorId(int id)
        {
            return _context.Repuestos.SingleOrDefault(p => p.RepuestoId == id);
        }

        public List<RepuestoListDto> GetRepuestos(bool todos)
        {
            IQueryable<RepuestoListDto> query = _context.Repuestos
                .Include(c => c.Categoria)
                .OrderBy(c=>c.Descripcion)
                .Select(p => new RepuestoListDto()
                {
                    RepuestoId = p.RepuestoId,
                    Descripcion = p.Descripcion,
                    Categoria = p.Categoria.NombreCategoria,
                    PrecioUnitario = p.PrecioUnitario,
                    UnidadesDisponibles = p.Stock - p.UnidadesEnPedido,
                    Suspendido = p.Suspendido,
                    Imagen = p.Imagen
                });
            if (!todos)
            {
                query = query.Where(p => (p.UnidadesDisponibles > 0 && p.Suspendido == false) || p.Suspendido == false);
            }
            return query.ToList();
            
        }


        public int GetCantidad(Func<Repuesto, bool> predicado)
        {
            return _context.Repuestos.Count(predicado);
        }

        public int GetCantidad()
        {
            return _context.Repuestos.Count();
        }

        public List<SelectListItem> GetRepuestosDropDownList()
        {
            var lista = GetRepuestos(true);
            var dropDown = lista.Select(p => new SelectListItem
            {
                Text = p.Descripcion,
                Value = p.RepuestoId.ToString()
            }).ToList();
            return dropDown;
        }
    }
}
