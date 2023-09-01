using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MotorMax.Datos.Interfaces
{
    public interface IRepositorioProvincias
    {
        List<Provincia> GetProvincias();
        void Agregar(Provincia provincia);
        void Editar(Provincia provincia);
        void Borrar(int id);
        bool Existe(Provincia provincia);
        Provincia GetProvinciaPorId(int provinciaId);
        bool EstaRelacionado(Provincia provincia);
        List<Provincia> GetProvinciasPorPagina(int cantidad, int pagina);
        int GetCantidad();
        List<SelectListItem> GetProvinciasDropDownList();
    }
}
