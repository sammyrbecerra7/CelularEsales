using App1.Domain.ModelsResult;
using App1.ViewModels;
using Common.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClientePage : ContentPage
	{
		public ClientePage(ClienteSqLite cliente)
		{
            BindingContext = new ClienteViewModel(cliente);
			InitializeComponent ();
		}
	}
}