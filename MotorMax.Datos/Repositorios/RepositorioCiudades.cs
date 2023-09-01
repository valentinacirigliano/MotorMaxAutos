using MotorMax.Datos.Interfaces;
using MotorMax.Entidades.Dto.Ciudad;
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
    public class RepositorioCiudades : IRepositorioCiudades
    {
        private readonly AutosDbContext _context;

        public RepositorioCiudades(AutosDbContext context)
        {
            _context = context;
        }

        public void Agregar(Ciudad ciudad)
        {
            try
            {
                _context.Ciudades.Add(ciudad);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Borrar(int ciudadId)
        {
            try
            {
                var ciudadInDb = GetCiudadPorId(ciudadId);
                if (ciudadInDb == null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                else
                {
                    _context.Entry(ciudadInDb).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Editar(Ciudad ciudad)
        {
            try
            {
                var ciudadInDb = GetCiudadPorId(ciudad.CiudadId);

                if (ciudadInDb == null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                ciudadInDb.Nombre = ciudad.Nombre;
                ciudadInDb.ProvinciaId = ciudad.ProvinciaId;


                _context.Entry(ciudadInDb).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionada(Ciudad ciudad)
        {
            try
            {
                return _context.Clientes.Any(c => c.CiudadId == ciudad.CiudadId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Ciudad ciudad)
        {
            try
            {
                //if (ciudad.CiudadId == 0)
                //{
                    return _context.Ciudades.Any(c => c.Nombre == ciudad.Nombre
                        && c.ProvinciaId == ciudad.ProvinciaId);

                //}
                //return _context.Ciudades.Any(c => c.Nombre == ciudad.Nombre
                //            && c.ProvinciaId == ciudad.ProvinciaId
                //            && c.CiudadId != ciudad.CiudadId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CiudadListDto> Filtrar(Func<Ciudad, bool> predicado, int cantidad, int pagina)
        {
            return _context.Ciudades.Include(c => c.Provincia)
                .Where(predicado)
                .OrderBy(c => c.Nombre)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .Select(c => new CiudadListDto
                {
                    CiudadId = c.CiudadId,
                    Nombre = c.Nombre,
                    NombreProvincia = c.Provincia.Nombre

                }).ToList();
        }

        public int GetCantidad()
        {
            return _context.Ciudades.Count();
        }

        public int GetCantidad(Func<Ciudad, bool> predicado)
        {
            return _context.Ciudades.Count(predicado);
        }

        public List<CiudadListDto> GetCiudades()
        {
            return _context.Ciudades.Include(c => c.Provincia)
                .OrderBy(c=>c.Nombre)
                .Select(c => new CiudadListDto
                {
                    CiudadId = c.CiudadId,
                    Nombre = c.Nombre,
                    NombreProvincia = c.Provincia.Nombre
                })
                .AsNoTracking()
                .ToList();
        }

        public List<CiudadListDto> GetCiudades(int provinciaId)
        {
            try
            {
                return _context.Ciudades.Include(c => c.Provincia)
                    .Where(c => c.ProvinciaId == provinciaId)
                    .Select(c => new CiudadListDto
                    {
                        CiudadId = c.CiudadId,
                        Nombre = c.Nombre,
                        NombreProvincia = c.Provincia.Nombre
                    }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CiudadListDto> GetCiudadesPorPagina(int cantidad, int pagina)
        {
            return _context.Ciudades.Include(c => c.Provincia)
                .OrderBy(c => c.ProvinciaId)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .Select(c => new CiudadListDto
                {
                    CiudadId = c.CiudadId,
                    Nombre = c.Nombre,
                    NombreProvincia = c.Provincia.Nombre
                }).ToList();
        }

        public Ciudad GetCiudadPorId(int ciudadId)
        {
            try
            {
                return _context.Ciudades.Include(c => c.Provincia)

                    .SingleOrDefault(c => c.CiudadId == ciudadId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetCiudadesDropDownList(int provinciaId)
        {
            var lista = GetCiudades(provinciaId);
            var dropDownCiudades = lista.Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.CiudadId.ToString()
            }).ToList();
            return dropDownCiudades;
        }

        public List<SelectListItem> GetCiudadesDropDownList()
        {
            var lista = GetCiudades();
            var dropDownCiudades = lista.Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.CiudadId.ToString()
            }).ToList();
            return dropDownCiudades;
        }

    }

}
