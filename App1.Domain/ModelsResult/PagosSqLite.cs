using System;
using SQLite;

namespace App1.Domain.ModelsResult
{
    public class PagosSqLite
    {
        [PrimaryKey,AutoIncrement]
        public int PagoCodigo { get; set; }
        
        public string FacturaNumeroLegal { get; set; }

        public string CodigoDocumento { get; set; }

        public string FormaPagoCodigo { get; set; }

        public decimal Valor { get; set; }

        public string Banco { get; set; }

        public string NumeroCheque { get; set; }

        public DateTime? FechaCobro { get; set; }

        public string Postfechado { get; set; }

        public string NumeroTransferencia { get; set; }

        public DateTime? FechaTransferencia { get; set; }

        public string EnviadoIdeal { get; set; }

        public string EnviadoBwise { get; set; }

        public string Contrapartida { get; set; }

        public string TransferidoSAP { get; set; }
    }
}
