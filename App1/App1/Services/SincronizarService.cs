using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Domain.ModelsResult;
using App1.Domain.Utils;
using Common.Models;
using Common.Utils;

namespace App1.Services
{
   public   class SincronizarService
    {

        public   async Task<bool> Sincronizar()
        {
            var a = await App.apiService.CheckConnection();
            try
            {
                if (a.IsSuccess)
                {
                
                    await this.EliminarTodosClientes();
                    await this.CargarClientes();
                    await this.InsertarTodosClientes();

                    await this.EliminarTodosDocumentos();
                    await this.CargarDocumentos();
                    await this.InsertarTodosDocumentos();

                    await this.EliminarInformacionCredito();
                    await this.InsertarInformacionCredito();
                    return true;
                }
               
            }
             catch (System.Exception ex)
            {
                return false;
            }
            return false;
        }
        private   async Task<bool> EliminarInformacionCredito()
        {
            await App.dataService.EliminarInformacionCredito();
            return true;
        }
        private   async Task<bool>  InsertarInformacionCredito()
        {

            var response = await App.apiService.Post<List<InfoCredito>>(Global.UrlBase, Global.RoutePrefix, Global.ObtenerInfoCredito, Settings.TokenType, Settings.AccessToken, new Vendedor { Correo = Settings.UserASP });

            if (!response.IsSuccess)
            {
                return false;
               
            }

            var infocredito = (List<InfoCredito>)response.Result;
            var lista = infocredito.Select(c => new InfoCreditoSqLite
            {
                Codigo = c.Codigo,
                VendedorCodigo = c.VendedorCodigo,
                Anio = c.Anio,
                Mes = c.Mes,
                CreditoActual = c.CreditoActual,
                CreditoLimite = c.CreditoLimite,
                ObjetivoCobro = c.ObjetivoCobro

            }).ToList();

            await App.dataService.Insert(lista);
            App.InfoCreditoSqLite = lista.FirstOrDefault();
            return true;
        }
        private   async Task<bool> InsertarTodosDocumentos()
        {
            var lista = App.ListaDocumentos.Select(x => new DocumentosSqLite
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
            App.ListaDocumentoSqLite = lista;
            await App.dataService.Insert(App.ListaDocumentoSqLite);
            return true;
        }
        private   async Task<bool> CargarDocumentos()
        {
            var response = await App.apiService.Post<List<Documentos>>(Global.UrlBase, Global.RoutePrefix, Global.ListarDocumentos, Settings.TokenType, Settings.AccessToken, App.ListaClienteSqLite);

            if (!response.IsSuccess)
            {
                return false;
            }
            App.ListaDocumentos = (List<Documentos>)response.Result;
            return true;

        }
        private   async Task<bool> InsertarTodosClientes()
        {
            var lista = App.ListaClientes.Select(x => new ClienteSqLite
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
            App.ListaClienteSqLite = lista;
            await App.dataService.Insert(App.ListaClienteSqLite);
            return true;
        }
        private   async Task<bool> EliminarTodosClientes()
        {
            await App.dataService.EliminarTodosClientes();
            return true;
        }
        private   async Task EliminarTodosDocumentos()
        {
            await App.dataService.EliminarTodosEstadoCuenta();
        }
        private   async Task<bool> CargarClientes()
        {
            var response = await App.apiService.Post<List<Cliente>>(Global.UrlBase, Global.RoutePrefix, Global.ObtenerClientePorVendedor, Settings.TokenType, Settings.AccessToken, new Vendedor { Correo = Settings.UserASP });

            if (!response.IsSuccess)
            {

               
                return false;
            }

            App.ListaClientes = (List<Cliente>)response.Result;
            return true;

        }
    }
}
