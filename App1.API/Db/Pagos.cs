namespace App1.API.Db
{
    using Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pagos
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string CodigoDocumento { get; set; }

        public decimal Valor { get; set; }

        [Required]
        [StringLength(1)]
        public string FormaPagoCodigo { get; set; }

        [StringLength(50)]
        public string Dato1 { get; set; }

        [StringLength(50)]
        public string Dato2 { get; set; }

        [StringLength(50)]
        public string Dato3 { get; set; }

        public decimal? Dato4 { get; set; }

        public decimal? Dato5 { get; set; }

        public decimal? Dato6 { get; set; }

        [Required]
        [StringLength(1)]
        public string EnviadoIdeal { get; set; }

        [Required]
        [StringLength(1)]
        public string EnviadoBwise { get; set; }

        [StringLength(100)]
        public string Contrapartida { get; set; }

        public virtual EstadoCuenta EstadoCuenta { get; set; }

        public virtual FormaPago FormaPago { get; set; }
       
    }
}
