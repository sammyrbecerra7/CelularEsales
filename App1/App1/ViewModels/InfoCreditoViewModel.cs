using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Domain.ModelsResult;
using App1.Services;
using App1.Views;
using Common.Models;
using GalaSoft.MvvmLight.Command;

namespace App1.ViewModels
{
    public class InfoCreditoViewModel
    {
        public InfoCreditoSqLite InfoCredito { get; set; }

        public InfoCreditoViewModel()
        {
            
            Task.Run(() => this.ObtenerDatos()).Wait();
        }

        public async Task ObtenerDatos()
        {
            this.InfoCredito = new InfoCreditoSqLite();
            InfoCredito = await App.dataService.ObtenerInformacionCredito();
        }


        public ICommand PagoCommand { get { return new RelayCommand(RealizaPago); } }

        private async void RealizaPago()
        {
            await App.Navigator.PushAsync(new InfoCreditoPage());
        }

    }
}
