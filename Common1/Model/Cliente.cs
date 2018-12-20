namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Cliente")]
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            EstadoCuenta = new HashSet<EstadoCuenta>();
        }

        [Key]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCompleto { get; set; }

        [Required]
        [StringLength(50)]
        public string VendedorCodigo { get; set; }

        public decimal Limite { get; set; }

        public decimal Garantia { get; set; }

        public decimal TotalVencido { get; set; }

        public decimal TotalAdeudado { get; set; }

        public DateTime UltimaFechaActualizacion { get; set; }

        public virtual Vendedor Vendedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstadoCuenta> EstadoCuenta { get; set; }
    }
}
