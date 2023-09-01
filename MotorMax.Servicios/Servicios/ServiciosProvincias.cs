using MotorMax.Datos.Interfaces;
using MotorMax.Datos;
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
    public class ServiciosProvincias:IServiciosProvincias
    {
        private readonly IRepositorioProvincias _repositorio;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosProvincias(IRepositorioProvincias repositorio, IUnitOfWork unitOfWork)
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

        public bool EstaRelacionado(Provincia provincia)
        {
            try
            {
                return _repositorio.EstaRelacionado(provincia);
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
                return _repositorio.Existe(provincia);
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

        public List<Provincia> GetProvincias()
        {
            try
            {
                return _repositorio.GetProvincias();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetProvinciasDropDownList()
        {
            try
            {
                return _repositorio.GetProvinciasDropDownList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Provincia GetProvinciaPorId(int provinciaId)
        {
            try
            {
                return _repositorio.GetProvinciaPorId(provinciaId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Provincia> GetProvinciasPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorio.GetProvinciasPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Guardar(Provincia provincia)
        {
            try
            {
                if (provincia.ProvinciaId == 0)
                {
                    _repositorio.Agregar(provincia);

                }
                else
                {
                    _repositorio.Editar(provincia);
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
