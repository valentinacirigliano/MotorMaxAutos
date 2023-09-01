using MotorMax.Entidades.Dto.Repuesto;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorMax.Datos.Interfaces
{
    public interface IRepositorioRepuestos
    {
        void Agregar(Repuesto repuesto);
        void Borrar(int id);
        void Editar(Repuesto repuesto);
        bool EstaRelacionado(Repuesto repuesto);
        bool Existe(Repuesto repuesto);
        Repuesto GetRepuestoPorId(int id);
        List<RepuestoListDto> GetRepuestos(bool todos);
        void ActualizarUnidadesEnPedido(int repuestoId, int cantidad, bool suma);
        List<RepuestoListDto> Filtrar(Func<Repuesto, bool> predicado);
        void ActualizarStock(int repuestoId, int cantidad);
        List<SelectListItem> GetRepuestosDropDownList();
        int GetCantidad(Func<Repuesto, bool> predicado);
        int GetCantidad();
    }
}
