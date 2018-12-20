using System.Collections.ObjectModel;
using App1.Domain.ModelsResult;
using App1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FacturasPage : ContentPage
	{
		public FacturasPage(ObservableCollection<DocumentoItemViewModel> lista)
		{
            BindingContext = new ListaFacturasViewModel(lista);
			InitializeComponent ();
            
		}
    }
}