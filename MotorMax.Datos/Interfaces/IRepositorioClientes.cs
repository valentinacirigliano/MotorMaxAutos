using MotorMax.Entidades.Dto.Cliente;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.Interfaces
{
    public interface IRepositorioClientes
    {
        void Agregar(Cliente cliente);
        void Borrar(int id);
        void Editar(Cliente cliente);
        bool EstaRelacionado(Cliente cliente);
        bool Existe(Cliente cliente);
        int GetCantidad();
        Cliente GetClientePorId(int id);
        List<ClienteListDto> GetClientes();
        List<ClienteListDto> GetClientes(int provinciaId, int ciudadId);
        List<ClienteListDto> Filtrar(Func<Cliente, bool> predicado);
        Cliente GetClientePorUsuario(string user);
    }
}
