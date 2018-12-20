using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Domain.ModelsResult;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ClientesViewModel:PropertyChangedClass
    {

        #region Servicios

       
        

        #endregion

        #region Atributos
        private ObservableCollection<ClienteItemViewModel> clientes;
        private bool isRefreshing;
        private string filtro;
        
        #endregion

        #region propiedades
        public ObservableCollection<ClienteItemViewModel> Clientes
        {
            get { return this.clientes; }
            set { SetValue(ref this.clientes, value); }
        }

        public string Filtro
        {
            get { return this.filtro; }
            set
            {
                SetValue(ref this.filtro, value);
                this.Buscar();
            }
        }



        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructores
        public ClientesViewModel()
        {
            this.IsRefreshing = true;
            //para insertar los registros de prueba SB
            Task.Run(() => this.InsertarRegistrosPrueba()).Wait();
            Task.Run(() => this.CargarClientes()).Wait();
            this.IsRefreshing = false;

        }


        #endregion



        #region Comandos

        public ICommand RefreshCommand { get { return new RelayCommand(CargarClientes); } }


        public ICommand BuscarCommand { get { return new RelayCommand(Buscar); } }

        

        #endregion


        #region Metodos

       

        private void Buscar()
        {
            if (string.IsNullOrEmpty(this.Filtro))
            {
                this.Clientes = new ObservableCollection<ClienteItemViewModel>(this.ConvertirClienteItem());
                return;
            }
            this.Clientes = new ObservableCollection<ClienteItemViewModel>(this.ConvertirClienteItem().Where(x=>x.NombreCompleto.ToLower().Contains(this.Filtro.ToLower()) 
                                                                                || x.NombreCompleto.ToLower().Contains(this.Filtro.ToLower())));
            return;
        }

        private async Task InsertarRegistrosPrueba()
        {
            //registro 1
            var registro1 = new ClienteSqLite {
                Codigo = "001",
                NombreCompleto = "ACERO COMERCIAL ECUATORIANO S.A.",
                VendedorCodigo = "Vendedor001",
                Limite = 1001,
                Garantia = 2001,
                TotalVencido = 5001,
                TotalAdeudado = 10001,
                UltimaFechaActualizacion = System.DateTime.Now,
                CreditoDisponible = 1001,
                ObjetivoCobro = 2001,
                RUC = "0101010101",
                TotalChequesPosfechados = 1501,
                OrdenesAbiertas = 1351,
                EntregasAbiertas = 1251
            };
            App.ListaClienteSqLite.Add(registro1);
            //registro 2
            var registro2 = new ClienteSqLite
            {
                Codigo = "002",
                NombreCompleto = "ACOSTA MUNOZ LUIS RODRIGO",
                VendedorCodigo = "Vendedor001",
                Limite = 2002,
                Garantia = 4002,
                TotalVencido = 6002,
                TotalAdeudado = 80002,
                UltimaFechaActualizacion = System.DateTime.Now,
                CreditoDisponible = 1002,
                ObjetivoCobro = 2002,
                RUC = "0101010102",
                TotalChequesPosfechados = 1502,
                OrdenesAbiertas = 1352,
                EntregasAbiertas = 1252
            };
            App.ListaClienteSqLite.Add(registro2);
            //registro 3
            var registro3 = new ClienteSqLite
            {
                Codigo = "003",
                NombreCompleto = "ARMIJOS MACHUCA MARIO EDWITHAR",
                VendedorCodigo = "Vendedor001",
                Limite = 3003,
                Garantia = 6003,
                TotalVencido = 9003,
                TotalAdeudado = 12003,
                UltimaFechaActualizacion = System.DateTime.Now,
                CreditoDisponible = 1003,
                ObjetivoCobro = 2003,
                RUC = "0101010103",
                TotalChequesPosfechados = 1503,
                OrdenesAbiertas = 1353,
                EntregasAbiertas = 1253
            };
            App.ListaClienteSqLite.Add(registro3);

            var registro4 = new ClienteSqLite
            {
                Codigo = "004",
                NombreCompleto = "CABEZAS ORELLANA PATRICIO GERARDO",
                VendedorCodigo = "Vendedor001",
                Limite = 3004,
                Garantia = 6004,
                TotalVencido = 9004,
                TotalAdeudado = 12004,
                UltimaFechaActualizacion = System.DateTime.Now,
                CreditoDisponible = 1004,
                ObjetivoCobro = 2004,
                RUC = "0101010104",
                TotalChequesPosfechados = 1504,
                OrdenesAbiertas = 1354,
                EntregasAbiertas = 1254
            };
            App.ListaClienteSqLite.Add(registro4);

            var registro5 = new ClienteSqLite
            {
                Codigo = "005",
                NombreCompleto = "CISNEROS MANTILLA WALTER ERNESTO",
                VendedorCodigo = "Vendedor001",
                Limite = 3005,
                Garantia = 6005,
                TotalVencido = 9005,
                TotalAdeudado = 12005,
                UltimaFechaActualizacion = System.DateTime.Now,
                CreditoDisponible = 1005,
                ObjetivoCobro = 20058,
                RUC = "0101010105",
                TotalChequesPosfechados = 1505,
                OrdenesAbiertas = 1355,
                EntregasAbiertas = 1255
            };
            App.ListaClienteSqLite.Add(registro5);

            var registro6 = new ClienteSqLite
            {
                Codigo = "006",
                NombreCompleto = "JARAMILLO ORDONEZ EDWIN RODRIGO",
                VendedorCodigo = "Vendedor001",
                Limite = 3006,
                Garantia = 6006,
                TotalVencido = 9006,
                TotalAdeudado = 12006,
                UltimaFechaActualizacion = System.DateTime.Now,
                CreditoDisponible = 1006,
                ObjetivoCobro = 2006,
                RUC = "0101010106",
                TotalChequesPosfechados = 1506,
                OrdenesAbiertas = 1356,
                EntregasAbiertas = 1256
            };
            App.ListaClienteSqLite.Add(registro6);

            var registro7 = new ClienteSqLite
            {
                Codigo = "007",
                NombreCompleto = "DIPAC MANTA S.A.",
                VendedorCodigo = "Vendedor001",
                Limite = 3007,
                Garantia = 6007,
                TotalVencido = 9007,
                TotalAdeudado = 12007,
                UltimaFechaActualizacion = System.DateTime.Now,
                CreditoDisponible = 1007,
                ObjetivoCobro = 2007,
                RUC = "0101010107",
                TotalChequesPosfechados = 1507,
                OrdenesAbiertas = 1357,
                EntregasAbiertas = 1257
            };
            App.ListaClienteSqLite.Add(registro7);

            var registro8 = new ClienteSqLite
            {
                Codigo = "008",
                NombreCompleto = "DISTRIBUIDORA MERCANTIL ORIENTAL",
                VendedorCodigo = "Vendedor001",
                Limite = 3008,
                Garantia = 6008,
                TotalVencido = 9008,
                TotalAdeudado = 12008,
                UltimaFechaActualizacion = System.DateTime.Now,
                CreditoDisponible = 1008,
                ObjetivoCobro = 2008,
                RUC = "0101010108",
                TotalChequesPosfechados = 1508,
                OrdenesAbiertas = 1358,
                EntregasAbiertas = 1258
            };
            App.ListaClienteSqLite.Add(registro8);

            var registro9 = new ClienteSqLite
            {
                Codigo = "009",
                NombreCompleto = "FERRETERIA CARRERA",
                VendedorCodigo = "Vendedor001",
                Limite = 3009,
                Garantia = 6009,
                TotalVencido = 9009,
                TotalAdeudado = 12009,
                UltimaFechaActualizacion = System.DateTime.Now,
                CreditoDisponible = 1009,
                ObjetivoCobro = 2009,
                RUC = "0101010109",
                TotalChequesPosfechados = 1509,
                OrdenesAbiertas = 1359,
                EntregasAbiertas = 1250
            };
            App.ListaClienteSqLite.Add(registro9);

            var registro10 = new ClienteSqLite
            {
                Codigo = "010",
                NombreCompleto = "PROVEEDORA VILLAFUERTE DE ACEROS",
                VendedorCodigo = "Vendedor001",
                Limite = 3010,
                Garantia = 6010,
                TotalVencido = 9010,
                TotalAdeudado = 12010,
                UltimaFechaActualizacion = System.DateTime.Now,
                CreditoDisponible = 1010,
                ObjetivoCobro = 2010,
                RUC = "0101010110",
                TotalChequesPosfechados = 1510,
                OrdenesAbiertas = 1310,
                EntregasAbiertas = 1210
            };
            App.ListaClienteSqLite.Add(registro10);

            var infocredito = new InfoCreditoSqLite
            {
                Codigo = "1",
                NombreVendedor = "Juan Pablo Tapia",
                VendedorCodigo = "009876",
                Anio = 2018,
                Mes = 11,
                CreditoLimite = (decimal)18000,
                CreditoActual = (decimal)20000,
                ObjetivoCobro = (decimal)50000
            };
            await App.dataService.Insert(infocredito);

            await App.dataService.Insert(App.ListaClienteSqLite);

            var estadocuenta = new EstadoCuentaSqLite
            {
                ClienteCodigo = "001",
                Codigo = "8859034079",
                FechaVencimiento = System.DateTime.Now.AddDays(-40),
                NombreCorto = "001001000098884",
                Tipo = "RV",
                ValorRetencion = (decimal)6945.1, //valormonedalocal
                ValorTotal = (decimal)6945.1,
                SpecialGLIndicator = "",
                FacturaNumeroLegal = "001001000098884",
                Referencia = "001001000098884",
                Texto = "VIA MAIL CLIENTE",
                PaymentTerm = "Y795",
                NumeroDiasVencer = 40,
                Moneda = "USD",
                ValorNeto = (decimal)6945.1,
                EbillingDocument = "8859034079"
            };
            App.ListaFacturaSqLite.Add(estadocuenta);
            await App.dataService.Insert(estadocuenta);
            await App.dataService.Insert(App.ListaFacturaSqLite);

            Settings.SincronizacionCompleta = true;
        }

        private async void CargarClientes()
        {
           
            //this.clientesList = new List<Cliente>();
            //this.clientesSqLiteList = new List<ClienteSqLite>();
            this.IsRefreshing = true;
            var a= await App.apiService.CheckConnection();
            if (!a.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", a.Message, "Aceptar");
                Application.Current.MainPage= new NavigationPage(App.LoginPage) ;
                return;
            }
            //var response = await apiService.GetList<Cliente>(Global.UrlBase, Global.RoutePrefix, Global.ListarClientes,Settings.TokenType,Settings.AccessToken);
            //if (!response.IsSuccess)
            //{
            //    this.IsRefreshing = false;
            //    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
            //    await Application.Current.MainPage.Navigation.PopAsync();
            //    return;
            //}

            //this.paisesList= (List<Cliente>)response.Result;

            //clientesSqLiteList= await  this.dataService.ListarClientes();
            this.Clientes = new ObservableCollection<ClienteItemViewModel>(this.ConvertirClienteItem());
            this.IsRefreshing = false;
        }

       

        private IEnumerable<ClienteItemViewModel> ConvertirClienteItem()
        {

            return App.ListaClienteSqLite.Select(c => new ClienteItemViewModel
            {
                Codigo = c.Codigo,
                NombreCompleto = c.NombreCompleto,
                VendedorCodigo = c.VendedorCodigo,
                CreditoLimite = c.Limite,
                Garantia = c.Garantia,
                TotalVencido = c.TotalVencido,
                TotalFacturado = c.TotalAdeudado,
                UltimaFechaActualizacion = c.UltimaFechaActualizacion,
                ObjetivoCobro = c.ObjetivoCobro,
                CreditoDisponible = c.CreditoDisponible,
                RUC = c.RUC,
                TotalChequesPosfechados = c.TotalChequesPosfechados,
                OrdenesAbiertas = c.OrdenesAbiertas,
                EntregasAbiertas = c.EntregasAbiertas
            });
        }
        #endregion
    }
}
