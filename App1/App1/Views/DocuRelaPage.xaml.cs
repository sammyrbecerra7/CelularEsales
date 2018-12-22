using App1.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DocuRelaPage : ContentPage
	{
		public DocuRelaPage (ObservableCollection<DocumentoItemViewModel> lista)
		{
            BindingContext = new ListaFacturasViewModel(lista);
            InitializeComponent ();
		}
	}
}