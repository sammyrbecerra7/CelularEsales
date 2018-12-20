using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Utils
{
   public class ClienteResponse
    {
        public string Codigo { get; set; }

        public decimal CreditoLimite { get; set; }

        public decimal CreditoDisponible { get; set; }

        public decimal ObjetivoCobro { get; set; }

        public decimal TotalFacturado { get; set; }

        public decimal TotalVencido { get; set; }

       
        public string NombreCompleto { get; set; }

     
        public string RUC { get; set; }

       
        public string VendedorCodigo { get; set; }

        public decimal Garantia { get; set; }

        public DateTime UltimaFechaActualizacion { get; set; }

        public decimal TotalChequesPosfechados { get; set; }

        public decimal OrdenesAbiertas { get; set; }

        public decimal EntregasAbiertas { get; set; }

        public virtual ICollection<Documentos> Documentos { get; set; }

        public virtual Vendedor Vendedor { get; set; }
    }
}
