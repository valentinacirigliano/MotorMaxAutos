using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorMax.Datos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutosDbContext _context;


        public UnitOfWork(AutosDbContext context)
        {
            _context = context;
        }


        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.ToList()
                     .ForEach(entry =>
                     {
                         entry.Reload();

                     });

                throw new Exception("Registro modificado o borrado por otro usuario");


            }
            catch (Exception ex)
            {

                //if (ex.InnerException != null && ex.InnerException.InnerException != null)
                //{
                //    if (ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                //    {
                //        throw new Exception("Registro relacionado\nBaja denegada");
                //    }
                //    else if (ex.InnerException.InnerException.Message.Contains("IX"))
                //    {
                //        throw new Exception("Registro repetido\nAlta o edición denegada");

                //    }
                //    else { throw new Exception(ex.Message); }
                //}
                throw new Exception(ex.Message);
            }
        }
    }
}
