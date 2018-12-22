using System;

namespace App1.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using App1.Domain.ModelsResult;
    using Common.Models;
    using Interfaces;
    using SQLite;
    using Xamarin.Forms;

    public class DataService
    {
        private SQLiteAsyncConnection connection;

        public DataService()
        {
            this.OpenOrCreateDB().GetAwaiter();
        }

        private async Task OpenOrCreateDB()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
            this.connection = new SQLiteAsyncConnection(databasePath);

            try
            {
                await connection.CreateTableAsync<VendedorSqLite>().ConfigureAwait(false);
                await connection.CreateTableAsync<ClienteSqLite>().ConfigureAwait(false);
                await connection.CreateTableAsync<DocumentosSqLite>().ConfigureAwait(false);
                await connection.CreateTableAsync<PagosSqLite>().ConfigureAwait(false);
                await connection.CreateTableAsync<FormaPagoSqLite>().ConfigureAwait(false);
                await connection.CreateTableAsync<ContrapartidaSqLite>().ConfigureAwait(false);
                await connection.CreateTableAsync<InfoCreditoSqLite>().ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task Insert<T>(T model)
        {
            await this.connection.InsertAsync(model);
        }

        public async Task Insert<T>(List<T> models)
        {
            await this.connection.InsertAllAsync(models);
        }

        public async Task Update<T>(T model)
        {
            await this.connection.UpdateAsync(model);
        }

        public async Task Update<T>(List<T> models)
        {
            await this.connection.UpdateAllAsync(models);
        }

        public async Task Delete<T>(T model)
        {
            await this.connection.DeleteAsync(model);
        }

        public async Task<List<ClienteSqLite>> ListarClientes()
        {
            var query = await this.connection.QueryAsync<ClienteSqLite>("select * from [ClienteSqLite]");
            var array = query.ToArray();
            var list = array.Select(x => new ClienteSqLite
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
                ObjetivoCobro = x.ObjetivoCobro,
                EntregasAbiertas = x.EntregasAbiertas,
                TotalChequesPosfechados = x.TotalChequesPosfechados,
                OrdenesAbiertas = x.OrdenesAbiertas,

            }).ToList();
            return list;
        }


       

        public async Task<InfoCreditoSqLite> ObtenerInformacionCredito()
        {
            var query = await this.connection.QueryAsync<InfoCreditoSqLite>("select * from [InfoCreditoSqLite]");
            var array = query.ToArray();
            var info = array.Select(c => new InfoCreditoSqLite
            {
                Codigo = c.Codigo,
                VendedorCodigo = c.VendedorCodigo,
                Anio = c.Anio,
                Mes = c.Mes,
                CreditoActual = c.CreditoActual,
                CreditoLimite = c.CreditoLimite,
                ObjetivoCobro = c.ObjetivoCobro
            }).FirstOrDefault();
            return info;
        }

        public async Task<List<DocumentosSqLite>> ListarDocumentos()
        {
            var query = await this.connection.QueryAsync<DocumentosSqLite>("select * from [DocumentosSqLite]");
            var array = query.ToArray();
            var list = array.Select(x => new DocumentosSqLite
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

            }).ToList();
            return list;
        }

        public async Task<List<DocumentosSqLite>> ListarDocumentosRelacionados(DocumentosSqLite documentosSqLite)
        {
            var query = await this.connection.QueryAsync<DocumentosSqLite>("select * from [DocumentosSqLite] " );
            var array = query.ToArray();
            var list = array.Select(x => new DocumentosSqLite
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

            }).ToList();
            return list;
        }

        public async Task EliminarTodosClientes()
        {
            var query = await this.connection.QueryAsync<ClienteSqLite>("delete from [ClienteSqLite]");
        }

        public async Task EliminarTodosEstadoCuenta()
        {
            var query = await this.connection.QueryAsync<DocumentosSqLite>("delete from [DocumentosSqLite]");
        }

        public async Task EliminarInformacionCredito()
        {
            var query = await this.connection.QueryAsync<InfoCreditoSqLite>("delete from [InfoCreditoSqLite]");
        }
    }
}
