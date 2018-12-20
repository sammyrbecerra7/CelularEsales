using System.Windows.Input;
using App1.Views;
using Common.Models;
using GalaSoft.MvvmLight.Command;

namespace App1.ViewModels
{
    public class DocumentoItemViewModel : Documentos
    {
        private Documentos estadocuenta;
        public bool Seleccionado { get; set; }
        public DocumentoItemViewModel()
        {
            this.estadocuenta = this;
        }

        public ICommand DetalleDocumentoCommand { get { return new RelayCommand(DetalleDocumento); } }

        public bool EsFactura { get; internal set; }

        private async void DetalleDocumento()
        {
            var a = this;
            var estadoCuenta = new EstadoCuentaViewModel
            {
                Codigo = a.Codigo,
                ClienteCodigo = a.ClienteCodigo,
                NombreCorto = a.NombreCorto,
                TipoDocumento = a.TipoDocumento,
                Valor = a.Valor,
                ValorMonedaLocal = a.ValorMonedaLocal,
                FechaDocumento = a.FechaDocumento,
                SpecialGLIndicator = a.SpecialGLIndicator,
                FacturaNumeroLegal = a.FacturaNumeroLegal,
                Referencia = a.Referencia,
                Texto = a.Texto,
                PaymentTerm = a.PaymentTerm,
                Moneda = a.Moneda,
                ValorNeto = a.ValorNeto,
                EbillingDocument = a.EbillingDocument,
            };
            await App.Navigator.PushAsync(new DetalleDocumentoPage(estadoCuenta));
        }

    }
}
