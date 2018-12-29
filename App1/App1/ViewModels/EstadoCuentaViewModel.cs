using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using App1.Domain.ModelsResult;
using App1.Domain.Utils;
using App1.Views;
using Common.Models;
using Common.Utils;
using GalaSoft.MvvmLight.Command;

namespace App1.ViewModels
{
    public class FormaPago
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }

    public class Banco
    {
        public string Nombre { get; set; }
    }

    public class PostFechado
    {
        public string Nombre { get; set; }
    }

    public class EstadoCuentaViewModel : DocumentosSqLite, INotifyPropertyChanged
    {




        private bool isVisibleCheque = false;
        public bool IsVisibleCheque
        {
            set
            {
                if (isVisibleCheque != value)
                {
                    isVisibleCheque = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsVisibleCheque"));
                }
            }
            get
            {
                return isVisibleCheque;
            }
        }

        private bool isVisibleTransferencia = false;
        public bool IsVisibleTransferencia
        {
            set
            {
                if (isVisibleTransferencia != value)
                {
                    isVisibleTransferencia = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsVisibleTransferencia"));
                }
            }
            get
            {
                return isVisibleTransferencia;
            }
        }

        private bool isVisiblePostFechado = false;
        public bool IsVisiblePostFechado
        {
            set
            {
                if (isVisiblePostFechado != value)
                {
                    isVisiblePostFechado = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsVisiblePostFechado"));
                }
            }
            get
            {
                return isVisiblePostFechado;
            }
        }


        private bool isVisible = true;
        public bool IsVisible
        {
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsVisible"));
                }
            }
            get
            {
                return isVisible;
            }
        }


        private bool isRunning;

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }



        public PagosSqLite PagoItem { get; set; }


        public List<Banco> ListaBanco { get; set; }

        public Banco Banco { get; set; }

        public List<FormaPago> ListaFormaPago { get; set; }

        public List<PostFechado> ListaPostFechado { get; set; }


        private FormaPago formaPago { get; set; }
        public FormaPago FormaPago
        {
            get
            {
                return formaPago;
            }

            set
            {
                if (formaPago != value)
                {
                    formaPago = value;
                    RenderVista(formaPago.Nombre);

                }
            }
        }


        private PostFechado postFechado { get; set; }
        public PostFechado PostFechado
        {
            get
            {
                return postFechado;
            }

            set
            {
                if (postFechado != value)
                {
                    postFechado = value;
                    RenderVistaPostFechado(postFechado.Nombre);

                }
            }
        }

        private void RenderVistaPostFechado(string nombre)
        {
            if (!string.IsNullOrEmpty(nombre))
            {
                if (nombre == "SI")
                {
                    IsVisiblePostFechado = true;
                    IsVisibleTransferencia = false;
                    IsVisibleCheque = true;
                }
                if (nombre == "NO")
                {
                    IsVisiblePostFechado = false;
                    IsVisibleTransferencia = false;
                    IsVisibleCheque = true;
                }

            }
        }

        private void RenderVista(string nombre)
        {
            if (!string.IsNullOrEmpty(nombre))
            {
                if (nombre == "Cheque")
                {
                    PagoItem.Valor = 0;
                    if (PostFechado.Nombre == "SI")
                    {
                        IsVisiblePostFechado = true;
                    }
                    IsVisiblePostFechado = false;
                    IsVisibleTransferencia = false;
                    IsVisibleCheque = true;
                }

                if (nombre == "Transferencia")
                {
                    IsVisiblePostFechado = false;
                    IsVisibleTransferencia = true;
                    IsVisibleCheque = false;
                }
            }

        }

        public EstadoCuentaViewModel(DocumentosSqLite documento)
        {

            PostFechado = new PostFechado();
            var PostFechadoSI = new PostFechado { Nombre = "SI" };
            var PostFechadoNO = new PostFechado { Nombre = "NO" };
            ListaPostFechado = new List<PostFechado>();
            ListaPostFechado.Add(PostFechadoSI);
            ListaPostFechado.Add(PostFechadoNO);


            Banco = new Banco();
            var Pichincha = new Banco { Nombre = "Pichincha" };
            var Guayaquil = new Banco { Nombre = "Guayaquil" };
            var Pacifico = new Banco { Nombre = "Pacífico" };
            var Internacional = new Banco { Nombre = "Internacional" };
            ListaBanco = new List<Banco>();
            ListaBanco.Add(Pichincha);
            ListaBanco.Add(Guayaquil);
            ListaBanco.Add(Pacifico);
            ListaBanco.Add(Internacional);

            FormaPago = new FormaPago();
            var cheque = new FormaPago { Nombre = "Cheque", Codigo = "1" };
            var trasferencia = new FormaPago { Nombre = "Transferencia", Codigo = "2" };
            ListaFormaPago = new List<FormaPago>();
            ListaFormaPago.Add(cheque);
            ListaFormaPago.Add(trasferencia);



            this.PagoItem = new PagosSqLite
            {
                CodigoDocumento = documento.Codigo,
                FacturaNumeroLegal = documento.FacturaNumeroLegal,
                FechaCobro = DateTime.Now.Date,
                FechaTransferencia = DateTime.Now.Date,
            };


        }


        public ICommand PagoCommand { get { return new RelayCommand(Pago); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Pago()
        {
            IsVisible = false;
            IsRunning = true;

            if (string.IsNullOrEmpty(FormaPago.Nombre))
            {
                IsVisible = true;
                IsRunning = false;
                await App.Navigator.DisplayAlert(Mensaje.Error, Mensaje.ValidarFormulario, Mensaje.Aceptar);
                return;
            }

            if (FormaPago.Nombre == "Cheque" && string.IsNullOrEmpty(PostFechado.Nombre))
            {
                IsVisible = true;
                IsRunning = false;
                await App.Navigator.DisplayAlert(Mensaje.Error, Mensaje.ValidarFormulario, Mensaje.Aceptar);
                return;
            }

            var pago = new PagosSqLite();


            if (FormaPago.Nombre == "Cheque" && PostFechado.Nombre == "SI")
            {
                if (string.IsNullOrEmpty(this.Banco.Nombre) || this.PagoItem.Valor <= 0 || string.IsNullOrEmpty(this.PagoItem.NumeroCheque))
                {
                    IsVisible = true;
                    IsRunning = false;
                    await App.Navigator.DisplayAlert(Mensaje.Error, Mensaje.ValidarFormulario, Mensaje.Aceptar);
                    return;
                }

                pago.CodigoDocumento = this.PagoItem.CodigoDocumento;
                pago.FacturaNumeroLegal = this.PagoItem.FacturaNumeroLegal;
                pago.Valor = this.PagoItem.Valor;
                pago.NumeroCheque = this.PagoItem.NumeroCheque;
                pago.FechaCobro = this.PagoItem.FechaCobro;
                pago.Banco = this.Banco.Nombre;
                pago.FormaPagoCodigo = this.FormaPago.Codigo;


            }

            if (FormaPago.Nombre == "Cheque" && PostFechado.Nombre == "NO")
            {
                if (this.PagoItem.Valor <= 0)
                {
                    IsVisible = true;
                    IsRunning = false;
                    await App.Navigator.DisplayAlert(Mensaje.Error, Mensaje.ValidarFormulario, Mensaje.Aceptar);
                    return;
                }

                pago.CodigoDocumento = this.PagoItem.CodigoDocumento;
                pago.FacturaNumeroLegal = this.PagoItem.FacturaNumeroLegal;
                pago.Valor = this.PagoItem.Valor;
                pago.FormaPagoCodigo = this.FormaPago.Codigo;
                pago.Banco = string.Empty;
            }

            if (FormaPago.Nombre == "Transferencia")
            {
                if (string.IsNullOrEmpty(this.PagoItem.NumeroTransferencia) || this.PagoItem.Valor <= 0)
                {
                    IsVisible = true;
                    IsRunning = false;
                    await App.Navigator.DisplayAlert(Mensaje.Error, Mensaje.ValidarFormulario, Mensaje.Aceptar);
                    return;
                }
                pago.CodigoDocumento = this.PagoItem.CodigoDocumento;
                pago.FechaTransferencia = this.PagoItem.FechaTransferencia;
                pago.NumeroTransferencia = this.PagoItem.NumeroTransferencia;
                pago.FacturaNumeroLegal = this.PagoItem.FacturaNumeroLegal;
                pago.Valor = this.PagoItem.Valor;
                pago.FormaPagoCodigo = this.FormaPago.Codigo;
                pago.Banco = string.Empty;

            }


            pago.EnviadoBwise = "N";
            pago.EnviadoIdeal = "N";
            pago.TransferidoSAP = "N";

            var listaPago = new List<PagosSqLite>();
            listaPago.Add(pago);

            await App.dataService.Insert(pago);

            var connection = await App.apiService.CheckConnection();
            if (connection.IsSuccess )
            {
                var response = await App.apiService.Post<Response>(Global.UrlBase, Global.RoutePrefix, Global.InsertarPagos, Settings.TokenType, Settings.AccessToken, listaPago);
            }

            IsVisible = true;
            IsRunning = false;
            await App.Master.DisplayAlert(Mensaje.Informacion, Mensaje.Satisfactorio, Mensaje.Aceptar);
            if (FormaPago.Nombre == "Cheque" && PostFechado.Nombre == "SI")
            {
                await App.Navigator.PopAsync();
                return;
            }

            await App.Navigator.PushAsync(new CobroPage());

        }
    }
}
