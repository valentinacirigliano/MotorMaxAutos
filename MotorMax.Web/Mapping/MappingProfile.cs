using AutoMapper;
using MotorMax.Entidades.Dto.Auto;
using MotorMax.Entidades.Dto.Ciudad;
using MotorMax.Entidades.Dto.Cliente;
using MotorMax.Entidades.Dto.DetallesVenta;
using MotorMax.Entidades.Dto.Proveedor;
using MotorMax.Entidades.Dto.Repuesto;
using MotorMax.Entidades.Dto.Venta;
using MotorMax.Entidades.Entidades;
using MotorMax.Web.ViewModels.Auto;
using MotorMax.Web.ViewModels.Carrito;
using MotorMax.Web.ViewModels.Ciudad;
using MotorMax.Web.ViewModels.Cliente;
using MotorMax.Web.ViewModels.DetalleVenta;
using MotorMax.Web.ViewModels.Marca;
using MotorMax.Web.ViewModels.Proveedor;
using MotorMax.Web.ViewModels.Provincia;
using MotorMax.Web.ViewModels.Repuesto;
using MotorMax.Web.ViewModels.RepuestoProveedor;
using MotorMax.Web.ViewModels.Venta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MotorMax.Web.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            LoadMarcasMapping();
            LoadCiudadesMapping();
            LoadProvinciaMapping();
            LoadClientesMapping();
            LoadAutosMapping();
            LoadRepuestosMapping();
            LoadCarritoMapping();
            LoadDetalleVentaMapping();
            LoadVentasMapping();
            LoadProveedoresMapping();
            LoadRepuestosProveedores();
        }

        private void LoadRepuestosProveedores()
        {
            CreateMap<RepuestoProveedor, RepuestoProveedorEditVm>().ReverseMap();
        }

        private void LoadProveedoresMapping()
        {
            CreateMap<Proveedor, ProveedorListVm>().ReverseMap();
            CreateMap<Proveedor,ProveedorEditVm>().ReverseMap();
            CreateMap<ProveedorListDto, ProveedorListVm>();
        }

        private void LoadCarritoMapping()
        {
            CreateMap<ItemCarrito, ItemCarritoVm>();
        }

        private void LoadDetalleVentaMapping()
        {
            CreateMap<DetalleVentaListDto, DetalleVentaListVm>();
        }

        private void LoadVentasMapping()
        {
            CreateMap<VentaListDto, VentaListVm>();
            CreateMap<Venta, VentaListVm>().ReverseMap();
        }

        private void LoadRepuestosMapping()
        {
            CreateMap<RepuestoListDto, RepuestoListVm>();
            CreateMap<RepuestoEditVm, Repuesto>().ReverseMap();
            CreateMap<Repuesto, RepuestoListVm>()
                .ForMember(dest => dest.Categoria,
                opt => opt.MapFrom(src => src.Categoria.NombreCategoria));
        }

        private void LoadAutosMapping()
        {
            CreateMap<AutoListDto, AutoListVm>();
            CreateMap<AutoEditVm, Auto>().ReverseMap();
            CreateMap<Auto, AutoListVm>()
                .ForMember(dest => dest.Marca,
                opt => opt.MapFrom(src => src.Marca.NombreMarca));
        }

        private void LoadClientesMapping()
        {
            CreateMap<ClienteListDto, ClienteListVm>();
            CreateMap<Cliente, ClienteEditVm>();
            CreateMap<ClienteEditVm, Cliente>();
            CreateMap<Cliente, ClienteListVm>()
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.NombreApellido, opt => opt.MapFrom(src => src.NombreApellido))
                .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.Ciudad.Nombre));
        }

        private void LoadMarcasMapping()
        {
            CreateMap<Marca, MarcaListVm>();
            CreateMap<Marca, MarcaEditVm>().ReverseMap();
        }
        

        private void LoadCiudadesMapping()
        {
            CreateMap<CiudadListDto, CiudadListVm>();
            CreateMap<CiudadEditVm, Ciudad>().ReverseMap();
            CreateMap<Ciudad, CiudadListVm>()
               .ForMember(dest => dest.NombreProvincia,
               opt => opt.MapFrom(src => src.Provincia.Nombre));
        }

        private void LoadProvinciaMapping()
        {
            CreateMap<Provincia, ProvinciaListVm>();
            CreateMap<Provincia, ProvinciaEditVm>().ReverseMap();

        }
    }
}