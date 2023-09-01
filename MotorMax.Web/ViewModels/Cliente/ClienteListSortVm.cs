
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorMax.Web.ViewModels.Cliente
{
    public class ClienteListSortVm
    {
        public IPagedList<ClienteListVm> Clientes { get; set; }
        public string SortBy { get; set; }
        public Dictionary<string, string> Sorts { get; set; }
        public string SearchBy { get; set; }
    }
}