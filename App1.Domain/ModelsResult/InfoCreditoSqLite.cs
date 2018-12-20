using System;

namespace App1.Domain.ModelsResult
{
    public class InfoCreditoSqLite
    {
        public string Codigo { get; set; }
        public string NombreVendedor { get; set; }
        public string VendedorCodigo { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public decimal CreditoLimite { get; set; }
        public decimal CreditoActual { get; set; }
        public decimal ObjetivoCobro { get; set; }
    }
}
