using System;
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
       
        private async void CargarClientes()
        {

            this.IsRefreshing = true;

            this.Clientes = new ObservableCollection<ClienteItemViewModel>(this.ConvertirClienteItem());
            this.IsRefreshing = false;
        }
        
        private IEnumerable<ClienteItemViewModel> ConvertirClienteItem()
        {

            return App.ListaClienteSqLite.Select(x => new ClienteItemViewModel
            {
                NombreCompleto = x.NombreCompleto,
                Codigo = x.Codigo,
                CreditoDisponible = x.CreditoDisponible,
                CreditoLimite = x.CreditoLimite ==null ? Convert.ToDecimal(0.0) :Convert.ToDecimal(x.CreditoLimite),
                VendedorCodigo = x.VendedorCodigo,
                Garantia = x.Garantia,
                RUC = x.RUC,
                TotalFacturado = x.TotalFacturado,
                TotalVencido = x.TotalVencido,
                ObjetivoCobro = x.ObjetivoCobro,
                EntregasAbiertas = x.EntregasAbiertas,
                TotalChequesPosfechados = x.TotalChequesPosfechados,
                OrdenesAbiertas = x.OrdenesAbiertas,
            });
        }
        #endregion
    }
}
