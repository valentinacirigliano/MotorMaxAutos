using MotorMax.Entidades.Dto.Repuesto;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorMax.Servicios.Interfaces
{
    public interface IServiciosRepuestos
    {
        void Guardar(Repuesto repuesto);
        void Borrar(int id);
        bool EstaRelacionado(Repuesto repuesto);
        bool Existe(Repuesto repuesto);
        Repuesto GetRepuestoPorId(int id);
        List<RepuestoListDto> GetRepuestos(bool todos);
        void ActualizarUnidadesEnPedido(int repuestoId, int cantidad, bool suma);
        List<RepuestoListDto> Filtrar(Func<Repuesto, bool> predicado);
        List<SelectListItem> GetRepuestosDropDownList();
        int GetCantidad(Func<Repuesto, bool> predicado);
    }
}
