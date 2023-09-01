using MotorMax.Datos.Interfaces;
using MotorMax.Datos;
using MotorMax.Entidades.Dto.Marca;
using MotorMax.Entidades.Entidades;
using MotorMax.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MotorMax.Servicios.Servicios
{
    public class ServiciosMarcas : IServiciosMarcas
    {
        private readonly IRepositorioMarcas _repositorio;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosMarcas(IRepositorioMarcas repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
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

        public bool EstaRelacionado(Marca marca)
        {
            try
            {
                return _repositorio.EstaRelacionado(marca);
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
                return _repositorio.Existe(marca);
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

        public List<Marca> GetMarcas()
        {
            try
            {
                return _repositorio.GetMarcas();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Marca GetMarcaPorId(int marcaId)
        {
            try
            {
                return _repositorio.GetMarcaPorId(marcaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        

        public void Guardar(Marca marca)
        {
            try
            {
                if (marca.MarcaId == 0)
                {
                    _repositorio.Agregar(marca);

                }
                else
                {
                    _repositorio.Editar(marca);
                }
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<Marca> GetMarcasPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorio.GetMarcasPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetMarcasDropDownList()
        {
            try
            {
                return _repositorio.GetMarcasDropDownList();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }

}
