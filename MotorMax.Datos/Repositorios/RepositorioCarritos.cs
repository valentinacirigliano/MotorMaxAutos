using MotorMax.Datos.Interfaces;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos.Repositorios
{
    public class RepositorioCarritos : IRepositorioCarritos
    {
        private readonly AutosDbContext _context;

        public RepositorioCarritos(AutosDbContext context)
        {
            _context = context;
        }

        public void Guardar(ItemCarrito carritoTemp)
        {
            var itemInDb = GetItem(carritoTemp.UserName,
                carritoTemp.RepuestoId);

            if (itemInDb != null)
            {
                itemInDb.Cantidad += carritoTemp.Cantidad;
                _context.Entry(itemInDb).State = EntityState.Modified;

            }
            else
            {
                _context.Carrito.Add(carritoTemp);

            }

        }


        public void Borrar(string user, int productoId)
        {
            var itemInDb = GetItem(user, productoId);
            if (itemInDb != null)
            {
                _context.Entry(itemInDb).State = EntityState.Deleted;
            }
        }


        public int GetCantidad(string user)
        {
            return _context.Carrito.Count(c => c.UserName == user);
        }

        public List<ItemCarrito> GetCarrito(string user)
        {
            return _context.Carrito
               .Where(c => c.UserName == user).ToList();
        }

        public ItemCarrito GetItem(string user, int productoId)
        {
            return _context.Carrito
                        .SingleOrDefault(i => i.UserName == user && i.RepuestoId == productoId);

        }
    }
}
