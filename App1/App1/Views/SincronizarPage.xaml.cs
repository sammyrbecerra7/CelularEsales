using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Domain.ModelsResult;
using App1.Domain.Utils;
using Common.Models;
using Common.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SincronizarPage : ContentPage
	{
		public SincronizarPage ()
		{
			InitializeComponent ();
            Task.Delay(5000);
            
        }
        


       
    }
}