using MotorMax.Datos.Interfaces;
using MotorMax.Entidades.Dto.Marca;
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
    public class RepositorioMarcas : IRepositorioMarcas
    {
        private readonly AutosDbContext _context;

        public RepositorioMarcas(AutosDbContext context)
        {
            _context = context;
        }


        public void Agregar(Marca marca)
        {

            _context.Marcas.Add(marca);


        }

        public void Borrar(int id)
        {
            try
            {
                var marcaInDb = _context.Marcas.SingleOrDefault(p => p.MarcaId == id);
                if (marcaInDb == null)
                {
                    Exception ex = new Exception("Borrado por otro usuario");
                    throw ex;
                }
                _context.Entry(marcaInDb).State = EntityState.Deleted;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(Marca marca)
        {
            try
            {
                var marcaInDb = _context.Marcas.SingleOrDefault(c => c.MarcaId == marca.MarcaId);
                if (marcaInDb == null)
                {
                    throw new Exception("Borrado por otro usuario");
                }
                marcaInDb.NombreMarca = marca.NombreMarca;
                _context.Entry(marcaInDb).State = EntityState.Modified;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionado(Marca marca)
        {
            try
            {
                return _context.Autos.Any(c => c.MarcaId == marca.MarcaId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Marca marca)
        {
            try
            {
                if (marca.MarcaId == 0)
                {
                    return _context.Marcas.Any(p => p.NombreMarca == marca.NombreMarca);
                }
                return _context.Marcas.Any(p => p.NombreMarca == marca.NombreMarca && p.MarcaId != marca.MarcaId);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad()
        {
            return _context.Marcas.Count();
        }

        public List<Marca> GetMarcas()
        {
            try
            {
                return _context.Marcas
                    .OrderBy(p => p.NombreMarca)
                    .ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Marca> GetMarcasPorPagina(int cantidad, int pagina)
        {
            return _context.Marcas.OrderBy(p => p.NombreMarca)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .ToList();
        }

        public Marca GetMarcaPorId(int marcaId)
        {
            try
            {
                var marcaInDb = _context.Marcas
                    .SingleOrDefault(p => p.MarcaId == marcaId);
                return marcaInDb;


            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetMarcasDropDownList()
        {
            var lista = GetMarcas();
            var dropDown = lista.Select(p => new SelectListItem
            {
                Text = p.NombreMarca,
                Value = p.MarcaId.ToString()
            }).ToList();
            return dropDown;
        }
    }

}
