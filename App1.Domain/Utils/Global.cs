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

        public static string UrlBase { get { return "http://192.168.6.237"; } set {; } }
        public static string RoutePrefix { get { return "/movilApi"; } set {; } }

        public static string ListarClientes { get { return "/Cliente/ListaCliente"; } set {; } }

        public static string ListarFacturas { get { return "/Factura/ListarFacturaPorListaCliente"; } set {; } }
    }
}
