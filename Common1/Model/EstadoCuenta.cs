namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EstadoCuenta")]
    public partial class EstadoCuenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EstadoCuenta()
        {
            Pagos = new HashSet<Pagos>();
        }

        [Key]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string ClienteCodigo { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreCorto { get; set; }

        [Required]
        [StringLength(1)]
        public string Tipo { get; set; }

        public decimal ValorTotal { get; set; }

        public decimal? ValorRetencion { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public virtual Cliente Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pagos> Pagos { get; set; }
    }
}
