 using AutoMapper;
using MotorMax.Entidades.Entidades;
using MotorMax.Servicios.Interfaces;
using MotorMax.Web.App_Start;
using MotorMax.Web.Helpers;
using MotorMax.Web.ViewModels.Cliente;
using PagedList;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MotorMax.Web.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IServiciosClientes _servicios;
        private readonly IServiciosCiudades _serviciosCiudades;
        private readonly IServiciosProvincias _serviciosProvincias;
        private readonly IMapper _mapper;
        public ClientesController(IServiciosClientes servicios, IServiciosCiudades serviciosCiudades,
            IServiciosProvincias serviciosProvincias)
        {
            _servicios = servicios;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosCiudades = serviciosCiudades;
            _serviciosProvincias= serviciosProvincias;
        }
        // GET: Clientes
        public ActionResult Index(int? page, int? pageSize, string SortBy = "Cliente", string SearchBy = null)
        {
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            var lista = _servicios.GetClientes();
            if (SearchBy != null)
            {
                lista = lista
                    .Where(c => c.NombreApellido.Contains(SearchBy) || c.Ciudad.Contains(SearchBy))
                    .ToList();
            }

            var listaVm = _mapper.Map<List<ClienteListVm>>(lista);
            if (SortBy == "Cliente")
            {
                listaVm = listaVm.OrderBy(c => c.NombreApellido).ToList();
            }
            else
            {
                listaVm = listaVm.OrderBy(c => c.Ciudad).ThenBy(c => c.Ciudad).ToList();
            }
            var clienteVm = new ClienteListSortVm
            {
                Clientes = listaVm.ToPagedList(page.Value, pageSize.Value),
                Sorts = new Dictionary<string, string> {
                    {"Por Cliente","Cliente"},
                    {"Por Ciudad","Ciudad" }
                },
                SortBy = SortBy,
                SearchBy = SearchBy
            };

            return View(clienteVm);
        }


        public ActionResult Create()
        {
            var clienteVm = new ClienteEditVm
            {
                Ciudades = _serviciosCiudades.GetCiudadesDropDownList()

            };
            return View(clienteVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteEditVm clienteVm)
        {

            if (!ModelState.IsValid)
            {
                clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList();
                return View(clienteVm);
            }
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteVm);
                if (!_servicios.Existe(cliente))
                {
                    try
                    {
                        _servicios.Guardar(cliente);
                        UsersHelper.CreateUserAsp(cliente.Email, "Comprador");
                        TempData["Msg"] = "Registro agregado satisfactoriamente";
                        return RedirectToAction("Index");
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Cliente existente!");
                    clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(clienteVm.CiudadId);

                    return View(clienteVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intentar agregar un Cliente");
                clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(clienteVm.CiudadId);

                return View(clienteVm);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var cliente = _servicios.GetClientePorId(id.Value);
            if (cliente == null)
            {
                return HttpNotFound("Cód. de cliente inexistente!!!");
            }
            var clienteVm = _mapper.Map<ClienteEditVm>(cliente);
            clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(cliente.CiudadId.Value);
            return View(clienteVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteEditVm clienteVm)
        {
            if (!ModelState.IsValid)
            {
                clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(clienteVm.CiudadId);
                return View(clienteVm);

            }
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteVm);
                if (!_servicios.Existe(cliente))
                {
                    try
                    {
                        _servicios.Guardar(cliente);
                    }
                    catch (System.Exception)
                    {
                        ModelState.AddModelError(string.Empty, "Error al editar!!");
                        throw;
                    }
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Cliente existente!!!");
                    clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(clienteVm.CiudadId);
                    return View(clienteVm);

                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "Cliente existente!!!");
                clienteVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(clienteVm.CiudadId);
                return View(clienteVm);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var cliente = _servicios.GetClientePorId(id.Value);
            if (cliente == null)
            {
                return HttpNotFound("Cód. cliente inexistente!!!");
            }
            var clienteVm = _mapper.Map<ClienteListVm>(cliente);
            return View(clienteVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var cliente = _servicios.GetClientePorId(id);
            var clienteVm = _mapper.Map<ClienteListVm>(cliente);
            try
            {
                if (!_servicios.EstaRelacionado(cliente))
                {
                    _servicios.Borrar(id);
                    TempData["Msg"] = "Cliente borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registro relacionado... Baja denegada");
                    return View(clienteVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intengar borrar un cliente");
                return View(clienteVm);

            }
        }

    }
}