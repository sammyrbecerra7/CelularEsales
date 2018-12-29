using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using App1.Domain.ModelsResult;
using App1.Views;
using GalaSoft.MvvmLight.Command;

namespace App1.ViewModels
{
    class ListaFacturasViewModel
    {
        public ObservableCollection<DocumentoItemViewModel> Documentos { get; set; }

        public ListaFacturasViewModel(ObservableCollection<DocumentoItemViewModel> documentos)
        {
            Documentos = documentos;
        }

        public ICommand PagarCommand { get { return new RelayCommand(Pagar); } }

        private async void Pagar()
        {
            var a = Documentos;
            await App.Navigator.PushAsync(new CobroPage());
        }

    }
}
