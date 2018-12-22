using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Domain.Utils
{
   public static class Global
    {
        //public static string UrlBase { get {  return "https://demoidealapi.azurewebsites.net"; } set {; } }
        //public static string RoutePrefix { get { return "/Api"; } set {; } }

        //public static string UrlBase { get { return "http://esalesideal.bekaert.com:9005"; } set {; } }

        public static string UrlBase { get { return "http://192.168.6.177"; } set {; } }
        public static string RoutePrefix { get { return "/movilApi/Api"; } set {; } }

        public static string ListarClientes { get { return "/Cliente/ListaCliente"; } set {; } }

        public static string ObtenerClientePorVendedor { get { return "/Clientes/ObtenerClientePorVendedor"; } set {; } }

        public static string ListarDocumentos { get { return "/Clientes/ObtenerFacturasPorCliente"; } set {; } }

        public static string ObtenerInfoCredito { get { return "/Clientes/ObtenerInfoCredito"; } set {; } }

        public static string InsertarPagos { get { return "/Clientes/InsertarPagos"; } set {; } }
    }
}
