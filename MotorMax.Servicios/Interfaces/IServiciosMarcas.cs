using MotorMax.Entidades.Dto.Marca;
using MotorMax.Entidades.Entidades;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MotorMax.Servicios.Interfaces
{
    public interface IServiciosMarcas
    {
        List<Marca> GetMarcas();
        void Guardar(Marca marca);
        void Borrar(int id);
        bool Existe(Marca marca);
        Marca GetMarcaPorId(int marcaId);
        bool EstaRelacionado(Marca marca);
        List<Marca> GetMarcasPorPagina(int cantidad, int pagina);
        int GetCantidad();
        List<SelectListItem> GetMarcasDropDownList();
    }
}
