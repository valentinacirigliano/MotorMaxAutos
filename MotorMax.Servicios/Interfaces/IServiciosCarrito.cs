using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Servicios.Interfaces
{
    public interface IServiciosCarrito
    {
        List<ItemCarrito> GetCarrito(string user);
        void Guardar(ItemCarrito carritoTemp);
        void Borrar(string user, int productoId);
        ItemCarrito GetItem(string user, int productoId);
        int GetCantidad(string user);
    }
}
