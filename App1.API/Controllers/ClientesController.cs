using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using App1.API.Db;
using Common.Models;
using Common.Utils;
using static Common.Enums.ResultEnum;

namespace App1.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Clientes")]
    public class ClientesController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/Clientes
        public IQueryable<Cliente> GetCliente()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Cliente.Include(x=>x.Documentos);
        }


        [HttpPost]
        [Route("ObtenerInfoCredito")]
        public async Task<Response> ObtenerInfoCredito(Vendedor vendedor)
        {

            try
            {
                if (!string.IsNullOrEmpty(vendedor?.Codigo))
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    var infoCredito = await db.InfoCredito.Where(x => x.Vendedor.Correo == vendedor.Correo)
                                            .Select(x => new InfoCreditoResponse
                                            {
                                                Codigo = x.Codigo,
                                                VendedorCodigo = x.VendedorCodigo,
                                                Anio = x.Anio,
                                                Mes = x.Mes,
                                                CreditoLimite = x.CreditoLimite,
                                                CreditoActual = x.CreditoActual,
                                                ObjetivoCobro = x.ObjetivoCobro
                                            }).ToListAsync();

                    return new Response { IsSuccess = true, Message = Mensaje.OK, Result = infoCredito };
                }

                return new Response { IsSuccess = false, Message = Mensaje.CorreoNoEncontrado, Result = EnumRetult.EMPTY };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.ErrorAlConsultar, Result = EnumRetult.EXCEPTION };
                throw;
            }
        }

        [HttpPost]
        [Route("InsertarPagos")]
        public async Task<Response> InsertarPagos(List<Pagos> listadopagos)
        {
            if (listadopagos != null)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        db.Pagos.AddRange(listadopagos);
                        await db.SaveChangesAsync();
                        transaction.Commit();
                        return new Response { IsSuccess = true, Message = Mensaje.OK, Result = null };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new Response { IsSuccess = false, Message = Mensaje.ErrorAlConsultar, Result = EnumRetult.EXCEPTION };
                        throw;
                    }
                }
            }
            return new Response { IsSuccess = false, Message = Mensaje.CorreoNoEncontrado, Result = EnumRetult.EMPTY };
        }

        [HttpPost]
        [Route("ObtenerFacturasPorCliente")]
        public async Task<Response> ObtenerFactura(List<Cliente>  listadoclientes)
        {
            try
            {
                if (listadoclientes != null)
                {
                    List<DocumentoResponse> documentos = new List<DocumentoResponse>();
                    foreach (Cliente cliente in listadoclientes)
                    {
                        if (!string.IsNullOrEmpty(cliente?.Codigo))
                        {
                            db.Configuration.ProxyCreationEnabled = false;
                            var facturas = await db.Documentos.Where(x => x.ClienteCodigo == cliente.Codigo)
                                                    .Select(x => new DocumentoResponse
                                                    {
                                                        Codigo = x.Codigo,
                                                        ClienteCodigo = x.ClienteCodigo,
                                                        SpecialGLIndicator = x.SpecialGLIndicator,
                                                        FacturaNumeroLegal = x.FacturaNumeroLegal,
                                                        FechaDocumento = x.FechaDocumento,
                                                        Referencia = x.Referencia,
                                                        TipoDocumento = x.TipoDocumento,
                                                        ValorMonedaLocal = x.ValorMonedaLocal,
                                                        Valor = x.Valor,
                                                        Texto = x.Texto,
                                                        PaymentTerm = x.PaymentTerm,
                                                        NumeroDiasAVencer = x.NumeroDiasAVencer,
                                                        ValorNeto = x.ValorNeto,
                                                        NombreCorto = x.NombreCorto,
                                                        EbillingDocument = x.EbillingDocument
                                                    }).ToListAsync();
                            documentos.AddRange(facturas);
                        }
                    }
                    return new Response { IsSuccess = true, Message = Mensaje.OK, Result = documentos };
                }
                
                return new Response { IsSuccess = false, Message = Mensaje.CorreoNoEncontrado, Result = EnumRetult.EMPTY };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.ErrorAlConsultar, Result = EnumRetult.EXCEPTION };
                throw;
            }
        }

        [HttpPost]
        [Route("ObtenerClientePorVendedor")]
        public async Task<Response> ObtenerCliente(Vendedor vendedor)
        {

            try
            {
                if (!string.IsNullOrEmpty(vendedor?.Correo))
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    var Clientes = await db.Cliente.Where(x => x.Vendedor.Correo == vendedor.Correo)
                                            .Select(x => new ClienteResponse
                                            {
                                                NombreCompleto = x.NombreCompleto,
                                                Codigo = x.Codigo,
                                                CreditoDisponible = x.CreditoDisponible,
                                                CreditoLimite = x.CreditoLimite,
                                                VendedorCodigo = x.VendedorCodigo,
                                                Garantia = x.Garantia,
                                                RUC = x.RUC,
                                                TotalFacturado = x.TotalFacturado,
                                                TotalVencido = x.TotalVencido,
                                                Documentos = x.Documentos,
                                                ObjetivoCobro = x.ObjetivoCobro,
                                                EntregasAbiertas = x.EntregasAbiertas,
                                                TotalChequesPosfechados = x.TotalChequesPosfechados,
                                                OrdenesAbiertas = x.OrdenesAbiertas,
                                            }).ToListAsync();

                    return new Response { IsSuccess = true, Message = Mensaje.OK, Result = Clientes };
                }

                return new Response { IsSuccess = false, Message = Mensaje.CorreoNoEncontrado, Result = EnumRetult.EMPTY };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = Mensaje.ErrorAlConsultar, Result = EnumRetult.EXCEPTION };
                throw;
            }
        }
        // GET: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> GetCliente(string id)
        {
            Cliente cliente = await db.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCliente(string id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.Codigo)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Clientes
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> PostCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cliente.Add(cliente);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(cliente.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cliente.Codigo }, cliente);
        }

        // DELETE: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> DeleteCliente(string id)
        {
            Cliente cliente = await db.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.Cliente.Remove(cliente);
            await db.SaveChangesAsync();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(string id)
        {
            return db.Cliente.Count(e => e.Codigo == id) > 0;
        }
    }
}