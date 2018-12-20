using System;

namespace App1.Domain.ModelsResult
{
    public class ContrapartidaSqLite
    {
        public string Codigo { get; set; }
        public string FormaPagoCodigo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Valor { get; set; }
        public string EnviadoIdeal { get; set; }
        public string EnviadoBwise { get; set; }
    }
}
