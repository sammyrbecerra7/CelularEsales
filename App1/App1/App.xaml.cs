using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using App1.Domain.ModelsResult;
using App1.Domain.Utils;
using App1.Services;
using App1.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using Common;
using Common.Models;
using App1.Data;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App1
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public static DataService dataService { get; set; }

        public static ApiService apiService { get; set; }

        public static SincronizarService SincronizarService { get; set; }

        public static List<ClienteSqLite> ListaClienteSqLite { get; set; }

        public static InfoCreditoSqLite InfoCreditoSqLite { get; set; }

        public static List<DocumentosSqLite> ListaDocumentoSqLite { get; set; }

        public static List<Cliente> ListaClientes { get; set; }

        public static List<Documentos> ListaDocumentos { get; set; }

        public static LoginPage LoginPage { get; set; }

        public static MenuPage MenuPage { get; set; }
        public static MasterPage Master { get; internal set; }

        //public static XamSharpServiceManager serviceManager { get; set; }

        public  App()
        {
            InitializeComponent();
            ListaClienteSqLite = new List<ClienteSqLite>();
            LoginPage = new LoginPage();
            dataService = new DataService();
            apiService = new ApiService();
            SincronizarService = new SincronizarService();
            ListaClientes = new List<Cliente>();
            ListaDocumentos = new List<Documentos>();
            ListaDocumentoSqLite = new List<DocumentosSqLite>();
            InfoCreditoSqLite = new InfoCreditoSqLite();
            Application.Current.MainPage = new MasterPage();

            //descomentar todo el bloquesiguiente para entrar con el login SB
            if (Settings.IsRemembered && !string.IsNullOrEmpty(Settings.UserASP))
            {
                Task.Run(() => this.CargarClientes()).Wait();
                Task.Run(() => this.CargarFacturas()).Wait();
                Task.Run(() => this.CargarInfoCredito()).Wait();


                Application.Current.MainPage = new MasterPage();

            }
            else
            {
                MainPage = new NavigationPage(LoginPage);
            }

            //serviceManager = new XamSharpServiceManager(new SOAPXamSharpService());

        }

        

        public async Task Sincronizar()
        {
            

            var a = await apiService.CheckConnection();
            if (a.IsSuccess)
            {
                await this.EliminarTodosClientes();
                await this.CargarClientes();
                await this.InsertarTodosClientes();

                await this.EliminarTodosFactura();
                await this.CargarFacturas();
                await this.InsertarTodosEstadoCuenta();

            }
        }


        private async Task CargarInfoCredito()
        {
            App.InfoCreditoSqLite =await App.dataService.ObtenerInformacionCredito();
        }

        private async Task InsertarTodosClientes()
        {
            var lista = ListaClientes.Select(x => new ClienteSqLite
            {
                Codigo = x.Codigo,
                NombreCompleto = x.NombreCompleto,
                VendedorCodigo = x.VendedorCodigo,
                CreditoLimite = x.CreditoLimite,
                Garantia = x.Garantia,
                TotalVencido = x.TotalVencido,
                TotalFacturado = x.TotalFacturado,
                UltimaFechaActualizacion = x.UltimaFechaActualizacion
            }).ToList();
            ListaClienteSqLite = lista;
           
            await dataService.Insert(ListaClienteSqLite);
        }

        private async Task InsertarTodosEstadoCuenta()
        {
            //var lista = ListaFacturas.Select(x => new DocumentosSqLite { Codigo = x.Codigo, ClienteCodigo = x.ClienteCodigo, NombreCorto = x.NombreCorto,Tipo = x.TipoDocumento, ValorTotal = x.Valor }).ToList();
            //ListaFacturaSqLite = lista;
            //await dataService.Insert(ListaFacturaSqLite);
        }

        private async Task EliminarTodosClientes()
        {
            await dataService.EliminarTodosClientes();
        }

        private async Task EliminarTodosFactura()
        {
            await dataService.EliminarTodosEstadoCuenta();
        }

        private async Task CargarClientes()
        {
            App.ListaClienteSqLite = await App.dataService.ListarClientes();
        }


        private async Task CargarFacturas()
        {
           
           App.ListaDocumentoSqLite= await App.dataService.ListarDocumentos();

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
