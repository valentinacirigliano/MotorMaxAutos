using MotorMax.Entidades.Dto.Venta;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;

namespace MotorMax.Datos.Interfaces
{
    public interface IRepositorioVentas
    {
        List<VentaListDto> GetVentas();
        void Agregar(Venta venta);
        List<VentaListDto> Filtrar(Func<Venta, bool> predicado);
        Venta GetVentaPorId(int id);
        int GetCantidad();
        VentaListDto GetVentaListDtoPorId(int id);
    }

}
