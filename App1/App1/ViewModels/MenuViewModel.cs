using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Domain.ModelsResult;
using App1.Domain.Utils;
using App1.Views;
using Common.Models;
using Common.Utils;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App1.ViewModels
{
   public class MenuViewModel: PropertyChangedClass
    {
        private bool isVisible;
        private bool isRunning;
        public MenuViewModel()
        {
            IsVisible = true;
            IsRunning = false;
            
        }

        private string mensajeCargando;
        public string MensajeCargando
        {
            get { return this.mensajeCargando; }
            set { SetValue(ref this.mensajeCargando, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsVisible
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }

        #region Commands
        public ICommand LogoutCommand
        {
            get
            {
                return new RelayCommand(Logout);
            }
        }

        public ICommand InformacionCreditoCommand
        {
            get
            {
                return new RelayCommand(InformacionCredito);
            }
        }


        public ICommand SincronizarCommand
        {
            get
            {
                return new RelayCommand(Sincronizar);
            }
        }

        private async void Sincronizar()
        {
            var conexion = await App.apiService.CheckConnection();
            if (!conexion.IsSuccess)
            {
                this.IsRunning = false;

                IsVisible = true;
                await Application.Current.MainPage.DisplayAlert("Error", conexion.Message, "Aceptar");
                return;
            }
            MensajeCargando = Mensaje.Sincronizando;
            IsVisible = false;
            IsRunning = true;
            var result = Task.Run(() => App.SincronizarService.Sincronizar().Result).Result;
            if (result == true)
            {
                await Task.Delay(3000);
                IsRunning = false;
                IsVisible = true;
                Application.Current.MainPage = new MasterPage();
            }

            IsRunning = false;
            IsVisible = true;
            await Application.Current.MainPage.DisplayAlert("Error", Mensaje.ErrorAlSincornizar, "Aceptar");
        }

        private async void InformacionCredito()
        {
            App.Master.IsPresented = false;
            await App.Navigator.PushAsync(new InfoCreditoPage());
        }

        private async void Logout()
        {
                Settings.AccessToken = string.Empty;
                Settings.TokenType = string.Empty;
                Settings.IsRemembered = false;
                Application.Current.MainPage = new NavigationPage(App.LoginPage);
            
        }


       
        #endregion
    }
}
