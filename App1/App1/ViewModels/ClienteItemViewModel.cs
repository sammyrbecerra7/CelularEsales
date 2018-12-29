using System.Windows.Input;
using App1.Domain.ModelsResult;
using App1.Views;
using Common.Models;
using GalaSoft.MvvmLight.Command;

namespace App1.ViewModels
{
    public class ClienteItemViewModel:ClienteSqLite
    {
        private ClienteSqLite cliente;
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
