namespace App1.API.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contrapartida")]
    public partial class Contrapartida
    {
        [Key]
        [StringLength(100)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(1)]
        public string FormaPagoCodigo { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Valor { get; set; }

        [Required]
        [StringLength(1)]
        public string EnviadoIdeal { get; set; }

        [Required]
        [StringLength(1)]
        public string EnviadoBwise { get; set; }

        public virtual FormaPago FormaPago { get; set; }
    }
}
