using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Utils
{
    public class InfoCreditoResponse
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
