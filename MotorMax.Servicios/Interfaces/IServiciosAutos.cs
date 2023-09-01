using MotorMax.Entidades.Dto.Auto;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Servicios.Interfaces
{
    public interface IServiciosAutos
    {
        void Guardar(Auto auto);
        void Borrar(int id);
        bool EstaRelacionado(Auto auto);
        bool Existe(Auto auto);
        Auto GetAutoPorPatente(string patente);
        Auto GetAutoPorId(int id);
        List<AutoListDto> GetAutos();
        List<AutoListDto> GetAutos(bool todos);
        List<AutoListDto> GetAutos(int marcaId);
        List<AutoListDto> Filtrar(Func<Auto, bool> predicado);
        void ActualizarUnidadesEnPedido(string patente, int cantidad);
        int GetCantidad(Func<Auto, bool> value);
        int GetCantidad();
    }
}
