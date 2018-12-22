using System.Collections.ObjectModel;
using System.Linq;
using App1.Domain.ModelsResult;
using App1.ViewModels;
using Common.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClienteTabPage : TabbedPage
	{
		public ClienteTabPage(Cliente cliente)
		{
            Children.Add(new ClientePage(cliente));
            //Children.Add(new BordesPage(pais.Borders));
            var listaFacturas = new ObservableCollection<DocumentoItemViewModel>(App.ListaDocumentoSqLite
                    .Where(x => x.ClienteCodigo == cliente.Codigo)
                    .Select(c => new DocumentoItemViewModel
                    {
                        Codigo = c.Codigo,
                        ClienteCodigo = c.ClienteCodigo,
                        NombreCorto = c.NombreCorto,
                        TipoDocumento = c.TipoDocumento,
                        Valor = c.Valor,
                        FechaDocumento = c.FechaDocumento,
                        SpecialGLIndicator = c.SpecialGLIndicator,
                        FacturaNumeroLegal = c.FacturaNumeroLegal,
                        Referencia = c.Referencia,
                        PaymentTerm = c.PaymentTerm,
                        Moneda = c.Moneda,
                        ValorNeto = c.ValorNeto,
                        EbillingDocument = c.EbillingDocument,
                        Seleccionado = false,
                        EsFactura=c.TipoDocumento=="DZ" ?true:false,
                    }
                    ).ToList());
            Children.Add(new FacturasPage(listaFacturas));
            //Children.Add(new MonedasPage(pais.Currencies));
            //Children.Add(new TranslationsPage(pais.Translations));
            //BindingContext = new PaisViewModel(pais);
            InitializeComponent();

        }
	}
}