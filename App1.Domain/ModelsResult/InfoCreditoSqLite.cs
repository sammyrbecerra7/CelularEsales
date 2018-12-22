using System;

namespace App1.Domain.ModelsResult
{
    public class InfoCreditoSqLite
    {
        public int Codigo { get; set; }

        public string VendedorCodigo { get; set; }

        public int Anio { get; set; }

        public int Mes { get; set; }

        public decimal CreditoLimite { get; set; }

        public decimal CreditoActual { get; set; }

        public decimal ObjetivoCobro { get; set; }
    }
}
