using MotorMax.Datos.Interfaces;
using MotorMax.Entidades.Dto.RepuestoProveedor;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.Repositorios
{
    public class RepositoriosRepuestosProveedores:IRepositorioRepuestosProveedores
    {
        private readonly AutosDbContext _context;

        public RepositoriosRepuestosProveedores(AutosDbContext context)
        {
            _context = context;
        }

        public void Guardar(RepuestoProveedor repuestoProveedor)
        {
            try
            {
                _context.RepuestosProveedores.Add(repuestoProveedor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Borrar(int repuestoId, int proveedorId)
        {
            try
            {
                var repuestoProveedorInDb = GetRepuestoProveedorPorId(repuestoId, proveedorId);
                if (repuestoProveedorInDb == null)
                {
                    throw new Exception("Registro borrado por otro usuario");
                }
                _context.Entry(repuestoProveedorInDb).State = EntityState.Deleted;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private RepuestoProveedor GetRepuestoProveedorPorId(int repuestoId, int proveedorId)
        {
            try
            {
                return _context.RepuestosProveedores.Include(c => c.Repuesto).Include(c=>c.Proveedor)
                    .SingleOrDefault(c => c.RepuestoId == repuestoId && c.ProveedorId == proveedorId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad()
        {
            return _context.RepuestosProveedores.Count();
        }

        public bool Existe(RepuestoProveedor repuestoProveedor)
        {
            try
            {
                return _context.RepuestosProveedores.Any
                    (c => c.RepuestoId == repuestoProveedor.RepuestoId && c.ProveedorId == repuestoProveedor.ProveedorId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RepuestoProveedorListDto> Filtrar(Func<RepuestoProveedor, bool> predicado)
        {
            return _context.RepuestosProveedores
                .Where(predicado)
                .Select(c => new RepuestoProveedorListDto
                {
                    RepuestoId =c.RepuestoId,
                    ProveedorId = c.ProveedorId
                }).ToList();
        }


        public List<RepuestoProveedorListDto> GetRepuestosProveedores()
        {
            return _context.RepuestosProveedores
                .Select(c => new RepuestoProveedorListDto
                {
                    RepuestoId = c.RepuestoId,
                    ProveedorId = c.ProveedorId
                }).ToList();
        }

        public List<RepuestoProveedor> GetListaPorProveedor(int proveedorId)
        {
            try
            {
                return _context.RepuestosProveedores
                    .Where(c => c.ProveedorId == proveedorId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RepuestoProveedor> GetListaPorRepuesto(int repuestoId)
        {
            try
            {
                return _context.RepuestosProveedores
                    .Where(c => c.RepuestoId == repuestoId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
