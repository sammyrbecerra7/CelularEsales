using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Domain.ModelsResult;
using App1.Services;
using App1.Views;
using Common.Models;
using GalaSoft.MvvmLight.Command;

namespace App1.ViewModels
{
    public class InfoCreditoViewModel
    {
        public InfoCreditoSqLite InfoCredito { get; set; }

        public InfoCreditoViewModel()
        {
            InfoCredito = new InfoCreditoSqLite();
            InfoCredito = App.InfoCreditoSqLite;
         
        }
    }
}
