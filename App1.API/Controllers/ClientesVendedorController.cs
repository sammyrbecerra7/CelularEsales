using App1.API.Db;
using Common.Enums;
using Common.Models;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static Common.Enums.ResultEnum;

namespace App1.API.Controllers
{
    [RoutePrefix("api/ClienteVendedor")]
    public class ClientesVendedorController : ApiController
    {
        private Model1 db = new Model1();

       
        [Route("ObtenerClientePorVendedor")]
        [HttpPost]
        public async Task<Response> ObtenerCliente(Vendedor vendedor)
        {

            try
            {
                if (string.IsNullOrEmpty(vendedor?.Correo))
                {

                    var Clientes =await db.Cliente.Where(x => x.Vendedor.Correo == vendedor.Correo)
                        .Select(x => new Cliente
                        {
                            NombreCompleto = x.NombreCompleto,
                            Codigo = x.Codigo,
                            CreditoDisponible = x.CreditoDisponible,
                            CreditoLimite=x.CreditoLimite,
                             VendedorCodigo=x.VendedorCodigo,
                             Garantia=x.Garantia,
                             RUC=x.RUC,
                             TotalFacturado=x.TotalFacturado,
                             TotalVencido=x.TotalVencido,
                             Documentos=x.Documentos,
                             ObjetivoCobro=x.ObjetivoCobro,
                             EntregasAbiertas=x.EntregasAbiertas,
                             TotalChequesPosfechados=x.TotalChequesPosfechados,
                             OrdenesAbiertas=x.OrdenesAbiertas,
                             
                        }).ToListAsync();

                    return new Response {IsSuccess=true,Message= Mensaje.OK,Result=Clientes };
                }

                return new Response { IsSuccess = false, Message = Mensaje.CorreoNoEncontrado,Result = EnumRetult.EMPTY };
            }
            catch (Exception)
            {
                return new Response { IsSuccess = false, Message = Mensaje.ErrorAlConsultar, Result = EnumRetult.EXCEPTION };
                throw;
            }
        }
    }
}
