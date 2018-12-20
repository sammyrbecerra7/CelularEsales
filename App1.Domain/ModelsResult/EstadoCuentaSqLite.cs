using System;
using System.Windows.Input;

namespace App1.Domain.ModelsResult
{
    public class EstadoCuentaSqLite
    {
        public string Codigo { get; set; } //
        public string ClienteCodigo { get; set; } //
        public string NombreCorto { get; set; } //
        public string Tipo { get; set; } //
        public decimal ValorTotal { get; set; } //
        public decimal ValorRetencion { get; set; } //
        public DateTime FechaVencimiento { get; set; } //

        public string SpecialGLIndicator { get; set; }
        public string FacturaNumeroLegal { get; set; }
        public string Referencia { get; set; }
        public string Texto { get; set; }
        public string PaymentTerm { get; set; }
        public int NumeroDiasVencer { get; set; }
        public string Moneda { get; set; }
        public decimal ValorNeto { get; set; }
        public string EbillingDocument { get; set; }

    }
}