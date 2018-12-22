using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Domain.ModelsResult;
using App1.Domain.Utils;
using App1.Helpers;
using App1.Services;
using App1.Views;
using Common.Models;
using Common.Utils;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class LoginViewModel : PropertyChangedClass
    {

        #region Servicios

        private ApiService apiService;

        #endregion


        #region Atributos

        private string contrasena ;
        
        private bool isRunning ;
        
        private bool isEnabled;

        private bool isVisible;

        #endregion

        #region Propiedades


        public string Email { get; set; }
        public string Contrasena
        {
            get { return this.contrasena; }
            set { SetValue(ref this.contrasena, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool Recuerdame { get; set; }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsVisible
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }

        private string mensajeCargando;
        public string MensajeCargando
        {
            get { return this.mensajeCargando; }
            set { SetValue(ref this.mensajeCargando, value); }
        }

        #endregion

        #region Constructores

        public LoginViewModel()
        {
            apiService = new ApiService();
            Email = string.Empty;
            Contrasena =string.Empty;
            Recuerdame = true;
            IsEnabled = true;
            IsVisible = true;


        }
        #endregion


        #region Comandos

        public ICommand LoginCommand { get { return new RelayCommand(Login); } }







        #endregion

        #region Metodos
        private async void Login()
        {
            this.IsRunning = true;
            this.IsEnabled = false;
            IsVisible = false;
            MensajeCargando = Mensaje.Autenticando;

            if (string.IsNullOrEmpty(Email))
            {
                IsVisible = true;
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Aceptar, Languages.ValidacionEmail,Languages.Aceptar);
                return;
            }

            if (string.IsNullOrEmpty(this.Contrasena))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                IsVisible = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar la contraseña", "Aceptar");
                return;
            }

            var conexion = await apiService.CheckConnection();
            if (!conexion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                IsVisible = true;
                await Application.Current.MainPage.DisplayAlert("Error", conexion.Message, "Aceptar");
                return;
            }

            MensajeCargando = Mensaje.Sincronizando;
            var token = await apiService.GetToken(Global.UrlBase, this.Email, this.Contrasena);
            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                IsVisible = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error, intente de nuevo", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                IsVisible = true;
                await Application.Current.MainPage.DisplayAlert("Error", token.ErrorDescription, "Aceptar");
                this.Contrasena = string.Empty;
                return;
            }
            


            Settings.TokenType = token.TokenType;
            Settings.AccessToken = token.AccessToken;
            Settings.UserASP = token.UserName; //aquí guardamos el nombre del asesor
            Settings.IsRemembered = this.Recuerdame;
            Task.Run(() => this.Sincronizar()).Wait();
            this.IsRunning = false;
            this.IsEnabled = true;
            IsVisible = true;
            Application.Current.MainPage = new MasterPage();
            return;
        }

        public async Task Sincronizar()
        {
            var a = await apiService.CheckConnection();
            if (a.IsSuccess)
            {
                try
                {
                    await this.EliminarTodosClientes();
                    await this.CargarClientes();
                    await this.InsertarTodosClientes();

                    await this.EliminarTodosDocumentos();
                    await this.CargarDocumentos();
                    await this.InsertarTodosDocumentos();

                    await this.EliminarInformacionCredito();
                    await this.InsertarInformacionCredito();
                }
                catch (System.Exception ex)
                {
                   IsVisible = true;
                   await App.Master.DisplayAlert("Error", Mensaje.ErrorAlSincornizar, "Aceptar");
                    throw;
                }
            }
        }

        private async Task EliminarInformacionCredito()
        {
            await App.dataService.EliminarInformacionCredito();
        }

        private async Task InsertarInformacionCredito()
        {

            var response = await apiService.Post<List<InfoCredito>>(Global.UrlBase, Global.RoutePrefix, Global.ObtenerInfoCredito, Settings.TokenType, Settings.AccessToken, new Vendedor { Correo=Settings.UserASP} );

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            var infocredito = (List<InfoCredito>) response.Result;
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
        }

        private async Task InsertarTodosDocumentos()
        {
            var lista =App.ListaDocumentos.Select(x => new DocumentosSqLite
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
        }
        private async Task CargarDocumentos()
        {
            var response = await apiService.Post<List<Documentos>>(Global.UrlBase, Global.RoutePrefix, Global.ListarDocumentos, Settings.TokenType, Settings.AccessToken, App.ListaClienteSqLite);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }
            App.ListaDocumentos = (List<Documentos>)response.Result;

        }
        private async Task InsertarTodosClientes()
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
            Settings.SincronizacionCompleta = true;
        }

        private async Task EliminarTodosClientes()
        {
            await App.dataService.EliminarTodosClientes();
        }

        private async Task EliminarTodosDocumentos()
        {
            await App.dataService.EliminarTodosEstadoCuenta();
        }
        private async Task CargarClientes()
        {
            var response = await apiService.Post<List<Cliente>>(Global.UrlBase,Global.RoutePrefix, Global.ObtenerClientePorVendedor,Settings.TokenType,Settings.AccessToken,new Vendedor {Correo=Settings.UserASP });

            if (!response.IsSuccess)
            {
              
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            App.ListaClientes = (List<Cliente>)response.Result;

        }
        #endregion
    }
}
