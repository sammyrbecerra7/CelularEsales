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
            Documentos = new HashSet<Documentos>();
        }

        [Key]
        [StringLength(50)]
        public string Codigo { get; set; }

        public decimal CreditoLimite { get; set; }

        public decimal CreditoDisponible { get; set; }

        public decimal ObjetivoCobro { get; set; }

        public decimal TotalFacturado { get; set; }

        public decimal TotalVencido { get; set; }

        [Required]
        [StringLength(255)]
        public string NombreCompleto { get; set; }

        [Required]
        [StringLength(15)]
        public string RUC { get; set; }

        [Required]
        [StringLength(50)]
        public string VendedorCodigo { get; set; }

        public decimal Garantia { get; set; }

        public DateTime UltimaFechaActualizacion { get; set; }

        public decimal TotalChequesPosfechados { get; set; }

        public decimal OrdenesAbiertas { get; set; }

        public decimal EntregasAbiertas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documentos> Documentos { get; set; }

        public virtual Vendedor Vendedor { get; set; }
    }
}
