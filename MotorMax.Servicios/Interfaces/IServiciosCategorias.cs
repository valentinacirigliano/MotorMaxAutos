﻿using MotorMax.Entidades.Dto.Categoria;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorMax.Servicios.Interfaces

{
    public interface IServiciosCategorias
    {
        List<CategoriaListDto> GetCategorias();
        void Guardar(Categoria categoria);
        void Borrar(int id);
        bool Existe(Categoria categoria);
        Categoria GetCategoriaPorId(int categoriaId);
        bool EstaRelacionado(Categoria categoria);
        List<Categoria> GetCategoriasPorPagina(int cantidad, int pagina);
        int GetCantidad();
        List<SelectListItem> GetCategoriasDropDownList();
    }
}
