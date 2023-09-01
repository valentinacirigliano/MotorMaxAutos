using MotorMax.Datos.Interfaces;
using MotorMax.Datos;
using MotorMax.Entidades.Dto.DetallesVenta;
using MotorMax.Entidades.Dto.Venta;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MotorMax.Servicios.Interfaces;
using MotorMax.Entidades.Enums;

namespace MotorMax.Servicios.Servicios 
{
    public class ServiciosVentas:IServiciosVentas
    {
        private readonly IRepositorioVentas _repositorio;
        private readonly IRepositorioDetalleVentas _repoDetalleVentas;
        private readonly IRepositorioRepuestos _repoRepuestos;
        private readonly IRepositorioCarritos _repoCarritos;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosVentas(IRepositorioVentas repositorio,
            IRepositorioDetalleVentas repoDetalleVentas,
            IRepositorioRepuestos repoRepuestos,
            IRepositorioCarritos repositorioCarritos,
            IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _repoDetalleVentas = repoDetalleVentas;
            _repoRepuestos = repoRepuestos;
            _repoCarritos = repositorioCarritos;
            _unitOfWork = unitOfWork;
        }

        public int GetCantidad()
        {
            try
            {
                return _repositorio.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<DetalleVentaListDto> GetDetalleVenta(int ventaId)
        {
            try
            {
                return _repoDetalleVentas.GetDetalleVentas(ventaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<VentaListDto> GetVentas()
        {
            try
            {
                return _repositorio.GetVentas();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Venta GetVentasPorId(int id)
        {
            try
            {
                return _repositorio.GetVentaPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public VentaListDto GetVentaListDtoPorId(int id)
        {
            try
            {
                return _repositorio.GetVentaListDtoPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int Guardar(Venta venta, string user)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {

                    var ventaGuardar = new Venta()
                    {
                        ClienteId = venta.ClienteId,
                        FechaOperacion = venta.FechaOperacion,
                        TransactionId = venta.TransactionId,
                        Estado = Estado.Paga,
                        Monto = venta.Monto
                    };
                    _repositorio.Agregar(ventaGuardar);
                    _unitOfWork.SaveChanges();
                    foreach (var item in venta.Detalles)
                    {
                        item.VentaId = ventaGuardar.VentaId;
                        _repoDetalleVentas.Agregar(item);
                        _repoRepuestos.ActualizarStock(item.RepuestoId, item.Cantidad);
                        _repoCarritos.Borrar(user, item.RepuestoId);
                    }
                    _unitOfWork.SaveChanges();
                    transaction.Complete();
                    return ventaGuardar.VentaId;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
