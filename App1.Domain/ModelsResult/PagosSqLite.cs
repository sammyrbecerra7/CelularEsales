using System;

namespace App1.Domain.ModelsResult
{
    public class PagosSqLite
    {
        public string Documento { get; set; }
        public decimal Valor { get; set; }
        public string FormaPagoCodigo { get; set; }
        public string Dato1 { get; set; }
        public string Dato2 { get; set; }
        public string Dato3 { get; set; }
        public decimal Dato4 { get; set; }
        public decimal Dato5 { get; set; }
        public decimal Dato6 { get; set; }
        public string EnviadoIdeal { get; set; }
        public string EnviadoBwise { get; set; }
        public string Contrapartida { get; set; }
    }
}
