using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using App1.Domain.Utils;
using App1.Views;
using Common.Models;
using GalaSoft.MvvmLight.Command;

namespace App1.ViewModels
{
   public class EstadoCuentaViewModel :Documentos
    {
        public Pagos PagoItem { get; set; }


        public EstadoCuentaViewModel()
        {

            this.PagoItem = new Pagos { Banco="PiChincha" };
         

        }


        public ICommand PagoCommand { get { return new RelayCommand(Pago); } }

        private async void Pago()
        {


            var listaPago = new List<Pagos>();
           

            var Pago1 = new Pagos
            {
                FacturaNumeroLegal = "001",
                CodigoDocumento = "1",
                FormaPagoCodigo = "1",
                Valor = 250,
                Banco = "PICHINCHA",
                EnviadoIdeal = "S",
                EnviadoBwise = "S",
                TransferidoSAP = "S",
            };

            listaPago.Add(Pago1);

            this.PagoItem.FacturaNumeroLegal = "001";
            this.PagoItem.CodigoDocumento = "1";
            this.PagoItem.FormaPagoCodigo = "1";
            this.PagoItem.Valor = 250;
            this.PagoItem.EnviadoIdeal = "S";
            this.PagoItem.EnviadoBwise = "S";
            this.PagoItem.TransferidoSAP = "S";

            listaPago.Add(this.PagoItem);


            var response = await App.apiService.Post<Response>(Global.UrlBase, Global.RoutePrefix, Global.InsertarPagos, Settings.TokenType, Settings.AccessToken, listaPago);

            

            await App.Navigator.PushAsync(new CobroPage());
        }
    }
}
