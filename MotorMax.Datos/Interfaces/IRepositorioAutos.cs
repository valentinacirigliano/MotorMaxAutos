using MotorMax.Entidades.Dto.Auto;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.Interfaces
{
    public interface IRepositorioAutos
    {
        void Agregar(Auto auto);
        void Borrar(int id);
        void Editar(Auto auto);
        bool EstaRelacionado(Auto auto);
        bool Existe(Auto auto);
        Auto GetAutoPorPatente(string Patente);
        Auto GetAutoPorId(int id);
        List<AutoListDto> GetAutos();
        List<AutoListDto> GetAutos(bool todos);
        List<AutoListDto> GetAutos(int marcaId);
        List<AutoListDto> Filtrar(Func<Auto, bool> predicado);
        void ActualizarStock(string patente, int cantidad);
        int GetCantidad();
        int GetCantidad(Func<Auto, bool> predicado);

    }
}
