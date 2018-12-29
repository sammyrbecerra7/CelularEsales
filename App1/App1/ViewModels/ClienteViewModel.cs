using System;
using System.Collections.Generic;
using System.Text;
using Common.Models;
using App1.Models;
using App1.Views;
using Xamarin.Forms;
using App1.Domain.ModelsResult;

namespace App1.ViewModels
{
   public class ClienteViewModel
    {
        public ClienteSqLite Cliente { get; set; }
        public ClienteViewModel(ClienteSqLite cliente)
        {
            this.Cliente = cliente;
        }
    }
}
