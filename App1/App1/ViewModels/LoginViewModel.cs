﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Domain.ModelsResult;
using App1.Domain.Utils;
using App1.Helpers;
using App1.Services;
using App1.Views;
using Common.Models;
using Common.Utils;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class LoginViewModel : PropertyChangedClass
    {
        #region Atributos

        private string contrasena ;
        
        private bool isRunning ;
        
        

        private bool isVisible;

        #endregion

        #region Propiedades


        public string Email { get; set; }
        public string Contrasena
        {
            get { return this.contrasena; }
            set { SetValue(ref this.contrasena, value); }
        }

       

        public bool Recuerdame { get; set; }
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

        private string mensajeCargando;
        public string MensajeCargando
        {
            get { return this.mensajeCargando; }
            set { SetValue(ref this.mensajeCargando, value); }
        }

        #endregion

        #region Constructores

        public LoginViewModel()
        {
           
            Email = string.Empty;
            Contrasena =string.Empty;
            Recuerdame = true;
            IsRunning = false;
            IsVisible = true;


        }
        #endregion


        #region Comandos

        public ICommand LoginCommand { get { return new RelayCommand(Login); } }







        #endregion

        #region Metodos
        private async void Login()
        {
            this.IsRunning = true;
           
            IsVisible = false;
            MensajeCargando = Mensaje.Autenticando;

            if (string.IsNullOrEmpty(Email))
            {
                IsVisible = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Aceptar, Languages.ValidacionEmail,Languages.Aceptar);
                return;
            }

            if (string.IsNullOrEmpty(this.Contrasena))
            {
                this.IsRunning = false;
                this.IsVisible = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar la contraseña", "Aceptar");
                return;
            }

            var conexion = await App.apiService.CheckConnection();
            if (!conexion.IsSuccess)
            {
                this.IsRunning = false;
                 
                IsVisible = true;
                await Application.Current.MainPage.DisplayAlert("Error", conexion.Message, "Aceptar");
                return;
            }

            MensajeCargando = Mensaje.Sincronizando;
            var token = await App.apiService.GetToken(Global.UrlBase, this.Email, this.Contrasena);
            if (token == null)
            {
                this.IsRunning = false;
                 
                IsVisible = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error, intente de nuevo", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                 
                IsVisible = true;
                await Application.Current.MainPage.DisplayAlert("Error", token.ErrorDescription, "Aceptar");
                this.Contrasena = string.Empty;
                return;
            }
            


            Settings.TokenType = token.TokenType;
            Settings.AccessToken = token.AccessToken;
            Settings.UserASP = token.UserName; //aquí guardamos el nombre del asesor
            Settings.IsRemembered = this.Recuerdame;
           
            this.IsRunning = false;
             
          
            var result= Task.Run(() => App.SincronizarService.Sincronizar().Result).Result;
            if (result==true)
            {
                this.IsVisible = true;
                IsRunning = false;
                Application.Current.MainPage = new MasterPage();
            }

            IsRunning = false;
            IsVisible = true;
            await Application.Current.MainPage.DisplayAlert("Error", Mensaje.ErrorAlSincornizar, "Aceptar");
            return;
        }
        #endregion
    }
}
