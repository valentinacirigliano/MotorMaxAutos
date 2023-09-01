using MotorMax.Entidades.Dto.Marca;
using MotorMax.Entidades.Entidades;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MotorMax.Datos.Interfaces
{
    public interface IRepositorioMarcas
    {
        List<Marca> GetMarcas();
        void Agregar(Marca marca);
        void Editar(Marca marca);
        void Borrar(int id);
        bool Existe(Marca marca);
        Marca GetMarcaPorId(int marcaId);
        bool EstaRelacionado(Marca marca);
        List<Marca> GetMarcasPorPagina(int cantidad, int pagina);
        int GetCantidad();
        List<SelectListItem> GetMarcasDropDownList();
    }
}
