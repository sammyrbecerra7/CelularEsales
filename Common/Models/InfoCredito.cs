namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InfoCredito")]
    public partial class InfoCredito
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string VendedorCodigo { get; set; }

        public int Anio { get; set; }

        public int Mes { get; set; }

        public decimal CreditoLimite { get; set; }

        public decimal CreditoActual { get; set; }

        public decimal ObjetivoCobro { get; set; }

        public virtual Vendedor Vendedor { get; set; }
    }
}
