using MotorMax.Datos.Interfaces;
using MotorMax.Entidades.Dto.Cliente;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.Repositorios
{
    public class RepositorioClientes : IRepositorioClientes
    {
        private readonly AutosDbContext _context;

        public RepositorioClientes(AutosDbContext context)
        {
            _context = context;
        }

        public void Agregar(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Borrar(int id)
        {
            try
            {
                var clienteInDb = GetClientePorId(id);
                if (clienteInDb == null)
                {
                    throw new Exception("Registro borrado por otro usuario");
                }
                _context.Entry(clienteInDb).State = EntityState.Deleted;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad()
        {
            return _context.Clientes.Count();
        }
        public void Editar(Cliente cliente)
        {
            try
            {
                var clienteInDb = GetClientePorId(cliente.ClienteId);
                if (clienteInDb == null)
                {
                    throw new Exception("Registro borrado por otro usuario");
                }
                _context.Entry(cliente).State = EntityState.Modified;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Cliente cliente)
        {
            try
            {
                return _context.Ventas.Any(v => v.ClienteId == cliente.ClienteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Cliente cliente)
        {
            try
            {
                if (cliente.ClienteId == 0)
                {
                    return _context.Clientes.Any(c => c.NombreApellido == cliente.NombreApellido || c.Email==cliente.Email);
                }
                return _context.Clientes.Any(c => (c.NombreApellido == cliente.NombreApellido || c.Email == cliente.Email) 
                                                        && c.ClienteId != cliente.ClienteId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteListDto> Filtrar(Func<Cliente, bool> predicado)
        {
            return _context.Clientes.Include(c => c.Ciudad)
                .Include(c => c.Ciudad)
                .Where(predicado)
                .Select(c => new ClienteListDto
                {
                    ClienteId = c.ClienteId,
                    NombreApellido = c.NombreApellido,
                    Ciudad = c.Ciudad.Nombre
                }).ToList();
        }

        public Cliente GetClientePorId(int id)
        {
            try
            {
                return _context.Clientes.Include(c => c.Ciudad)
                    .SingleOrDefault(c => c.ClienteId == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteListDto> GetClientes()
        {
            return _context.Clientes.Include(c => c.Ciudad)
                .Select(c => new ClienteListDto
            {
                ClienteId = c.ClienteId,
                NombreApellido = c.NombreApellido,
                Ciudad = c.Ciudad.Nombre
            }).ToList();
        }

        public List<ClienteListDto> GetClientes(int provinciaId, int ciudadId)
        {
            try
            {
                return _context.Clientes
                    .Include(c => c.Ciudad)
                    .Where(c => c.CiudadId == ciudadId)
                    .Select(c => new ClienteListDto
                    {
                        ClienteId = c.ClienteId,
                        NombreApellido = c.NombreApellido,
                        Ciudad = c.Ciudad.Nombre
                    }).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Cliente GetClientePorUsuario(string user)
        {
            try
            {
                return _context.Clientes.Include(c => c.Ciudad)
                    .SingleOrDefault(c => c.Email == user);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
