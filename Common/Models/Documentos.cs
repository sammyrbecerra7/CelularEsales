namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Documentos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Documentos()
        {
            Pagos = new HashSet<Pagos>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string ClienteCodigo { get; set; }

        [StringLength(1)]
        public string SpecialGLIndicator { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string FacturaNumeroLegal { get; set; }

        public DateTime? FechaDocumento { get; set; }

        [StringLength(50)]
        public string Referencia { get; set; }

        [Required]
        [StringLength(2)]
        public string TipoDocumento { get; set; }

        public decimal ValorMonedaLocal { get; set; }

        public decimal Valor { get; set; }

        [StringLength(50)]
        public string Texto { get; set; }

        [StringLength(4)]
        public string PaymentTerm { get; set; }

        public int? NumeroDiasAVencer { get; set; }

        [Required]
        [StringLength(3)]
        public string Moneda { get; set; }

        public decimal? ValorNeto { get; set; }

        [StringLength(50)]
        public string NombreCorto { get; set; }

        [StringLength(50)]
        public string EbillingDocument { get; set; }

        public virtual Cliente Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pagos> Pagos { get; set; }
    }
}
