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
		public ClienteTabPage(ClienteSqLite cliente)
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
                        Valor = c.Valor,
                        FechaDocumento = c.FechaDocumento,
                        NumeroDiasAVencer=c.NumeroDiasAVencer,
                        FacturaNumeroLegal=c.FacturaNumeroLegal,
                    }
                    ).ToList());


            Children.Add(new DocumentosPage(listaFacturas));
            //Children.Add(new MonedasPage(pais.Currencies));
            //Children.Add(new TranslationsPage(pais.Translations));
            //BindingContext = new PaisViewModel(pais);
            InitializeComponent();

        }
	}
}