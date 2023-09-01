using MotorMax.Datos.Interfaces;
using MotorMax.Entidades.Dto.Auto;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.Repositorios
{
    public class RepositorioAutos : IRepositorioAutos
    {
        private readonly AutosDbContext _context;
        public RepositorioAutos(AutosDbContext context)
        {
            _context = context;
        }

        public void ActualizarStock(string patente, int cantidad)
        {
            var vehiculoInDb = _context.Autos.SingleOrDefault(p => p.Patente == patente);
            vehiculoInDb.UnidadesEnPedido -= cantidad;
            vehiculoInDb.Stock -= cantidad;
            _context.Entry(vehiculoInDb).State = EntityState.Modified;

        }
        public void Agregar(Auto auto)
        {
            _context.Autos.Add(auto);
        }

        public void ActualizarUnidadesEnPedido(string patente, int cantidad)
        {
            var vehiculoInDb = _context.Autos.SingleOrDefault(p => p.Patente == patente);
            vehiculoInDb.UnidadesEnPedido += cantidad;
            _context.Entry(vehiculoInDb).State = EntityState.Modified;
        }


        public void Borrar(int id)
        {
            var vehiculoInDb = _context.Autos.SingleOrDefault(p => p.AutoId == id);
            if (vehiculoInDb == null)
            {
                throw new Exception("Auto borrado por otro usuario");
            }
            _context.Entry(vehiculoInDb).State = EntityState.Deleted;
        }

        public void Editar(Auto auto)
        {
            var vehiculoInDb = _context.Autos.SingleOrDefault(p => p.Patente == auto.Patente);
            if (vehiculoInDb == null)
            {
                throw new Exception("Auto borrado por otro usuario");
            }
            vehiculoInDb.Modelo = auto.Modelo;
            vehiculoInDb.MarcaId = auto.MarcaId;
            vehiculoInDb.PrecioFinal = auto.PrecioFinal;
            vehiculoInDb.Stock = auto.Stock;
            vehiculoInDb.Suspendido = auto.Suspendido;
        }


        public bool EstaRelacionado(Auto auto)
        {
            return false;
        }


        public bool Existe(Auto auto)
        {
            if (auto.Patente == null)
            {
                return _context.Autos.Any(p => p.Modelo == auto.Modelo &&
                p.MarcaId == auto.MarcaId);
            }
            return _context.Autos.Any(p => p.Modelo == auto.Modelo &&
                p.MarcaId == auto.MarcaId && p.Patente != auto.Patente);

        }


        public List<AutoListDto> Filtrar(Func<Auto, bool> predicado)
        {
            try
            {
                //para probar si es el metodo o la tabla de la bd
                List<Auto> listaAutos = _context.Autos
                    .Include(p => p.Marca)
                    .ToList();
                List<Auto> listaFiltrada = _context.Autos
                    .Include(p => p.Marca)
                    .Where(predicado)
                    .ToList();
                //
                List<AutoListDto> listaFiltradaDto = _context.Autos.Include(p => p.Marca)
                       .Where(predicado)
                       .Select(p => new AutoListDto
                       {
                           AutoId = p.AutoId,
                           Patente = p.Patente,
                           Modelo = p.Modelo,
                           Marca = p.Marca.NombreMarca,
                           PrecioFinal = p.PrecioFinal,
                           UnidadesDisponibles = p.Stock - p.UnidadesEnPedido,
                           Suspendido = p.Suspendido
                       })
                       .ToList();
                return listaFiltradaDto;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public Auto GetAutoPorPatente(string patente)
        {
            return _context.Autos.Include(p => p.Marca)
                .SingleOrDefault(p => p.Patente == patente);
        }


        public List<AutoListDto> GetAutos(bool todos)
        {
            IQueryable<AutoListDto> query =
                (IQueryable<AutoListDto>)_context.Autos.Include(p => p.Marca)
                .Select(p => new AutoListDto()
                {
                    AutoId = p.AutoId,
                    Patente = p.Patente,
                    Modelo = p.Modelo,
                    Marca = p.Marca.NombreMarca,
                    PrecioFinal = p.PrecioFinal,
                    UnidadesDisponibles = p.Stock - p.UnidadesEnPedido,
                    Suspendido = p.Suspendido
                }).ToList();
            if (!todos)
            {
                query = query.Where(p => (p.UnidadesDisponibles > 0 && p.Suspendido == false) || p.Suspendido == false);
            }
            return query.ToList();

        }

        public List<AutoListDto> GetAutos()
        {
            return _context.Autos.Include(p => p.Marca)
                .Select(p => new AutoListDto()
                {
                    AutoId = p.AutoId,
                    Patente = p.Patente,
                    Modelo = p.Modelo,
                    Marca = p.Marca.NombreMarca,
                    PrecioFinal = p.PrecioFinal,
                    UnidadesDisponibles = p.Stock - p.UnidadesEnPedido,
                    Suspendido = p.Suspendido
                }).ToList();
        }

        public List<AutoListDto> GetAutos(int marcaId)
        {
            return _context.Autos.Include(p => p.Marca)
                .Where(p => p.MarcaId == marcaId)
                .Select(p => new AutoListDto()
                {
                    AutoId = p.AutoId,
                    Patente = p.Patente,
                    Modelo = p.Modelo,
                    Marca = p.Marca.NombreMarca,
                    PrecioFinal = p.PrecioFinal,
                    UnidadesDisponibles = p.Stock - p.UnidadesEnPedido,
                    Suspendido = p.Suspendido
                }).ToList();

        }
        public int GetCantidad()
        {
            return _context.Autos.Count();
        }

        public int GetCantidad(Func<Auto, bool> predicado)
        {
            return _context.Autos.Count(predicado);
        }

        public Auto GetAutoPorId(int id)
        {
            return _context.Autos.Include(p => p.Marca)
                .SingleOrDefault(p => p.AutoId == id);
        }
    }

}
