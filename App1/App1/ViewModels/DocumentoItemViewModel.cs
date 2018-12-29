using System.Windows.Input;
using App1.Domain.ModelsResult;
using App1.Views;
using Common.Models;
using GalaSoft.MvvmLight.Command;

namespace App1.ViewModels
{
    public class DocumentoItemViewModel : DocumentosSqLite
    {
        private DocumentosSqLite estadocuenta;
        public DocumentoItemViewModel()
        {
            this.estadocuenta = this;
        }

        public ICommand DetalleDocumentoCommand { get { return new RelayCommand(DetalleDocumento); } }

        private async void DetalleDocumento()
        {
            var a = this;
            
            await App.Navigator.PushAsync(new DetalleDocumentoPage(a));
        }

    }
}
