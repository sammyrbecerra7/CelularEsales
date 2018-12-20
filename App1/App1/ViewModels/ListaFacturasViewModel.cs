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
        public ObservableCollection<DocumentoItemViewModel> Facturas { get; set; }

        public ListaFacturasViewModel(ObservableCollection<DocumentoItemViewModel> facturas)
        {
            Facturas = facturas;
        }

        public ICommand PagarCommand { get { return new RelayCommand(Pagar); } }

        private async void Pagar()
        {
            var a = Facturas;
            await App.Navigator.PushAsync(new CobroPage());
        }

    }
}
