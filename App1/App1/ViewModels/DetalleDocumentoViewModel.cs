using System.Windows.Input;
using App1.Views;
using Common.Models;
using GalaSoft.MvvmLight.Command;

namespace App1.ViewModels
{
    public class DetalleDocumentoViewModel:Documentos
    {
        private Documentos estadocuenta;
        public DetalleDocumentoViewModel()
        {
            this.estadocuenta = this;
        }

        public ICommand PagoCommand { get { return new RelayCommand(RealizaPago); } }

        private async void RealizaPago()
        {
            await App.Navigator.PushAsync(new  InfoCreditoPage());
        }

        //public ICommand DocumentosCommand { get { return new RelayCommand(DocumentosRelacionados); } }

        //private async void DocumentosRelacionados()
        //{

        //    await App.Navigator.PushAsync(new DocuRelaPage());
        //}
    }
}
