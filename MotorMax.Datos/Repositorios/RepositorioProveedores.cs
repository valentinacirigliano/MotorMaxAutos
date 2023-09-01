using MotorMax.Datos.Interfaces;
using MotorMax.Entidades.Dto.Proveedor;
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
    public class RepositorioProveedores:IRepositorioProveedores
    {
        private readonly AutosDbContext _context;

        public RepositorioProveedores(AutosDbContext context)
        {
            _context = context;
        }

        public void Agregar(Proveedor proveedor)
        {
            try
            {
                _context.Proveedores.Add(proveedor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Borrar(int id)
        {
            try
            {
                var proveedorInDb = GetProveedorPorId(id);
                if (proveedorInDb == null)
                {
                    throw new Exception("Registro borrado por otro usuario");
                }
                _context.Entry(proveedorInDb).State = EntityState.Deleted;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Editar(Proveedor proveedor)
        {
            try
            {
                var proveedorInDb = GetProveedorPorId(proveedor.ProveedorId);
                if (proveedorInDb == null)
                {
                    throw new Exception("Registro borrado por otro usuario");
                }
                _context.Entry(proveedor).State = EntityState.Modified;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Proveedor proveedor)
        {
            try
            {
                return _context.RepuestosProveedores.Any(p => p.ProveedorId == proveedor.ProveedorId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Proveedor proveedor)
        {
            try
            {
                if (proveedor.ProveedorId == 0)
                {
                    return _context.Proveedores.Any(c => c.NombreProveedor == proveedor.NombreProveedor);
                }
                return _context.Proveedores.Any(c => c.NombreProveedor == proveedor.NombreProveedor && c.ProveedorId != proveedor.ProveedorId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProveedorListDto> Filtrar(Func<Proveedor, bool> predicado)
        {
            return _context.Proveedores
                .Include(c => c.Ciudad)
                .Where(predicado)
                .Select(c => new ProveedorListDto
                {
                    ProveedorId = c.ProveedorId,
                    NombreProveedor = c.NombreProveedor,
                    Ciudad = c.Ciudad.Nombre
                }).ToList();
        }

        public Proveedor GetProveedorPorId(int id)
        {
            try
            {
                return _context.Proveedores
                    .Include(c => c.Ciudad)
                    .SingleOrDefault(c => c.ProveedorId == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProveedorListDto> GetProveedores()
        {
            return _context.Proveedores
                .Include(c => c.Ciudad).Select(c => new ProveedorListDto
                {
                    ProveedorId = c.ProveedorId,
                    NombreProveedor = c.NombreProveedor,
                    Ciudad = c.Ciudad.Nombre
                })
                .AsNoTracking()
                .ToList();
        }

        public List<ProveedorListDto> GetProveedores(int ciudadId)
        {
            try
            {
                return _context.Proveedores
                    .Include(c => c.Ciudad)
                    .Where(c => c.CiudadId == ciudadId)
                    .Select(c => new ProveedorListDto
                    {
                        ProveedorId = c.ProveedorId,
                        NombreProveedor = c.NombreProveedor,
                        Ciudad = c.Ciudad.Nombre
                    }).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<SelectListItem> GetProveedoresDropDownList()
        {
            var lista = GetProveedores();
            var dropDown = lista.Select(p => new SelectListItem
            {
                Text = p.NombreProveedor,
                Value = p.ProveedorId.ToString()
            }).ToList();
            return dropDown;
        }
    }
}
