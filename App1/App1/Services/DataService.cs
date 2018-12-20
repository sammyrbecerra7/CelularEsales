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
                await connection.CreateTableAsync<EstadoCuentaSqLite>().ConfigureAwait(false);
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
            var list = array.Select(c => new ClienteSqLite
            {
                Codigo = c.Codigo,
                NombreCompleto = c.NombreCompleto,
                VendedorCodigo = c.VendedorCodigo,
                Limite = c.Limite,
                Garantia = c.Garantia,
                TotalVencido = c.TotalVencido,
                TotalAdeudado = c.TotalAdeudado,
                UltimaFechaActualizacion = c.UltimaFechaActualizacion,
                ObjetivoCobro = c.ObjetivoCobro
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
                NombreVendedor = c.NombreVendedor,
                VendedorCodigo = c.VendedorCodigo,
                Anio = c.Anio,
                Mes = c.Mes,
                CreditoActual = c.CreditoActual,
                CreditoLimite = c.CreditoLimite,
                ObjetivoCobro = c.ObjetivoCobro
            }).FirstOrDefault();
            return info;
        }

        public async Task<List<EstadoCuentaSqLite>> ListarFacturas()
        {
            var query = await this.connection.QueryAsync<EstadoCuentaSqLite>("select * from [EstadoCuentaSqLite]");
            var array = query.ToArray();
            var list = array.Select(c => new EstadoCuentaSqLite
            {
                Codigo = c.Codigo,
                ClienteCodigo = c.ClienteCodigo,
                NombreCorto = c.NombreCorto,
                Tipo = c.Tipo,
                ValorTotal = c.ValorTotal,
                ValorRetencion = c.ValorRetencion,
                FechaVencimiento = c.FechaVencimiento
            }).ToList();
            return list;
        }

        public async Task EliminarTodosClientes()
        {
            var query = await this.connection.QueryAsync<ClienteSqLite>("delete from [ClienteSqLite]");
        }

        public async Task EliminarTodosEstadoCuenta()
        {
            var query = await this.connection.QueryAsync<EstadoCuentaSqLite>("delete from [EstadoCuentaSqLite]");
        }
    }
}
