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