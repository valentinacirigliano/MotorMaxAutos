using MotorMax.Datos.Interfaces;
using MotorMax.Datos;
using MotorMax.Entidades.Dto.RepuestoProveedor;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotorMax.Datos.Repositorios;
using MotorMax.Servicios.Interfaces;

namespace MotorMax.Servicios.Servicios
{
    public class ServiciosRepuestosProveedores :IServiciosRepuestosProveedores
    {
        private readonly IRepositorioRepuestosProveedores _repoRepProv;
        private readonly IRepositorioRepuestos _repoRepuestos;
        private readonly IRepositorioProveedores _repoProveedores;
        private readonly IUnitOfWork _unitOfWork;


        public ServiciosRepuestosProveedores(IRepositorioRepuestosProveedores repositorioRepuestosProveedores,
            IUnitOfWork unitOfWork,
            IRepositorioRepuestos repositorioRepuestos,
            IRepositorioProveedores repoProveedores)
        {
            _repoRepProv = repositorioRepuestosProveedores;
            _repoRepuestos = repositorioRepuestos;
            _unitOfWork = unitOfWork;
            _repoProveedores = repoProveedores;
        }
        public void Borrar(int repuestoId, int proveedorId)
        {
            try
            {
                _repoRepProv.Borrar(repuestoId,proveedorId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Existe(RepuestoProveedor repuestoProveedor)
        {
            try
            {
                return _repoRepProv.Existe(repuestoProveedor);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<RepuestoProveedorListDto> GetRepuestosProveedores()
        {
            try
            {
                return _repoRepProv.GetRepuestosProveedores();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Guardar(RepuestoProveedor repuestoProveedor)
        {
            try
            {
               _repoRepProv.Guardar(repuestoProveedor);
               _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public int GetCantidad()
        {
            try
            {
                return _repoRepProv.GetCantidad();


            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RepuestoProveedor> GetListaPorProveedor(int proveedorId)
        {
            try
            {
                return _repoRepProv.GetListaPorProveedor(proveedorId);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Repuesto> GetRepuestosPorProveedor(int proveedorId)
        {
            try
            {
                var listaRepoProv = _repoRepProv.GetListaPorProveedor(proveedorId);
                List<Repuesto> listaRepuestos = new List<Repuesto>();
                foreach(var item in listaRepoProv)
                {
                    listaRepuestos.Add(_repoRepuestos.GetRepuestoPorId(item.RepuestoId));
                }
                return listaRepuestos;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Proveedor> GetProveedoresPorRepuesto(int repuestoId)
        {
            try
            {
                var listaRepoProv = _repoRepProv.GetListaPorRepuesto(repuestoId);
                List<Proveedor> listaProveedores = new List<Proveedor>();
                foreach (var item in listaRepoProv)
                {
                    listaProveedores.Add(_repoProveedores.GetProveedorPorId(item.ProveedorId));
                }
                return listaProveedores;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
