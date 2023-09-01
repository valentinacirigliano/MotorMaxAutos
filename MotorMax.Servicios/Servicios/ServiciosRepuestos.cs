using MotorMax.Datos;
using MotorMax.Datos.Interfaces;
using MotorMax.Datos.Repositorios;
using MotorMax.Entidades.Dto.Repuesto;
using MotorMax.Entidades.Entidades;
using MotorMax.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorMax.Servicios.Servicios
{
    public class ServiciosRepuestos:IServiciosRepuestos
    {
        private readonly IRepositorioRepuestos _repositorio;
        private readonly IUnitOfWork _unitOfWork;


        public ServiciosRepuestos(IRepositorioRepuestos repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public void ActualizarUnidadesEnPedido(int repuestoId, int cantidad, bool suma)
        {
            try
            {
                _repositorio.ActualizarUnidadesEnPedido(repuestoId, cantidad,suma);
                _unitOfWork.SaveChanges();
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
                _repositorio.Borrar(id);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Repuesto repuesto)
        {
            try
            {
                return _repositorio.EstaRelacionado(repuesto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Repuesto repuesto)
        {
            try
            {
                return _repositorio.Existe(repuesto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RepuestoListDto> Filtrar(Func<Repuesto, bool> predicado)
        {
            try
            {
                return _repositorio.Filtrar(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(Func<Repuesto, bool> predicado)
        {
            try
            {
                return _repositorio.GetCantidad(predicado);
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
                return _repositorio.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Repuesto GetRepuestoPorId(int id)
        {
            try
            {
                return _repositorio.GetRepuestoPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RepuestoListDto> GetRepuestos(bool todos)
        {
            try
            {
                return _repositorio.GetRepuestos(todos);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public void Guardar(Repuesto repuesto)
        {
            try
            {
                if (repuesto.RepuestoId == 0)
                {
                    _repositorio.Agregar(repuesto);
                }
                else
                {
                    _repositorio.Editar(repuesto);
                }
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetRepuestosDropDownList()
        {
            try
            {
                return _repositorio.GetRepuestosDropDownList();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
