namespace App1.API.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FormaPago")]
    public partial class FormaPago
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FormaPago()
        {
            Contrapartida = new HashSet<Contrapartida>();
            Pagos = new HashSet<Pagos>();
        }

        [Key]
        [StringLength(1)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Dato1 { get; set; }

        [StringLength(100)]
        public string Dato2 { get; set; }

        [StringLength(100)]
        public string Dato3 { get; set; }

        [StringLength(100)]
        public string Dato4 { get; set; }

        [StringLength(100)]
        public string Dato5 { get; set; }

        [StringLength(100)]
        public string Dato6 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contrapartida> Contrapartida { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pagos> Pagos { get; set; }
    }
}
