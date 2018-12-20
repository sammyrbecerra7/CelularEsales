namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Pagos
    {
        [Key]
        public int PagoCodigo { get; set; }

        [Required]
        [StringLength(50)]
        public string FacturaNumeroLegal { get; set; }

        [Required]
        [StringLength(50)]
        public string CodigoDocumento { get; set; }

        [Required]
        [StringLength(1)]
        public string FormaPagoCodigo { get; set; }

        public decimal Valor { get; set; }

        [Required]
        [StringLength(50)]
        public string Banco { get; set; }

        [StringLength(50)]
        public string NumeroCheque { get; set; }

        public DateTime? FechaCobro { get; set; }

        [StringLength(2)]
        public string Postfechado { get; set; }

        [StringLength(50)]
        public string NumeroTransferencia { get; set; }

        public DateTime? FechaTransferencia { get; set; }

        [Required]
        [StringLength(1)]
        public string EnviadoIdeal { get; set; }

        [Required]
        [StringLength(1)]
        public string EnviadoBwise { get; set; }

        [StringLength(100)]
        public string Contrapartida { get; set; }

        [Required]
        [StringLength(1)]
        public string TransferidoSAP { get; set; }

        public virtual Documentos Documentos { get; set; }

        public virtual FormaPago FormaPago { get; set; }
    }
}
