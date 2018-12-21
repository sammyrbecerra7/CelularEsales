using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Utils
{
    public class DocumentoResponse
    {

        public string Codigo { get; set; }

        public string ClienteCodigo { get; set; }

        public string SpecialGLIndicator { get; set; }

        public string FacturaNumeroLegal { get; set; }

        public DateTime? FechaDocumento { get; set; }

        public string Referencia { get; set; }

        public string TipoDocumento { get; set; }

        public decimal ValorMonedaLocal { get; set; }

        public decimal Valor { get; set; }

        public string Texto { get; set; }

        public string PaymentTerm { get; set; }

        public int? NumeroDiasAVencer { get; set; }

        public string Moneda { get; set; }

        public decimal? ValorNeto { get; set; }

        public string NombreCorto { get; set; }

        public string EbillingDocument { get; set; }
    }
}
