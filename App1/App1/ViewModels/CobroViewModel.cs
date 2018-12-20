using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Domain.ModelsResult;
using App1.Domain.Utils;
using App1.Helpers;
using App1.Services;
using App1.Views;
using Common.Models;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.ViewModels
{
    public class CobroViewModel : PropertyChangedClass
    {
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;


        public string formapago { get; set; }
        public string valor { get; set; }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }


        public CobroViewModel()
        {
            apiService = new ApiService();
            formapago = "";
            valor = "";
            IsEnabled = true;
        }

        public ICommand LoginCommand { get { return new RelayCommand(Cobrar); } }

        private async void Cobrar()
        {
            //string ResultMessage = await App.serviceManager.ValidateLogin("","");

            this.IsRunning = true;
            this.IsEnabled = false;
            if (string.IsNullOrEmpty(formapago))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Aceptar, Languages.Error, Languages.Aceptar);
                this.IsRunning = false;
                this.IsEnabled = true;
                return;
            }

            if (string.IsNullOrEmpty(this.valor))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar un valor", "Aceptar");
                return;
            }

            var conexion = await apiService.CheckConnection();
            if (!conexion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", conexion.Message, "Aceptar");
                return;
            }

            var token = await apiService.GetToken(Global.UrlBase, this.formapago, this.valor);
            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error, intente de nuevo", "Aceptar");
                return;
            }
            return;
        }
    }
}
