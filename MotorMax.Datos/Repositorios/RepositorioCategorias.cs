using MotorMax.Datos.Interfaces;
using MotorMax.Entidades.Dto.Categoria;
using MotorMax.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorMax.Datos.Repositorios
{
    public class RepositorioCategorias: IRepositorioCategorias
    {
        private readonly AutosDbContext _context;

        public RepositorioCategorias(AutosDbContext context)
        {
            _context = context;
        }


        public void Agregar(Categoria categoria)
        {

            _context.Categorias.Add(categoria);


        }

        public void Borrar(int id)
        {
            try
            {
                var categoriaInDb = _context.Categorias.SingleOrDefault(p => p.CategoriaId == id);
                if (categoriaInDb == null)
                {
                    Exception ex = new Exception("Borrado por otro usuario");
                    throw ex;
                }
                _context.Entry(categoriaInDb).State = EntityState.Deleted;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(Categoria categoria)
        {
            try
            {
                var categoriaInDb = _context.Categorias.SingleOrDefault(c => c.CategoriaId == categoria.CategoriaId);
                if (categoriaInDb == null)
                {
                    throw new Exception("Borrado por otro usuario");
                }
                categoriaInDb.NombreCategoria = categoria.NombreCategoria;
                _context.Entry(categoriaInDb).State = EntityState.Modified;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionado(Categoria categoria)
        {
            try
            {
                return _context.Repuestos.Any(c => c.CategoriaId == categoria.CategoriaId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Categoria categoria)
        {
            try
            {
                if (categoria.CategoriaId == 0)
                {
                    return _context.Categorias.Any(p => p.NombreCategoria == categoria.NombreCategoria);
                }
                return _context.Categorias.Any(p => p.NombreCategoria == categoria.NombreCategoria && p.CategoriaId != categoria.CategoriaId);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad()
        {
            return _context.Categorias.Count();
        }

        public List<CategoriaListDto> GetCategorias()
        {
            try
            {
                return _context.Categorias
                    .OrderBy(c => c.NombreCategoria)
                    .Select(c => new CategoriaListDto
                    {
                        CategoriaId = c.CategoriaId,
                        NombreCategoria = c.NombreCategoria,
                    })
                    .ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Categoria> GetCategoriasPorPagina(int cantidad, int pagina)
        {
            return _context.Categorias.OrderBy(p => p.NombreCategoria)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .ToList();
        }

        public Categoria GetCategoriaPorId(int categoriaId)
        {
            try
            {
                var categoriaInDb = _context.Categorias
                    .SingleOrDefault(p => p.CategoriaId == categoriaId);
                return categoriaInDb;


            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetCategoriasDropDownList()
        {
            var lista = GetCategorias();
            var dropDown = lista.Select(c => new SelectListItem
            {
                Text = c.NombreCategoria,
                Value = c.CategoriaId.ToString()
            }).ToList();
            return dropDown;
        }

    }
}
