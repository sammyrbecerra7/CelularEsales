using System;

namespace App1.Domain.ModelsResult
{
    public class ClienteSqLite
    {
        public string Codigo { get; set; }
        public string NombreCompleto { get; set; } //
        public string VendedorCodigo { get; set; }
        public decimal Limite { get; set; } //
        public decimal Garantia { get; set; } //
        public decimal TotalVencido { get; set; }
        public decimal TotalAdeudado { get; set; }
        public DateTime UltimaFechaActualizacion { get; set; }

        public decimal CreditoDisponible { get; set; }
        public decimal ObjetivoCobro { get; set; }
        public string RUC { get; set; }
        public decimal TotalChequesPosfechados { get; set; }
        public decimal OrdenesAbiertas { get; set; }
        public decimal EntregasAbiertas { get; set; }
    }
    //en el dataservice del app1 modificar de acuerdo al modelo
}