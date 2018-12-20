using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using App1.Views;
using Common.Models;
using GalaSoft.MvvmLight.Command;

namespace App1.ViewModels
{
   public class EstadoCuentaViewModel :Documentos
    {
        public Documentos EstadoCuenta;

        public EstadoCuentaViewModel()
        {
        }


        public ICommand PagoCommand { get { return new RelayCommand(Pago); } }

        private async void Pago()
        {
            await App.Navigator.PushAsync(new CobroPage());
        }
    }
}
