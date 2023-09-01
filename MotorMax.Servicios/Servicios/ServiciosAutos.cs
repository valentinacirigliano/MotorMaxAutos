using MotorMax.Datos;
using MotorMax.Datos.Repositorios;
using MotorMax.Entidades.Dto.Auto;
using MotorMax.Entidades.Entidades;
using MotorMax.Servicios.Interfaces;
using System;
using System.Collections.Generic;

namespace MotorMax.Servicios.Servicios
{
    public class ServiciosAutos : IServiciosAutos
    {
        private readonly RepositorioAutos _repositorio;
        private readonly IUnitOfWork _unitOfWork;


        public ServiciosAutos(RepositorioAutos repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public void ActualizarUnidadesEnPedido(string patente, int cantidad)
        {
            try
            {
                _repositorio.ActualizarUnidadesEnPedido(patente, cantidad);
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

        public bool EstaRelacionado(Auto auto)
        {
            try
            {
                return _repositorio.EstaRelacionado(auto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Auto auto)
        {
            try
            {
                return _repositorio.Existe(auto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AutoListDto> Filtrar(Func<Auto, bool> predicado)
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

        public int GetCantidad(Func<Auto, bool> predicado)
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

        public Auto GetAutoPorId(int id)
        {
            try
            {
                return _repositorio.GetAutoPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Auto GetAutoPorPatente(string patente)
        {
            try
            {
                return _repositorio.GetAutoPorPatente(patente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AutoListDto> GetAutos()
        {
            try
            {
                return _repositorio.GetAutos();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AutoListDto> GetAutos(bool todos)
        {
            try
            {
                return _repositorio.GetAutos(todos);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<AutoListDto> GetAutos(int categoriaId)
        {
            try
            {
                return _repositorio.GetAutos(categoriaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Auto auto)
        {
            try
            {
                if (!Existe(auto))
                {
                    _repositorio.Agregar(auto);
                }
                else
                {
                    _repositorio.Editar(auto);
                }
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
