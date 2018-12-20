using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.ViewModels;
using Common.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleDocumentoPage : ContentPage
	{
		public DetalleDocumentoPage (EstadoCuentaViewModel estadoCuenta)
		{
            BindingContext = new EstadoCuentaViewModel();
			InitializeComponent ();
		}
	}
}