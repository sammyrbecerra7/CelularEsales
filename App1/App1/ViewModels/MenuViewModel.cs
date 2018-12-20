using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using App1.Domain.ModelsResult;
using App1.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App1.ViewModels
{
   public class MenuViewModel
    {
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
