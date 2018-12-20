using System.Windows.Input;
using App1.Views;
using Common.Models;
using GalaSoft.MvvmLight.Command;

namespace App1.ViewModels
{
    public class ClienteItemViewModel:Cliente
    {
        private Cliente cliente;
        public ClienteItemViewModel()
        {
            this.cliente = this;
        }

        public ICommand DetalleClienteCommand { get { return new RelayCommand(DetalleCliente); } }

        private async void DetalleCliente()
        {
            await App.Navigator.PushAsync(new ClienteTabPage(cliente));
        }
    }
}
