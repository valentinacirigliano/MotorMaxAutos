using MotorMax.Datos.Interfaces;
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
    public class RepositorioProvincias : IRepositorioProvincias
    {
        private readonly AutosDbContext _context;

        public RepositorioProvincias(AutosDbContext context)
        {
            _context = context;
        }


        public void Agregar(Provincia provincia)
        {
            _context.Provincias.Add(provincia);

        }

        public void Borrar(int id)
        {
            try
            {
                var provinciaInDb = _context.Provincias.SingleOrDefault(p => p.ProvinciaId == id);
                if (provinciaInDb == null)
                {
                    Exception ex = new Exception("Borrado por otro usuario");
                    throw ex;
                }
                _context.Entry(provinciaInDb).State = EntityState.Deleted;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(Provincia provincia)
        {
            try
            {
                _context.Entry(provincia).State = EntityState.Modified;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionado(Provincia provincia)
        {
            try
            {
                return _context.Ciudades.Any(c => c.ProvinciaId == provincia.ProvinciaId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Provincia provincia)
        {
            try
            {
                if (provincia.ProvinciaId == 0)
                {
                    return _context.Provincias.Any(p => p.Nombre == provincia.Nombre);
                }
                return _context.Provincias.Any(p => p.Nombre == provincia.Nombre && p.ProvinciaId != provincia.ProvinciaId);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad()
        {
            return _context.Provincias.Count();
        }

        public List<Provincia> GetProvincias()
        {
            try
            {
                return _context.Provincias
                    .OrderBy(p => p.Nombre)
                    .ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Provincia> GetProvinciasPorPagina(int cantidad, int pagina)
        {
            return _context.Provincias.OrderBy(p => p.Nombre)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .ToList();
        }

        public Provincia GetProvinciaPorId(int Id)
        {
            try
            {
                var provinciaInDb = _context.Provincias
                    .SingleOrDefault(p => p.ProvinciaId == Id);
                return provinciaInDb;


            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetProvinciasDropDownList()
        {
            var lista = GetProvincias();
            var dropDown = lista.Select(p => new SelectListItem
            {
                Text = p.Nombre,
                Value = p.ProvinciaId.ToString()
            }).ToList();
            return dropDown;
        }

    }

}
